using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.UIs.PlayerUIs
{
    public class PlayerStatusUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.Status,
            Defines.MenuType.Equipment,
            Defines.MenuType.Back,
        ];

        public PlayerStatusUI(CreatureBase player) : base(player)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            Hero hero = Owner as Hero;
            if (hero == null)
            {
                TextManager.ErrorWriteLine("Hero is null");
                return;
            }

            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();
                switch (Menus[selectedMenuIndex])
                {
                    case Defines.MenuType.Status:
                        WriteStatus(hero);
                        break;
                    case Defines.MenuType.Equipment:
                        WriteEquipment(hero);
                        break;
                }

                ConsoleKey key = InputKey();
                SelectMenu(key);
                if (Defines.CANCEL_KEY == key
                    || Defines.ACCEPT_KEY == key && Menus[selectedMenuIndex] == Defines.MenuType.Back)
                {
                    TextManager.Flush();
                    return;
                }
            }
        }

        private void WriteStatus(Hero hero)
        {
            // 스텟정보
            TextManager.InfoWrite("이름: ");
            TextManager.MenuWrite(hero.Name);
            TextManager.InfoWrite("\t| 직업: ");
            TextManager.MenuWrite(Util.HeroTypeToString(hero.JobType));
            TextManager.InfoWrite("\t| 레벨: ");
            TextManager.MenuWriteLine(hero.Level.ToString());

            TextManager.InfoWrite("경험치: ");
            TextManager.MenuWrite(hero.Exp.ToString());
            TextManager.InfoWrite("/");
            TextManager.MenuWriteLine(hero.NextLevelExp.ToString());

            TextManager.InfoWrite("체력: ");
            TextManager.MenuWrite(hero.Hp.ToString());
            TextManager.InfoWrite("/");
            TextManager.MenuWrite(hero.MaxHp.ToString());
            TextManager.MenuWriteLine($" (기본:{hero.DefaultMaxHp} + 추가:{hero.BonusMaxHp} + 직업:{hero.JobMaxHp} + 스킬: {hero.SBuffDefenseValue(hero.DefaultMaxHp)})");

            TextManager.InfoWrite("공격력: ");
            TextManager.MenuWrite(hero.Attack.ToString());
            TextManager.MenuWriteLine($" (기본:{hero.DefaultAttack} + 추가:{hero.BonusAttack} + 직업:{hero.JobAttack} + 스킬: {hero.SBuffAttackValue(hero.DefaultAttack)})");

            TextManager.InfoWrite("방어력: ");
            TextManager.MenuWrite(hero.Defense.ToString());
            TextManager.MenuWriteLine($" (기본:{hero.DefaultDefense} + 추가:{hero.BonusDefense} + 직업:{hero.JobDefense} + 스킬: {hero.SBuffDefenseValue(hero.DefaultDefense)})");

            TextManager.InfoWrite("속도: ");
            TextManager.MenuWrite(hero.Speed.ToString());
            TextManager.MenuWriteLine($" (기본:{hero.DefaultSpeed} + 추가:{hero.BonusSpeed} + 직업:{hero.JobSpeed} + 스킬: {hero.SBuffAttackValue(hero.DefaultSpeed)})");

            string consume = "효과 없음";
            if (hero.CBuff != null)
                consume = $"{hero.CBuff.Name} 효과 ({hero.CBuff.Value} 만큼 {Util.ConsumableTypeToString(hero.CBuff.ConsumableType)} 상승)";

            TextManager.InfoWrite($"물약 버프: ");
            TextManager.MenuWriteLine(consume);

            string skill = "효과 없음";
            if (hero.SBuff != null)
            {
                skill = $"전투중에만 {Util.SkillTypeToString(hero.SBuff.SkillType)} 효과";
                skill += $" ({hero.SBuff.Description})";
            }
            TextManager.InfoWrite($"스킬 버프: ");
            TextManager.MenuWriteLine(skill);
        }
        private void WriteEquipment(Hero hero)
        {
            // 착용중인 장비
            string emptyText = "장비 없음";
            TextManager.InfoWrite($"착용 무기: ");
            TextManager.MenuWriteLine(hero.EWeapon?.Name ?? emptyText);

            TextManager.InfoWrite($"착용 보조무기(방패): ");
            TextManager.MenuWriteLine(hero.ESubWeapon?.Name ?? emptyText);

            TextManager.InfoWrite($"착용 방어구: ");
            TextManager.MenuWriteLine(hero.EArmor?.Name ?? emptyText);

            TextManager.InfoWrite($"착용 악세서리: ");
            TextManager.MenuWriteLine(hero.EAccessory?.Name ?? emptyText);
        }
    }
}
