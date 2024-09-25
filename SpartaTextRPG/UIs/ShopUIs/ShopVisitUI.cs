using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.ShopUIs
{
    public class ShopVisitUI : UIBase
    {
        public ShopVisitUI(CreatureBase owner) : base(owner)
        {
        }

        public override Defines.MenuType[] Menus => [
            Defines.MenuType.Buy,
            Defines.MenuType.Sell,
            Defines.MenuType.Exit
        ];

        public ShopBuyUI BuyUI { get; set; }
        public ShopSellUI SellUI { get; set; }

        public override void Show(CreatureBase? visitor)
        {
            if (visitor == null) return;

            Npc npc = (Npc)Owner;
            string message = npc.Message;
            while (true)
            {
                TextManager.Flush();
                TextManager.MenuWriteLine(message);

                ShowAllMenus();

                ConsoleKey key = InputKey();
                SelectMenu(key);
                if (key == Defines.ACCEPT_KEY)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Buy:
                            UIManager.Instance.ShowShopBuy(Owner, visitor);
                            return;
                        case Defines.MenuType.Sell:
                            UIManager.Instance.ShowShopSell(Owner, visitor);
                            return;
                        case Defines.MenuType.Exit:
                            TextManager.MenuWriteLine("상점을 나갑니다.");
                            GameManager.Instance.WakeUpWorld();
                            return;
                    }
                }
            }
        }
    }
}
