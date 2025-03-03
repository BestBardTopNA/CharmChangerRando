using MenuChanger.Attributes;

namespace CharmChangerRando
{
    public class CharmChangerRandoSettings
    {
        public enum ERelationalLogic
        {
            NoLogic,
            NoStatDecrease,
            ScaleToCharmCost,
        }
        

        [MenuRange(1f, 5f)]
        public float RandomizationShrinking { get; set; } = 1.1f;

        public bool ExcludeRegularStats { get; set; } = true;

        public bool Enabled { get; set; } = true;

        public ERelationalLogic RelationalLogic { get; set; } = ERelationalLogic.NoLogic;
    }
}
