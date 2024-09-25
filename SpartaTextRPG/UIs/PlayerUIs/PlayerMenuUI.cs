using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.PlayerUIs
{
    public class PlayerMenuUI : UIBase
    {
        public override Defines.MenuType[] Menus => new Defines.MenuType[]
        {
            Defines.MenuType.Status,
            Defines.MenuType.Inventory,
            Defines.MenuType.Skill,
            Defines.MenuType.Quest,
            Defines.MenuType.GameSave,
            Defines.MenuType.GameExit,
        };

        public PlayerMenuUI(CreatureBase player) : base(player)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();

                ConsoleKey key = InputKey();
                if (key == Defines.ACCEPT_KEY)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Status:
                            // 상태창
                            UIManager.Instance.ShowPlayerStatusJob(Owner);
                            break;
                        case Defines.MenuType.Inventory:
                            // 인벤토리
                            UIManager.Instance.ShowPlayerInventoryJob(Owner);
                            break;
                        case Defines.MenuType.Skill:
                            // 스킬
                            UIManager.Instance.ShowPlayerSkillJob(Owner);
                            break;
                        case Defines.MenuType.Quest:
                            // 퀘스트
                            UIManager.Instance.ShowPlayerQuestJob(Owner);
                            break;
                        case Defines.MenuType.GameSave:
                            // 게임 저장
                            UIManager.Instance.ShowGameSaveJob(Owner);
                            break;
                        case Defines.MenuType.GameExit:
                            // 게임 종료
                            GameManager.Instance.GameExit();
                            return;
                    }
                }
                else if (key == Defines.RIGHT_KEY || key == Defines.LEFT_KEY)
                {
                    SelectMenu(key);
                }
                else if(key == Defines.CANCEL_KEY)
                {
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }
    }
}
