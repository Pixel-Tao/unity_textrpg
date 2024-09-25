using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Utils
{
    public class Defines
    {
        public enum CreatureType
        {
            None,
            Hero,
            Monster,
            NPC
        }
        public enum JobType
        {
            None,
            Warrior,
            Mage,
            Archer,
            Thief
        }
        public enum ItemType
        {
            None,
            Equipment,
            Consumable,
            Etc,
        }
        public enum EquipmentType
        {
            None,
            Weapon,
            SubWeapon,
            Armor,
            Accessory
        }
        public enum ConsumableType
        {
            None,
            Heal, // 즉시 회복, 버프 X
            HpRegenBuff, // 턴마다 회복, 버프 O
            MaxHpBuff, // 최대 생명력 증가 버프 O
            AttackBuff, // 공격력 증가 버프 O
            DefenseBuff, // 방어력 증가 버프 O
            SpeedBuff // 속도 증가 버프 O
        }
        public enum EtcType
        {
            None,
            Quest,
            Key,
            Material,
            Gold,
        }
        public enum MapType
        {
            None,
            NewbieTown,
            MidTown,
            HighTown,
            ForestField,
            SnowField,
            DesertField,
            CaveDungeon,
            RuinDungeon,
            TowerDungeon,
        }
        public enum NpcType
        {
            None,
            ItemShopNpc,
            ArmorShopNpc,
            WeaponShopNpc,
            InnNpc,
            AccessoryShopNpc,
        }
        public enum SkillType
        {
            None,
            Attack,
            AOE,
            Heal,
            AttackBuff,
            DefenseBuff,
            HealBuff,
        }
        public enum MenuType
        {
            None,
            Buy,
            Sell,
            Rest,
            Quest,
            Exit,
            Next,
            Prev,
            BuyItem,
            SellItem,
            GameStart,
            GameLoad,
            GameExit,
            Attack,
            Skill,
            Inventory,
            Equip,
            Unequip,
            Equipment,
            Status,
            GameSave,
            Battle,
            BattleEscape,
            Back,
            Use,
            Drop,
            UseOrEquip,
            BattleItem,
            BattleAttack,
            BattleSkill,
            Sort,
        }

        public enum MessageType
        {
            None,
            Info,
            Warning,
            Error,
            System,
            Battle,
            Menu,
            MenuSelect,
        }

        public enum TileType
        {
            None,
            Ground = 11,
            RecallPoint = 12,

            ItemShopEvent = 21,
            WeaponShopEvent = 22,
            ArmorShopEvent = 23,
            AccessoryShopEvent = 24,
            InnEvent = 25,

            ItemShopObject = 31,
            WeaponShopObject = 32,
            ArmorShopObject = 33,
            AccessoryShopObject = 34,
            InnObject = 35,

            Enter1 = 41,
            Enter2 = 42,
            Enter3 = 43,
            Enter4 = 44,

            Exit1 = 51,
            Exit2 = 52,
            Exit3 = 53,
            Exit4 = 54,

            Tree = 71,
            Rock = 72,
            Water = 81,
            Wall = 91,
        }

        public const int DEFAULT_INVENTORY_SIZE = 10;
        public const int WARRIOR_INVENTORY_SIZE = 30;
        public const int MAGE_INVENTORY_SIZE = 20;
        public const int ARCHER_INVENTORY_SIZE = 25;
        public const int THIEF_INVENTORY_SIZE = 25;
        public const int THIEF_DOUBLE_ATTACK_RATE = 30; // 두번공격 확률

        public const int CREATURE_MAX_LEVEL = 20;

        public const int MAX_CONSUMABLE_COUNT = 10;
        public const int MAX_ETC_COUNT = 100;


        public const ConsoleKey ACCEPT_KEY = ConsoleKey.Enter;
        public const ConsoleKey CANCEL_KEY = ConsoleKey.Escape;
        public const ConsoleKey RIGHT_KEY = ConsoleKey.RightArrow;
        public const ConsoleKey LEFT_KEY = ConsoleKey.LeftArrow;
        public const ConsoleKey UP_KEY = ConsoleKey.UpArrow;
        public const ConsoleKey DOWN_KEY = ConsoleKey.DownArrow;
    }
}
