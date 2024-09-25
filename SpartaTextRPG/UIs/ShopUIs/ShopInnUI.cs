using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
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
            Defines.MenuType.Cheat,
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
                                TextManager.LWriteLine("골드가 부족합니다.");
                            }
                            TextManager.Confirm("200G를 지불하고 휴식을 취하시겠습니까?", () =>
                            {
                                visitor.Rest();
                                TextManager.LWriteLine($"{visitor.Name}님이 여관에서 휴식을 취했습니다.");
                            });
                            break;
                        case Defines.MenuType.Exit:
                            TextManager.MenuWriteLine("여관을 나갑니다.");
                            GameManager.Instance.WakeUpWorld();
                            return;
                        case Defines.MenuType.Cheat:
                            TextManager.Confirm("정말로 르탄이의 축복을 받으시겠습니까?", () =>
                            {
                                TextManager.Confirm("그냥 가셔도 됩니다.", null, () =>
                                {
                                    TextManager.Confirm("이 선택은 돌이킬 수 없습니다.", () =>
                                    {
                                        Cheat(visitor);
                                    });
                                });
                            });
                            break;
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    TextManager.MenuWriteLine("여관을 나갑니다.");
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }

        private void Cheat(CreatureBase visitor)
        {
            TextManager.HWriteLine("***** 르탄이가 축복을 내립니다. *****");
            visitor.SetLevel(Defines.CREATURE_MAX_LEVEL);
            visitor.SetDefaultStat(9999, 999, 200, 200);
            visitor.OnHealed(visitor.MaxHp);
            foreach (Skill skill in visitor.SkillBook.Skills)
                skill.AddCount(99);
            if (visitor.Inventory?.Gold < 1000000)
                visitor.Inventory?.AddGold(1000000);
            DataManager.Instance.LoadAllItems();
            GameManager.Instance.EnterdMap?.LoadNpc();
            TextManager.HWriteLine("이 일로 저는 무슨일이 벌어져도 아무런 책임지지 않겠습니다.");
        }
    }
}
