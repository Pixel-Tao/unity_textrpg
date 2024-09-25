using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.GameUIs
{
    public class GameTitleUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.GameStart,
            Defines.MenuType.GameLoad,
            Defines.MenuType.GameExit
        ];

        public override void Show(CreatureBase? visitor = null)
        {
            TextManager.SystemWriteLine("이세계 텍스트 RPG에 오신 것을 환영합니다.");
            TextManager.SystemWriteLine("이세계 텍스트 RPG는 콘솔 환경에서 동작하는 텍스트 기반 RPG 게임입니다.");
            TextManager.SystemWriteLine("메뉴를 선택해주세요.");

            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();

                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                SelectMenu(key);

                if (key == Defines.ACCEPT_KEY)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.GameStart:
                            UIManager.Instance.GameStart();
                            return;
                        case Defines.MenuType.GameLoad:
                            UIManager.Instance.GameLoad();
                            return;
                        case Defines.MenuType.GameExit:
                            UIManager.Instance.GameExit();
                            return;
                    }
                }
            }
        }
    }
}
