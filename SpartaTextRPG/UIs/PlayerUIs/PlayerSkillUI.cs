using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.PlayerUIs
{
    public class PlayerSkillUI : UIBase
    {
        public override Defines.MenuType[] Menus =>
        [
            Defines.MenuType.Use,
            Defines.MenuType.Back,
        ];

        public PlayerSkillUI(CreatureBase player) : base(player)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            Hero hero = Owner as Hero;
            if (hero == null)
            {
                TextManager.HWriteLine("Hero is null");
                return;
            }

            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();
                ShowSkills(hero.SkillBook.Skills.ToArray());

                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectSkill(key, hero.SkillBook.Skills.ToArray());
                if (Defines.ACCEPT_KEY == key)
                {
                    Skill skill = GetSelectedSkill(hero.SkillBook.Skills.ToArray());
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Use:
                            // 스킬 사용
                            if (skill == null)
                            {
                                TextManager.MWriteLine("스킬이 없습니다.");
                                continue;
                            }
                            if (skill.SkillType == Defines.SkillType.Attack
                                || skill.SkillType == Defines.SkillType.AOE)
                            {
                                TextManager.InfoWriteLine($"공격 스킬은 전투중에만 사용 가능합니다.");
                                break;
                            }

                            TextManager.Confirm($"{skill.Name} 스킬을 사용하시겠습니까?", () =>
                            {
                                TextManager.InfoWriteLine($"{hero.Name}님이 {skill.Name}을 사용했습니다.");
                                skill.Use([hero]);
                                skill.RemoveCount();
                            });
                            break;
                        case Defines.MenuType.Back:
                            return;
                    }
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    TextManager.Flush();
                    return;
                }
            }
        }
    }
}
