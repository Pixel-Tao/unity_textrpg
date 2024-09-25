using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Utils
{
    public static class Util
    {
        public static long GenerateUid()
        {
            return DateTime.Now.Ticks;
        }
        public static string CreatureTypeToString(Defines.CreatureType type)
        {
            switch (type)
            {
                case Defines.CreatureType.Hero:
                    return "플레이어";
                case Defines.CreatureType.Monster:
                    return "몬스터";
                case Defines.CreatureType.NPC:
                    return "NPC";
                default:
                    return "알 수 없음";
            }
        }
        public static string HeroTypeToString(Defines.JobType type)
        {
            switch (type)
            {
                case Defines.JobType.Warrior:
                    return "전사";
                case Defines.JobType.Archer:
                    return "궁수";
                case Defines.JobType.Mage:
                    return "마법사";
                case Defines.JobType.Thief:
                    return "도적";
                default:
                    return "알 수 없음";
            }
        }
        public static string EquipmentTypeToString(Defines.EquipmentType type)
        {
            switch (type)
            {
                case Defines.EquipmentType.Weapon:
                    return "주무기";
                case Defines.EquipmentType.SubWeapon:
                    return "보조무기";
                case Defines.EquipmentType.Armor:
                    return "방어구";
                case Defines.EquipmentType.Accessory:
                    return "장신구";
                default:
                    return "알 수 없음";
            }
        }
        public static string ConsumableTypeToString(Defines.ConsumableType type)
        {
            switch (type)
            {
                case Defines.ConsumableType.Heal:
                    return "생명력 회복";
                case Defines.ConsumableType.MaxHpBuff:
                    return "최대 생명력 증가 버프";
                case Defines.ConsumableType.AttackBuff:
                    return "공격력 증가 버프";
                case Defines.ConsumableType.DefenseBuff:
                    return "방어력 증가 버프";
                case Defines.ConsumableType.SpeedBuff:
                    return "속도 증가 버프";
                default:
                    return "알 수 없음";
            }
        }
        public static string MenuTypeToString(Defines.MenuType type)
        {
            switch (type)
            {
                case Defines.MenuType.Buy:
                    return "구매";
                case Defines.MenuType.Sell:
                    return "판매";
                case Defines.MenuType.Rest:
                    return "휴식";
                case Defines.MenuType.Quest:
                    return "퀘스트";
                case Defines.MenuType.Exit:
                    return "나가기";
                case Defines.MenuType.BuyItem:
                    return "사기";
                case Defines.MenuType.SellItem:
                    return "팔기";
                case Defines.MenuType.Next:
                    return "다음";
                case Defines.MenuType.Prev:
                    return "이전";
                case Defines.MenuType.None:
                    return "없음";
                case Defines.MenuType.GameStart:
                    return "게임 시작";
                case Defines.MenuType.GameLoad:
                    return "게임 로드";
                case Defines.MenuType.GameExit:
                    return "게임 종료";
                case Defines.MenuType.Attack:
                    return "공격";
                case Defines.MenuType.Battle:
                    return "전투";
                case Defines.MenuType.BattleEscape:
                    return "도망";
                case Defines.MenuType.Equip:
                    return "장비 장착";
                case Defines.MenuType.Inventory:
                    return "인벤토리";
                case Defines.MenuType.Skill:
                    return "스킬";
                case Defines.MenuType.Status:
                    return "능력치";
                case Defines.MenuType.GameSave:
                    return "게임 저장";
                case Defines.MenuType.Unequip:
                    return "장비 해제";
                case Defines.MenuType.Equipment:
                    return "장비";
                case Defines.MenuType.Back:
                    return "뒤로";
                case Defines.MenuType.Use:
                    return "선택";
                case Defines.MenuType.Drop:
                    return "버리기";
                case Defines.MenuType.UseOrEquip:
                    return "사용/장착";
                case Defines.MenuType.BattleItem:
                    return "아이템";
                case Defines.MenuType.BattleAttack:
                    return "공격";
                case Defines.MenuType.BattleSkill:
                    return "스킬";
                case Defines.MenuType.Sort:
                    return "정렬";
                case Defines.MenuType.Cheat:
                    return "르탄이의 축복";
                case Defines.MenuType.Recall:
                    return "귀환";
                default:
                    return type.ToString();
            }
        }
        public static string MapTypeToString(Defines.MapType type)
        {
            switch (type)
            {
                case Defines.MapType.NewbieTown:
                    return "초보자 마을";
                case Defines.MapType.MidTown:
                    return "중급자 마을";
                case Defines.MapType.HighTown:
                    return "상급자 마을";
                case Defines.MapType.ForestField:
                    return "초보자 숲";
                case Defines.MapType.DesertField:
                    return "사막 지역";
                case Defines.MapType.SnowField:
                    return "빙하 지역";
                case Defines.MapType.CaveDungeon:
                    return "동굴 던전";
                case Defines.MapType.RuinDungeon:
                    return "폐허 던전";
                case Defines.MapType.TowerDungeon:
                    return "타워 던전";
                default:
                    return "알 수 없음";
            }
        }
        public static string SkillTypeToString(Defines.SkillType type)
        {
            switch (type)
            {
                case Defines.SkillType.Attack:
                    return "공격";
                case Defines.SkillType.AOE:
                    return "범위 공격";
                case Defines.SkillType.Heal:
                    return "회복";
                case Defines.SkillType.AttackBuff:
                    return "공격력 버프";
                case Defines.SkillType.DefenseBuff:
                    return "방어력 버프";
                case Defines.SkillType.HealBuff:
                    return "회복 버프";
                default:
                    return "알 수 없음";
            }
        }

        public static bool CanMoveToTile(Defines.TileType type)
        {
            switch (type)
            {
                case Defines.TileType.Ground:
                case Defines.TileType.RecallPoint:
                case Defines.TileType.Exit1:
                case Defines.TileType.Exit2:
                case Defines.TileType.Exit3:
                case Defines.TileType.Exit4:
                case Defines.TileType.Enter1:
                case Defines.TileType.Enter2:
                case Defines.TileType.Enter3:
                case Defines.TileType.Enter4:
                case Defines.TileType.ItemShopEvent:
                case Defines.TileType.WeaponShopEvent:
                case Defines.TileType.ArmorShopEvent:
                case Defines.TileType.AccessoryShopEvent:
                case Defines.TileType.InnEvent:
                    return true;

                default:
                    return false;
            }
        }

        // 문자열의 콘솔 출력 폭을 계산하는 함수
        public static int GetStringWidth(string str)
        {
            int width = 0;
            foreach (char c in str)
            {
                if (IsFullWidth(c))
                    width += 2;
                else
                    width += 1;
            }
            return width;
        }

        // 문자가 전각 문자(폭이 2인지)인지 확인하는 함수
        public static bool IsFullWidth(char c)
        {
            // 유니코드 범위를 기반으로 판단
            int codePoint = (int)c;
            return (codePoint >= 0x1100 && (
                codePoint <= 0x115F || // Hangul Jamo
                codePoint == 0x2329 || codePoint == 0x232A ||
                (codePoint >= 0x2E80 && codePoint <= 0xA4CF && codePoint != 0x303F) ||
                (codePoint >= 0xAC00 && codePoint <= 0xD7A3) || // Hangul Syllables
                (codePoint >= 0xF900 && codePoint <= 0xFAFF) || // CJK Compatibility Ideographs
                (codePoint >= 0xFE10 && codePoint <= 0xFE19) || // Vertical forms
                (codePoint >= 0xFE30 && codePoint <= 0xFE6F) || // CJK Compatibility Forms
                (codePoint >= 0xFF00 && codePoint <= 0xFF60) || // Fullwidth Forms
                (codePoint >= 0xFFE0 && codePoint <= 0xFFE6)));
        }

        public static int GrowthValue(int level, int maxLevel, double growthRate = 1.2, double baseExp = 100)
        {
            return (int)(baseExp * Math.Pow(growthRate, level));
        }

        public static string KeyString(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: return "↑";
                case ConsoleKey.DownArrow: return "↓";
                case ConsoleKey.LeftArrow: return "←";
                case ConsoleKey.RightArrow: return "→";
                default: return key.ToString();
            }
        }
    }
}
