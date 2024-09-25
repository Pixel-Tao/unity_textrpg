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
    public class ShopInnUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.Rest,
            Defines.MenuType.Exit,
        ];

        public ShopInnUI(CreatureBase owner) : base(owner)
        {
        }

        public override void Show(CreatureBase? visitor = null)
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
                        case Defines.MenuType.Rest:
                            if (visitor.Inventory?.Gold < 200)
                            {
                                TextManager.SystemWriteLine("골드가 부족합니다.");
                            }
                            TextManager.Confirm("200G를 지불하고 휴식을 취하시겠습니까?", () =>
                            {
                                visitor.Rest();
                                TextManager.SystemWriteLine($"{visitor.Name}님이 여관에서 휴식을 취했습니다.");
                            });
                            break;
                        case Defines.MenuType.Exit:
                            TextManager.MenuWriteLine("여관을 나갑니다.");
                            GameManager.Instance.WakeUpWorld();
                            return;
                    }
                }
                else if(key == ConsoleKey.Escape)
                {
                    TextManager.MenuWriteLine("여관을 나갑니다.");
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }
    }
}
