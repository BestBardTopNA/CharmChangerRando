using System;
using Modding;
using System.Linq;
using CharmChanger;
using RandomizerMod.RC;
using System.Reflection;
using MonoMod.RuntimeDetour;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CharmChangerRando
{
    public class CharmChangerRandoMod() : Mod(name: "CharmChangerRando"), IGlobalSettings<GlobalSettings>
    {
        private static readonly MethodInfo RandoControllerRun = typeof(RandoController).GetMethod(nameof(RandoController.Run));
        private static readonly Regex charmNotchCostRegex = new(@"charm(\d+)NotchCost");
        public static GlobalSettings GS { get; private set; } = new();
        private static CharmChangerRandoSettings Settings => GS.CharmChangerRandoSettings;

        private Hook _hook;

        public override string GetVersion() => "v2";

        private class FieldRandomizer(FieldInfo fieldInfo)
        {
            private readonly FieldInfo fieldInfo = fieldInfo;

            public double Value { get; private set; } = 0;
            public bool IsRandomized { get; private set; } = false;
            public bool IsRandomizable => (!Settings.ExcludeRegularStats || !fieldInfo.Name.StartsWith("regular")) && !IsRandomized;

            public (bool positive, string reference, Charms charm) Relation { get; } =
                CharmRelations.Relations.ContainsKey(fieldInfo.Name) ? CharmRelations.Relations[fieldInfo.Name] : (true, null, Charms.NullCharm);

            public void Randomize(Dictionary<string, FieldRandomizer> fieldRandomizers, Dictionary<Charms, (int @default, int @new)> charmNotchCosts, Random random)
            {
                foreach (var attribute in fieldInfo.GetCustomAttributes(false))
                {
                    switch (attribute)
                    {
                        case SliderIntElementAttribute attr:
                            Randomize(attr.MinValue, attr.MaxValue, (int)fieldInfo.GetValue(CharmChangerMod.LS), fieldRandomizers, charmNotchCosts, random);
                            break;
                        case InputIntElementAttribute attr:
                            Randomize(attr.MinValue, attr.MaxValue, (int)fieldInfo.GetValue(CharmChangerMod.LS), fieldRandomizers, charmNotchCosts, random);
                            break;
                        case SliderFloatElementAttribute attr:
                            Randomize(attr.MinValue, attr.MaxValue, (float)fieldInfo.GetValue(CharmChangerMod.LS), fieldRandomizers, charmNotchCosts, random);
                            break;
                        case InputFloatElementAttribute attr:
                            Randomize(attr.MinValue, attr.MaxValue, (float)fieldInfo.GetValue(CharmChangerMod.LS), fieldRandomizers, charmNotchCosts, random);
                            break;
                        case BoolElementAttribute:
                            var r = SampleGaussian(random, 0, 1);
                            fieldInfo.SetValue(CharmChangerMod.LS, r < 0);

                            Value = r > 0 ? 1 : 0;
                            break;
                        default:
                            break;
                    }
                }

                IsRandomized = true;
            }

            public void SetValue<T>(T value)
            {
                Value = Convert.ToDouble(value);
                SetValue<T>();
                IsRandomized = true;
            }

            public T GetValue<T>()
            {
                return (T)Convert.ChangeType(fieldInfo.GetValue(CharmChangerMod.LS), typeof(T));
            }

            private void SetValue<T>()
            {
                fieldInfo.SetValue(CharmChangerMod.LS, (T)Convert.ChangeType(Value, typeof(T)));
            }

            private void Randomize<T>(double minValue, double maxValue, T value, Dictionary<string, FieldRandomizer> fieldRandomizers, Dictionary<Charms, (int @default, int @new)> charmNotchCosts, Random random)
            {
                Value = Convert.ToDouble(value);

                if (!IsRandomizable)
                {
                    return;
                }

                FieldRandomizer referenceRandomizer = null;

                if (
                    Relation.reference != null &&
                    fieldRandomizers.TryGetValue(Relation.reference, out referenceRandomizer)
                )
                {
                    if (!referenceRandomizer.IsRandomized)
                    {
                        referenceRandomizer.Randomize(fieldRandomizers, charmNotchCosts, random);
                    }

                }

                var r = SampleGaussian(random, 0, 1);

                if (Settings.RelationalLogic == CharmChangerRandoSettings.ERelationalLogic.ScaleToCharmCost)
                {
                    (int defaultCost, int newCost) = charmNotchCosts[Relation.charm];

                    if (Relation.positive)
                    {
                        if (Relation.reference != null && Relation.reference.StartsWith("regular") && referenceRandomizer != null)
                        {
                            minValue = referenceRandomizer.Value;
                        }

                        if (newCost > defaultCost)
                        {
                            Value = maxValue - (12.0 - newCost) / (12.0 - defaultCost) * (maxValue - Value);
                        }
                        else
                        {
                            Value -= (defaultCost - newCost) * (Value - minValue) / (defaultCost + .5);
                        }
                    }
                    else
                    {
                        if (Relation.reference != null && Relation.reference.StartsWith("regular") && referenceRandomizer != null)
                        {
                            maxValue = referenceRandomizer.Value;
                        }

                        if (newCost > defaultCost)
                        {
                            Value = minValue + (12.0 - newCost) / (12.0 - defaultCost) * (Value - minValue);
                        }
                        else
                        {
                            Value += (defaultCost - newCost) * (maxValue - Value) / (defaultCost + .5);
                        }
                    }
                }

                if (
                    Settings.RelationalLogic == CharmChangerRandoSettings.ERelationalLogic.NoStatDecrease &&
                    referenceRandomizer != null
                )
                {

                    Value = referenceRandomizer.Value;

                    r = Math.Abs(r) * (Relation.positive ? 1 : -1);
                }


                Value = ScaleGaussian(r, minValue, maxValue, Convert.ToDouble(Value));

                if (typeof(T) == typeof(int))
                {
                    Value = Math.Round(Value);
                }

                SetValue<T>();
            }
        }

        public override void Initialize()
        {
            ConnectionMenu.Setup();

            _hook = new Hook(RandoControllerRun, (Action<RandoController> orig, RandoController self) =>
            {
                // Reset everything first, just in case
                foreach (var methodInfo in typeof(LocalSettings).GetMethods())
                {
                    if (methodInfo.Name.StartsWith("Reset"))
                    {
                        methodInfo.Invoke(CharmChangerMod.LS, null);
                    }
                }

                orig(self);

                if (!Settings.Enabled)
                {
                    return;
                }

                Random random = new(self.gs.Seed);
                var fieldRandomizers = typeof(LocalSettings).GetFields().ToDictionary(e => e.Name, e => new FieldRandomizer(e));

                Dictionary<Charms, (int @default, int @new)> charmNotchCosts = [];

                foreach (var (name, fieldRandomizer) in fieldRandomizers)
                {
                    if (name.StartsWith("charm"))
                    {
                        var match = charmNotchCostRegex.Match(name);
                        if (match.Success)
                        {
                            var charmIndex = int.Parse(match.Groups[1].Value) - 1;
                            var defaultCost = fieldRandomizer.GetValue<int>();
                            var newCost = defaultCost;

                            if (self.gs.MiscSettings.RandomizeNotchCosts)
                            {
                                newCost = self.ctx.notchCosts[charmIndex];
                                fieldRandomizer.SetValue(newCost);
                            }

                            charmNotchCosts.Add((Charms)charmIndex, (defaultCost, newCost));

                            continue;
                        }
                    }

                    fieldRandomizer.Randomize(fieldRandomizers, charmNotchCosts, random);
                }
            });
        }

        private static double SampleGaussian(Random random, double mean, double stddev)
        {
            double x1 = 1 - random.NextDouble();
            double x2 = 1 - random.NextDouble();

            double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
            return y1 * stddev + mean;
        }

        private static double ScaleGaussian(double r, double min, double max, double mean)
        {

            r = Math.Min(Math.Max(r, -Math.PI), Math.PI) / Math.PI / Settings.RandomizationShrinking;

            if (r > 0)
            {
                return (max - mean) * r + mean;
            }

            if (r < 0)
            {
                return (mean - min) * r + mean;
            }

            return mean;
        }

        public void OnLoadGlobal(GlobalSettings gs)
        {
            GS = gs;
        }

        public GlobalSettings OnSaveGlobal()
        {
            return GS;
        }
    }
}
