using System.Linq;
using MenuChanger;
using RandomizerMod.Menu;
using MenuChanger.Extensions;
using MenuChanger.MenuPanels;
using MenuChanger.MenuElements;
using static RandomizerMod.Localization;

namespace CharmChangerRando
{
    public class ConnectionMenu
    {
        public static ConnectionMenu Instance { get; private set; }
        public static void Setup()
        {
            RandomizerMenuAPI.AddMenuPage(OnRandomizerMenuConstruction, TryGetMenuButton);
            MenuChangerMod.OnExitMainMenu += () => Instance = null;
        }

        public static void OnRandomizerMenuConstruction(MenuPage page)
        {
            Instance = new(page);
        }

        public static bool TryGetMenuButton(MenuPage _, out SmallButton button)
        {
            button = Instance.menuButton;
            return true;
        }

        private readonly MenuPage menuPage;
        private readonly SmallButton menuButton;
        private readonly VerticalItemPanel panel;
        private readonly SmallButton resetDefault;
        private readonly MenuElementFactory<CharmChangerRandoSettings> menuFactory;

        private ConnectionMenu(MenuPage parentPage)
        {
            menuPage = new("CharmCharmgerRando Settings", parentPage);
            menuButton = new(parentPage, Localize("CharmChanger Rando"));
            menuButton.AddHideAndShowEvent(menuPage);

            menuFactory = new(menuPage, CharmChangerRandoMod.GS.CharmChangerRandoSettings);
            Localize(menuFactory);

            resetDefault = new(menuPage, Localize("Reset"));
            resetDefault.OnClick += () => menuFactory.SetMenuValues(new());

            panel = new(menuPage, SpaceParameters.TOP_CENTER_UNDER_TITLE, SpaceParameters.VSPACE_SMALL, true,
                menuFactory.Elements.Cast<IMenuElement>().Append(resetDefault).ToArray()
            );
        }
    }
}
