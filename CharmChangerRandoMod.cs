using System;
using Modding;
using CharmChanger;
using RandomizerMod.RC;
using System.Reflection;
using MonoMod.RuntimeDetour;
using System.Text.RegularExpressions;

namespace CharmChangerRando
{
    public class CharmChangerRandoMod(): Mod(name: "CharmChangerRando"), IGlobalSettings<GlobalSettings>
    {
        private static readonly MethodInfo RandoControllerRun = typeof(RandoController).GetMethod(nameof(RandoController.Run));
        private static readonly Regex charmNotchCostRegex = new(@"charm(\d+)NotchCost");
        public static GlobalSettings GS { get; private set; } = new();
        private static CharmChangerRandoSettings Settings => GS.CharmChangerRandoSettings;

        private Hook _hook;

        public override string GetVersion() => "v1";

        // This is in another function now, so it can exit after the first valid attribute was found
        private void ProcessField(FieldInfo fieldinfo, Random random)
        {
            foreach (var attribute in fieldinfo.GetCustomAttributes(false))
            {
                var r = SampleGaussian(random, 0, 1);
                switch (attribute)
                {
                    case SliderIntElementAttribute attr:
                        fieldinfo.SetValue(CharmChangerMod.LS, (int)ScaleGaussian(r, attr.MinValue, attr.MaxValue, (int)fieldinfo.GetValue(CharmChangerMod.LS)));
                        return;
                    case InputIntElementAttribute attr:
                        fieldinfo.SetValue(CharmChangerMod.LS, (int)ScaleGaussian(r, attr.MinValue, attr.MaxValue, (int)fieldinfo.GetValue(CharmChangerMod.LS)));
                        return;
                    case SliderFloatElementAttribute attr:
                        fieldinfo.SetValue(CharmChangerMod.LS, (float)ScaleGaussian(r, attr.MinValue, attr.MaxValue, (float)fieldinfo.GetValue(CharmChangerMod.LS)));
                        return;
                    case InputFloatElementAttribute attr:
                        fieldinfo.SetValue(CharmChangerMod.LS, (float)ScaleGaussian(r, attr.MinValue, attr.MaxValue, (float)fieldinfo.GetValue(CharmChangerMod.LS)));
                        return;
                    case BoolElementAttribute:
                        fieldinfo.SetValue(CharmChangerMod.LS, r < 0);
                        return;
                    default:
                        break;
                }
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

                foreach (var fieldinfo in typeof(LocalSettings).GetFields())                  
                {
                    if (Settings.ExcludeRegularStats && fieldinfo.Name.StartsWith("regular"))
                    {
                        continue;
                    }

                    if (fieldinfo.Name.StartsWith("charm"))
                    {
                        var match = charmNotchCostRegex.Match(fieldinfo.Name);
                        if (match.Success)
                        {
                            if (self.gs.MiscSettings.RandomizeNotchCosts)
                            {
                                fieldinfo.SetValue(CharmChangerMod.LS, self.ctx.notchCosts[int.Parse(match.Groups[1].Value) - 1]);
                            }

                            continue;
                        }
                    }

                    ProcessField(fieldinfo, random);
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