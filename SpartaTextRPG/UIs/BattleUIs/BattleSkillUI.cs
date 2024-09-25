using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.BattleUIs
{
    public class BattleSkillUI : UIBase
    {
        public override Defines.MenuType[] Menus =>
        [
            Defines.MenuType.Use,
            Defines.MenuType.Back,
        ];

        public BattleSkillUI(CreatureBase player) : base(player)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            if (Owner == null)
            {
                TextManager.HWriteLine("Owner is null");
                return;
            }

            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();
                ShowSkills(Owner.SkillBook.Skills.ToArray());

                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectSkill(key, Owner.SkillBook.Skills.ToArray());
                if (Defines.ACCEPT_KEY == key)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Use:
                            if (Owner.SkillBook.CanUseSkill(GetSelectedSkillId()) == false)
                            {
                                TextManager.MWriteLine("스킬을 사용할 수 없습니다.");
                                continue;
                            }
                            // selectedSkillIndex 는 SelectSkill 함수에서 선택 했으므로 그냥 return 하면 됨. 
                            return;
                        case Defines.MenuType.Back:
                            selectedSkillIndex = -1;
                            TextManager.Flush();
                            return;
                    }
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    selectedSkillIndex = -1;
                    TextManager.Flush();
                    return;
                }
            }
        }

        public int GetSelectedSkillId()
        {
            Skill? skill = GetSelectedSkill(Owner.SkillBook.Skills.ToArray());
            if (skill == null)
                return 0;

            return skill.DataId;
        }
    }
}
