using MenuChanger.Attributes;

namespace CharmChangerRando
{
    public class CharmChangerRandoSettings
    {
        [MenuRange(1f, 5f)]
        public float RandomizationShrinking { get; set; } = 1.1f;

        public bool ExcludeRegularStats { get; set; } = true;

        public bool NoStatDecrease { get; set; } = false;
        public bool Enabled { get; set; } = true;

    }
}
