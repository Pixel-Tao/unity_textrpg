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
            Defines.MenuType.Recall,
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
                        case Defines.MenuType.Recall:
                            // 퀘스트
                            if (TextManager.Confirm("마을로 귀환하시겠습니까?"))
                            {
                                if (GameManager.Instance.Hero == null)
                                {
                                    TextManager.HWriteLine("참조 가능한 플레이어 정보가 없습니다.");
                                    break;
                                }
                                if (GameManager.Instance.Hero.CurrentMapType == GameManager.Instance.SavedRecallPoint)
                                {
                                    TextManager.LWriteLine("이미 마을에 있습니다.");
                                    break;
                                }

                                switch (GameManager.Instance.Hero.CurrentMapType)
                                {
                                    case Defines.MapType.CaveDungeon:
                                    case Defines.MapType.RuinDungeon:
                                    case Defines.MapType.TowerDungeon:
                                        TextManager.LWriteLine("던전에서는 귀환을 할 수 없습니다.");
                                        break;
                                    default:
                                        TextManager.Flush();
                                        TextManager.LWriteLine($"{Util.MapTypeToString(GameManager.Instance.SavedRecallPoint)}(으)로 이동합니다.");
                                        GameManager.Instance.Recall();
                                        return;
                                }
                            }
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
                else if (key == Defines.CANCEL_KEY)
                {
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }
    }
}
