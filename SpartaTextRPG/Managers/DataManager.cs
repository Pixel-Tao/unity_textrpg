using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaTextRPG.Utils.Defines;

namespace SpartaTextRPG.Managers
{
    public class DataManager
    {
        private static DataManager? _instance;
        public static DataManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataManager();

                return _instance;
            }
        }

        public Dictionary<Defines.JobType, JobData> JobDict { get; private set; } = new Dictionary<Defines.JobType, JobData>();
        public Dictionary<int, CreatureStatData> CreatureStatDict { get; private set; } = new Dictionary<int, CreatureStatData>();
        public Dictionary<int, ItemData> ItemDict { get; private set; } = new Dictionary<int, ItemData>();
        public Dictionary<int, NpcData> NpcDict { get; private set; } = new Dictionary<int, NpcData>();
        public Dictionary<int, SkillData> SkillDict { get; private set; } = new Dictionary<int, SkillData>();
        public Dictionary<int, MonsterData> MonsterDict { get; private set; } = new Dictionary<int, MonsterData>();

        public DataManager()
        {
            _instance = this;
        }

        public void LoadData()
        {
            // Load data from files
            LoadCreatureStats();
            LoadConsumableItems();
            LoadEquipmentItems();
            LoadNpcs();
            LoadJobs();
            LoadSkills();
            LoadMonsters();
        }
        // 1~100
        private void LoadCreatureStats()
        {
            // 임시
            CreatureStatDict = new Dictionary<int, CreatureStatData>();
            int maxLevel = Defines.CREATURE_MAX_LEVEL;
            for (int i = 1; i <= maxLevel; i++)
                CreatureStatDict.Add(i, new CreatureStatData { Level = i, MaxHp = i * 10, Attack = i * 2, Defense = i, Speed = i * 5, NextLevelExp = Util.GrowthValue(i, maxLevel) });
        }
        // 101~200
        private void LoadConsumableItems()
        {
            if (ItemDict == null)
                ItemDict = new Dictionary<int, ItemData>();
            ItemDict.Add(101, new ConsumableItemData { DataId = 101, Name = "하급 체력 회복 포션", Description = "체력을 {0}만큼 즉시 회복합니다.", Price = 50, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.Heal, Value = 100 });
            ItemDict.Add(102, new ConsumableItemData { DataId = 102, Name = "중급 체력 회복 포션", Description = "체력을 {0}만큼 즉시 회복합니다.", Price = 200, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.Heal, Value = 300 });
            ItemDict.Add(103, new ConsumableItemData { DataId = 103, Name = "상급 체력 회복 포션", Description = "체력을 {0}만큼 즉시 회복합니다.", Price = 600, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.Heal, Value = 500 });
            ItemDict.Add(104, new ConsumableItemData { DataId = 104, Name = "체력 재생 버프 포션", Description = "전투하는 동안 매턴이 종료시 {0}만큼 체력을 회복합니다.", Price = 500, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.HpRegenBuff, Value = 50 });
            ItemDict.Add(105, new ConsumableItemData { DataId = 105, Name = "공격력 버프 포션", Description = "전투하는 동안 공격력이 {0}만큼 증가합니다.", Price = 2000, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.AttackBuff, Value = 30 });
            ItemDict.Add(106, new ConsumableItemData { DataId = 106, Name = "방어력 버프 포션", Description = "전투하는 동안 방어력이 {0}만큼 증가합니다.", Price = 2000, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.DefenseBuff, Value = 20 });
            ItemDict.Add(107, new ConsumableItemData { DataId = 107, Name = "속도 버프 포션", Description = "전투하는 동안 속도가 {0}만큼 증가합니다.", Price = 2000, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.SpeedBuff, Value = 15 });
            ItemDict.Add(108, new ConsumableItemData { DataId = 108, Name = "최대체력 상승 버프 포션", Description = "전투하는 동안 최대 체력을 {0}만큼 증가합니다.", Price = 2000, Type = Defines.ItemType.Consumable, ConsumableType = Defines.ConsumableType.MaxHpBuff, Value = 500 });
        }
        // 201~1000
        private void LoadEquipmentItems()
        {
            if (ItemDict == null)
                ItemDict = new Dictionary<int, ItemData>();
            // 전사용 아이템
            // 철 검, 철 갑옷, 철 방패, 구리 반지
            ItemDict.Add(201, new EquipmentItemData { DataId = 201, Name = "철 검", Description = "튼튼해 보이는 철제 검", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(202, new EquipmentItemData { DataId = 202, Name = "철 갑옷", Description = "튼튼해 보이는 철제 갑옷", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 10, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(203, new EquipmentItemData { DataId = 203, Name = "철 방패", Description = "튼튼해 보이는 방패", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 0, Defense = 5, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(204, new EquipmentItemData { DataId = 204, Name = "구리 반지", Description = "저렴해 보이는 구리 반지", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 5, Speed = 5, RequiredLevel = 1, HeroType = Defines.JobType.Warrior });
            // 미스릴 검, 미스릴 갑옷, 미스릴 방패, 은 반지
            ItemDict.Add(205, new EquipmentItemData { DataId = 205, Name = "미스릴 검", Description = "화려해 보이는 미스릴 검", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 50, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(206, new EquipmentItemData { DataId = 206, Name = "미스릴 갑옷", Description = "화려해 보이는 미스릴 갑옷", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 50, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(207, new EquipmentItemData { DataId = 207, Name = "미스릴 방패", Description = "화려해 보이는 미스릴 방패", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 0, Defense = 25, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(208, new EquipmentItemData { DataId = 208, Name = "은 반지", Description = "흔해 보이는 은 반지", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 25, Speed = 25, RequiredLevel = 5, HeroType = Defines.JobType.Warrior });
            // 다이아몬드 검, 다이아몬드 갑옷, 다이아몬드 방패, 금 반지
            ItemDict.Add(209, new EquipmentItemData { DataId = 209, Name = "다이아몬드 검", Description = "영롱해 보이는 다이아몬드 검", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 100, Defense = 0, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(210, new EquipmentItemData { DataId = 210, Name = "다이아몬드 갑옷", Description = "영롱해 보이는 다이아몬드 갑옷", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 100, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(211, new EquipmentItemData { DataId = 211, Name = "다이아몬드 방패", Description = "영롱해 보이는 다이아몬드 방패", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 0, Defense = 50, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Warrior });
            ItemDict.Add(212, new EquipmentItemData { DataId = 212, Name = "금 반지", Description = "비싸 보이는 반지", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 25, Speed = 50, RequiredLevel = 10, HeroType = Defines.JobType.Warrior });

            // 궁사용 아이템
            // 나무 활, 가죽 갑옷, 가죽 활통, 나무 반지
            ItemDict.Add(301, new EquipmentItemData { DataId = 301, Name = "나무 활", Description = "흔한 나무 활", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 15, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Archer });
            ItemDict.Add(302, new EquipmentItemData { DataId = 302, Name = "낡은 가죽 갑옷", Description = "흔한 낡은 가죽 갑옷", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 5, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Archer });
            ItemDict.Add(303, new EquipmentItemData { DataId = 303, Name = "낡은 가죽 활통", Description = "흔한 낡은 가죽 활통", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Archer });
            ItemDict.Add(304, new EquipmentItemData { DataId = 304, Name = "나무 반지", Description = "흔한 나무 반지", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 0, Speed = 10, RequiredLevel = 1, HeroType = Defines.JobType.Archer });

            // 강철 활, 튼튼한 가죽 갑옷, 튼튼한 가죽 활통, 거목 반지
            ItemDict.Add(305, new EquipmentItemData { DataId = 305, Name = "강철 활", Description = "튼튼한 강철 활", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 60, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Archer });
            ItemDict.Add(306, new EquipmentItemData { DataId = 306, Name = "튼튼한 가죽 갑옷", Description = "튼튼한 가죽 갑옷", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 10, Defense = 40, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Archer });
            ItemDict.Add(307, new EquipmentItemData { DataId = 307, Name = "튼튼한 가죽 활통", Description = "튼튼한 가죽 활통", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 25, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Archer });
            ItemDict.Add(308, new EquipmentItemData { DataId = 308, Name = "거목 반지", Description = "흔한 거목 반지", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 25, Defense = 0, Speed = 25, RequiredLevel = 5, HeroType = Defines.JobType.Archer });

            // 오리하르콘 활, 축복받은 가죽 갑옷, 축복받은 가죽 활통, 신수 반지
            ItemDict.Add(309, new EquipmentItemData { DataId = 309, Name = "오리하르콘 활", Description = "화려한 오리하르콘 활", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 150, Defense = 0, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Archer });
            ItemDict.Add(310, new EquipmentItemData { DataId = 310, Name = "축복받은 가죽 갑옷", Description = "축복받은 가죽 갑옷", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 70, Speed = 10, RequiredLevel = 10, HeroType = Defines.JobType.Archer });
            ItemDict.Add(311, new EquipmentItemData { DataId = 311, Name = "축복받은 가죽 활통", Description = "축복받은 가죽 활통", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 30, Defense = 0, Speed = 20, RequiredLevel = 10, HeroType = Defines.JobType.Archer });
            ItemDict.Add(312, new EquipmentItemData { DataId = 312, Name = "신수 반지", Description = "흔한 신수 반지", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 50, Defense = 0, Speed = 50, RequiredLevel = 10, HeroType = Defines.JobType.Archer });

            // 마법사용 아이템
            // 나무 완드, 린넨 로브, 초급 마법서, 수정 반지
            ItemDict.Add(401, new EquipmentItemData { DataId = 401, Name = "나무 완드", Description = "흔한 나무 완드", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Mage });
            ItemDict.Add(402, new EquipmentItemData { DataId = 402, Name = "린넨 로브", Description = "흔한 린넨 로브", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 2, Defense = 3, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Mage });
            ItemDict.Add(403, new EquipmentItemData { DataId = 403, Name = "초급 마법서", Description = "흔한 초급 마법서", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Mage });
            ItemDict.Add(404, new EquipmentItemData { DataId = 404, Name = "수정 반지", Description = "흔한 수정 반지", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 10, Defense = 0, Speed = 10, RequiredLevel = 1, HeroType = Defines.JobType.Mage });

            // 루비 완드, 마법사 로브, 중급 마법서, 가넷 반지
            ItemDict.Add(405, new EquipmentItemData { DataId = 405, Name = "루비 완드", Description = "화려한 루비 완드", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 50, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Mage });
            ItemDict.Add(406, new EquipmentItemData { DataId = 406, Name = "마법사 로브", Description = "화려한 마법사 로브", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 15, Defense = 35, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Mage });
            ItemDict.Add(407, new EquipmentItemData { DataId = 407, Name = "중급 마법서", Description = "화려한 중급 마법서", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 25, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Mage });
            ItemDict.Add(408, new EquipmentItemData { DataId = 408, Name = "가넷 반지", Description = "화려한 가넷 반지", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 25, Defense = 0, Speed = 10, RequiredLevel = 5, HeroType = Defines.JobType.Mage });

            // 에메랄드 완드, 대마법사 로브, 고급 마법서, 사파이어 반지
            ItemDict.Add(409, new EquipmentItemData { DataId = 409, Name = "에메랄드 완드", Description = "영롱한 에메랄드 완드", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 100, Defense = 0, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Mage });
            ItemDict.Add(410, new EquipmentItemData { DataId = 410, Name = "대마법사 로브", Description = "영롱한 대마법사 로브", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 30, Defense = 60, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Mage });
            ItemDict.Add(411, new EquipmentItemData { DataId = 411, Name = "고급 마법서", Description = "영롱한 고급 마법서", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 50, Defense = 0, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Mage });
            ItemDict.Add(412, new EquipmentItemData { DataId = 412, Name = "사파이어 반지", Description = "영롱한 사파이어 반지", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 70, Defense = 0, Speed = 20, RequiredLevel = 10, HeroType = Defines.JobType.Mage });

            // 도적용 아이템
            // 녹슨 단검, 검은 가죽 갑옷, 녹슨 보조 단검, 검은 가죽 팔찌
            ItemDict.Add(501, new EquipmentItemData { DataId = 501, Name = "녹슨 단검", Description = "녹슨 단검", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Thief });
            ItemDict.Add(502, new EquipmentItemData { DataId = 502, Name = "검은 가죽 갑옷", Description = "검은 가죽 갑옷", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 4, Speed = 2, RequiredLevel = 1, HeroType = Defines.JobType.Thief });
            ItemDict.Add(503, new EquipmentItemData { DataId = 503, Name = "녹슨 보조 단검", Description = "녹슨 보조 단검", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 10, Defense = 0, Speed = 0, RequiredLevel = 1, HeroType = Defines.JobType.Thief });
            ItemDict.Add(504, new EquipmentItemData { DataId = 504, Name = "검은 가죽 팔찌", Description = "검은 가죽 팔찌", Price = 100, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 0, Speed = 10, RequiredLevel = 1, HeroType = Defines.JobType.Thief });

            // 강철 단검, 강화된 가죽 갑옷, 강철 보조 단검, 은 팔찌
            ItemDict.Add(505, new EquipmentItemData { DataId = 505, Name = "강철 단검", Description = "강철 단검", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 50, Defense = 0, Speed = 0, RequiredLevel = 5, HeroType = Defines.JobType.Thief });
            ItemDict.Add(506, new EquipmentItemData { DataId = 506, Name = "강화된 가죽 갑옷", Description = "강화된 가죽 갑옷", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 40, Speed = 10, RequiredLevel = 5, HeroType = Defines.JobType.Thief });
            ItemDict.Add(507, new EquipmentItemData { DataId = 507, Name = "강철 보조 단검", Description = "강철 보조 단검", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 15, Defense = 0, Speed = 15, RequiredLevel = 5, HeroType = Defines.JobType.Thief });
            ItemDict.Add(508, new EquipmentItemData { DataId = 508, Name = "은 팔찌", Description = "은 팔찌", Price = 1000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 0, Defense = 25, Speed = 25, RequiredLevel = 5, HeroType = Defines.JobType.Thief });

            // 암흑 단검, 암흑 가죽 갑옷, 암흑 보조 단검, 금 팔찌
            ItemDict.Add(509, new EquipmentItemData { DataId = 509, Name = "암흑 단검", Description = "암흑 단검", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Weapon, Attack = 100, Defense = 0, Speed = 0, RequiredLevel = 10, HeroType = Defines.JobType.Thief });
            ItemDict.Add(510, new EquipmentItemData { DataId = 510, Name = "암흑 가죽 갑옷", Description = "암흑 가죽 갑옷", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Armor, Attack = 0, Defense = 80, Speed = 20, RequiredLevel = 10, HeroType = Defines.JobType.Thief });
            ItemDict.Add(511, new EquipmentItemData { DataId = 511, Name = "암흑 보조 단검", Description = "암흑 보조 단검", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.SubWeapon, Attack = 25, Defense = 0, Speed = 25, RequiredLevel = 10, HeroType = Defines.JobType.Thief });
            ItemDict.Add(512, new EquipmentItemData { DataId = 512, Name = "금 팔찌", Description = "금 팔찌", Price = 10000, Type = Defines.ItemType.Equipment, EquipmentType = Defines.EquipmentType.Accessory, Attack = 25, Defense = 25, Speed = 50, RequiredLevel = 10, HeroType = Defines.JobType.Thief });
        }
        // 1101~1200
        private void LoadNpcs()
        {
            NpcDict = new Dictionary<int, NpcData>();
            string[] itemMessaages = [
                "싸고 좋은 아이템이 많이 있습니다.",
                "효과가 아주 좋아요.",
                "어서오시오! 골라 보시오!",
                "유용한 아이템을 싸게 판매하고 있습니다.",
                "무엇을 구매하시겠습니까?",
                ];
            string[] innMessaages = ["푹 쉬다 가세요~", "방금 청소를 끝냈어요.", "마침 빈방이 딱 하나가 있네요."];
            int[] items = [101, 104, 105, 106, 107, 108];
            // 잡화 상점
            NpcDict.Add(1101, new NpcData { DataId = 1101, NpcType = NpcType.ItemShopNpc, Name = "초급 물약 상인", Description = "초보자들이 사용하기 좋은 잡화를 판다.", SaleItemIds = items, Messaages = itemMessaages });
            NpcDict.Add(1102, new NpcData { DataId = 1102, NpcType = NpcType.ItemShopNpc, Name = "중급 물약 상인", Description = "중급 모험가들이 사용하기 좋은 잡화를 판다.", SaleItemIds = [101, 102, 104, 105, 106, 107, 108], Messaages = itemMessaages });
            NpcDict.Add(1103, new NpcData { DataId = 1103, NpcType = NpcType.ItemShopNpc, Name = "상급 물약 상인", Description = "상급 모험가들이 사용하기 좋은 잡화를 판다.", SaleItemIds = [101, 102, 103, 104, 105, 106, 107, 108], Messaages = itemMessaages });

            // 무기 상점
            NpcDict.Add(1104, new NpcData { DataId = 1104, NpcType = NpcType.WeaponShopNpc, Name = "초급 무기 상인", Description = "초급 무기를 판매하는 무기 상점", SaleItemIds = [201, 301, 401, 501, 303, 403, 503], Messaages = itemMessaages });
            NpcDict.Add(1105, new NpcData { DataId = 1105, NpcType = NpcType.WeaponShopNpc, Name = "중급 무기 상인", Description = "중급 무기를 판매하는 무기 상점", SaleItemIds = [201, 301, 401, 501, 303, 403, 503, 202, 302, 402, 502, 307, 407, 507], Messaages = itemMessaages });
            NpcDict.Add(1106, new NpcData { DataId = 1106, NpcType = NpcType.WeaponShopNpc, Name = "상급 무기 상인", Description = "상급 무기를 판매하는 무기 상점", SaleItemIds = [201, 301, 401, 501, 303, 403, 503, 202, 302, 402, 502, 307, 407, 507, 203, 303, 403, 503, 311, 411, 511], Messaages = itemMessaages });

            // 방어구 상점
            NpcDict.Add(1107, new NpcData { DataId = 1107, NpcType = NpcType.ArmorShopNpc, Name = "초급 방어구 상인", Description = "초급 방어구를 판매하는 방어구 상점", SaleItemIds = [202, 302, 402, 502, 203], Messaages = itemMessaages });
            NpcDict.Add(1108, new NpcData { DataId = 1108, NpcType = NpcType.ArmorShopNpc, Name = "중급 방어구 상인", Description = "중급 방어구를 판매하는 방어구 상점", SaleItemIds = [202, 302, 402, 502, 203, 303, 403, 503, 207], Messaages = itemMessaages });
            NpcDict.Add(1109, new NpcData { DataId = 1109, NpcType = NpcType.ArmorShopNpc, Name = "상급 방어구 상인", Description = "상급 방어구를 판매하는 방어구 상점", SaleItemIds = [202, 302, 402, 502, 203, 303, 403, 503, 207, 204, 304, 404, 504, 211], Messaages = itemMessaages });

            // 악세사리 상점
            NpcDict.Add(1110, new NpcData { DataId = 1110, NpcType = NpcType.AccessoryShopNpc, Name = "초급 악세사리 상인", Description = "초급 악세사리를 판매하는 악세사리 상점", SaleItemIds = [204, 304, 404, 504], Messaages = itemMessaages });
            NpcDict.Add(1111, new NpcData { DataId = 1111, NpcType = NpcType.AccessoryShopNpc, Name = "중급 악세사리 상인", Description = "중급 악세사리를 판매하는 악세사리 상점", SaleItemIds = [204, 304, 404, 504, 205, 305, 405, 505], Messaages = itemMessaages });
            NpcDict.Add(1112, new NpcData { DataId = 1112, NpcType = NpcType.AccessoryShopNpc, Name = "상급 악세사리 상인", Description = "상급 악세사리를 판매하는 악세사리 상점", SaleItemIds = [204, 304, 404, 504, 205, 305, 405, 505, 206, 306, 406, 506], Messaages = itemMessaages });

            // 여관 Npc
            NpcDict.Add(1113, new NpcData { DataId = 1113, NpcType = NpcType.InnNpc, Name = "여관 주인", Description = "여관 주인", Messaages = innMessaages });
        }
        // enum
        private void LoadJobs()
        {
            JobDict.Add(Defines.JobType.Warrior, new JobData { Name = "전사", JobType = JobType.Warrior, MaxHp = 200, Attack = 20, Defense = 20, Speed = 20, SkillIds = new int[] { 2201, 2202, 2203, 2204, 2205 }, Description = "전사는 높은 체력과 방어력, 더 많은 아이템을 소지할 수 있습니다." });
            JobDict.Add(Defines.JobType.Archer, new JobData { Name = "궁수", JobType = JobType.Archer, MaxHp = 100, Attack = 35, Defense = 10, Speed = 25, SkillIds = new int[] { 2301, 2302, 2303, 2304, 2305 }, Description = "궁수는 높은 공격력과 속도를 가지고 있습니다." });
            JobDict.Add(Defines.JobType.Mage, new JobData { Name = "마법사", JobType = JobType.Mage, MaxHp = 100, Attack = 40, Defense = 10, Speed = 15, SkillIds = new int[] { 2101, 2102, 2103, 2104, 2105 }, Description = "마법사는 높은 공격력과 스킬 사용횟수를 가지고 있습니다." });
            JobDict.Add(Defines.JobType.Thief, new JobData { Name = "도적", JobType = JobType.Thief, MaxHp = 150, Attack = 25, Defense = 15, Speed = 30, SkillIds = new int[] { 2401, 2402, 2403, 2404, 2405 }, Description = "도적은 높은 속도와 높은 확률로 1회 추가 공격을 할 수 있습니다." });
        }
        // 2101~3000
        private void LoadSkills()
        {
            // 마법사
            SkillDict.Add(2101, new SkillData { DataId = 2101, SkillType = SkillType.Attack, Name = "매직 미사일", DamagePerValue = 1.2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 1, DefaultMaxCastCount = 3, Description = "단발성 매직 미사일을 쏘고 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2102, new SkillData { DataId = 2102, SkillType = SkillType.Attack, Name = "파이어 볼", DamagePerValue = 2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 3, DefaultMaxCastCount = 3, Description = "단발성 불속성 공격을 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2103, new SkillData { DataId = 2103, SkillType = SkillType.Heal, Name = "회복", DamagePerValue = 0, HealPerValue = 2, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 5, DefaultMaxCastCount = 3, Description = "상처를 공격력의 {HealPerValue}% 만큼 치유해 줍니다." });
            SkillDict.Add(2104, new SkillData { DataId = 2104, SkillType = SkillType.Attack, Name = "라이트닝", DamagePerValue = 2.5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 7, DefaultMaxCastCount = 2, Description = "번개를 연속으로 {ComboCount}회 쏘아 1회당 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2105, new SkillData { DataId = 2105, SkillType = SkillType.AOE, Name = "파이어 월", TargetCount = 6, DamagePerValue = 3, HealPerValue = 0, BuffPerValue = 0, ComboCount = 5, RequiredLevel = 10, DefaultMaxCastCount = 1, Description = "화염 벽을 깔고 {TargetCount}명의 적에게 연속으로 {ComboCount}회 공격하고 1회당 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });

            // 전사
            SkillDict.Add(2201, new SkillData { DataId = 2201, SkillType = SkillType.Attack, Name = "스매시", DamagePerValue = 1.5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 1, DefaultMaxCastCount = 3, Description = "적을 강하게 때려 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2202, new SkillData { DataId = 2202, SkillType = SkillType.DefenseBuff, Name = "집결 함성", DamagePerValue = 0, HealPerValue = 0, BuffPerValue = 1.5, ComboCount = 1, RequiredLevel = 3, DefaultMaxCastCount = 1, Description = "전투중에 기본 능력치의 {BuffPerValue}% 만큼 방어력과 생명력을 증가 시킵니다." });
            SkillDict.Add(2203, new SkillData { DataId = 2203, SkillType = SkillType.Attack, Name = "피어스", DamagePerValue = 2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 5, DefaultMaxCastCount = 3, Description = "적을 찌르고 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2204, new SkillData { DataId = 2204, SkillType = SkillType.AttackBuff, Name = "전투 함성", DamagePerValue = 0, HealPerValue = 0, BuffPerValue = 1.5, ComboCount = 0, RequiredLevel = 7, DefaultMaxCastCount = 1, Description = "전투중에 기본 능력치의 {BuffPerValue}% 만큼 공격력과 속도를 증가 시킵니다." });
            SkillDict.Add(2205, new SkillData { DataId = 2205, SkillType = SkillType.AOE, Name = "휠윈드", TargetCount = 6, DamagePerValue = 2.5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 3, RequiredLevel = 10, DefaultMaxCastCount = 3, Description = "주변의 적 {TargetCount}명에게 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });

            // 궁수
            SkillDict.Add(2301, new SkillData { DataId = 2301, SkillType = SkillType.AOE, Name = "멀티샷", TargetCount = 3, DamagePerValue = 1.2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 1, DefaultMaxCastCount = 3, Description = "여러개의 화살을 쏴서 {TargetCount}명에게 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2302, new SkillData { DataId = 2302, SkillType = SkillType.Attack, Name = "플레임 화살", DamagePerValue = 2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 3, DefaultMaxCastCount = 3, Description = "화염 화살을 쏘고 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2303, new SkillData { DataId = 2303, SkillType = SkillType.Attack, Name = "포이즌 화살", DamagePerValue = 1.5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 3, RequiredLevel = 5, DefaultMaxCastCount = 3, Description = "독 화살을 쏘고 공격력의 {DamagePerValue}% 만큼 {ComboCount}회 피해를 줍니다." });
            SkillDict.Add(2304, new SkillData { DataId = 2304, SkillType = SkillType.AttackBuff, Name = "명중률 향상", DamagePerValue = 0, HealPerValue = 0, BuffPerValue = 2, ComboCount = 1, RequiredLevel = 7, DefaultMaxCastCount = 1, Description = "일시적으로 명중률을 높여 치명적인 공격을 할 수 있게 합니다. {BuffPerValue}% 만큼 공격력과 속도를 향상시킵니다." });
            SkillDict.Add(2305, new SkillData { DataId = 2305, SkillType = SkillType.Attack, Name = "헤드샷", DamagePerValue = 5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 10, DefaultMaxCastCount = 1, Description = "적의 머리를 조준하여 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });

            // 도적
            SkillDict.Add(2401, new SkillData { DataId = 2401, SkillType = SkillType.Attack, Name = "더블 어택", DamagePerValue = 1, HealPerValue = 0, BuffPerValue = 0, ComboCount = 2, RequiredLevel = 1, DefaultMaxCastCount = 3, Description = "일반 공격을 {ComboCount}회 가합니다. 회당 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2402, new SkillData { DataId = 2402, SkillType = SkillType.Attack, Name = "가속화", DamagePerValue = 0, HealPerValue = 0, BuffPerValue = 1.3, ComboCount = 1, RequiredLevel = 3, DefaultMaxCastCount = 2, Description = "가속화를 사용하여 {BuffPerValue}% 만큼 공격력과 속도를 높입니다." });
            SkillDict.Add(2403, new SkillData { DataId = 2403, SkillType = SkillType.Attack, Name = "트리플 어택", DamagePerValue = 1.2, HealPerValue = 0, BuffPerValue = 0, ComboCount = 3, RequiredLevel = 5, DefaultMaxCastCount = 3, Description = "일반 공격을 {ComboCount}회 가합니다. 회당 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2404, new SkillData { DataId = 2404, SkillType = SkillType.Attack, Name = "투척", DamagePerValue = 2.5, HealPerValue = 0, BuffPerValue = 0, ComboCount = 1, RequiredLevel = 7, DefaultMaxCastCount = 5, Description = "투척 무기를 던져 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
            SkillDict.Add(2405, new SkillData { DataId = 2405, SkillType = SkillType.Attack, Name = "약점 공격", DamagePerValue = 3, HealPerValue = 0, BuffPerValue = 0, ComboCount = 2, RequiredLevel = 10, DefaultMaxCastCount = 2, Description = "적의 약점을 {ComboCount}회 공격하여 1회당 공격력의 {DamagePerValue}% 만큼 피해를 줍니다." });
        }
        // 3101~4000
        private void LoadMonsters()
        {
            MonsterDict = new Dictionary<int, MonsterData>();
            MonsterDict.Add(3101, new MonsterData { DataId = 3101, JobType = JobType.Warrior, MonsterType = MonsterType.Common, Name = "고블린", Description = "못생긴 고블린", Attack = 5, MaxHp = 30, Defense = 0, Speed = 10, Exp = 10, Gold = 1000, SkillIds = [], ItemIds = new int[] { 101 }, ItemDropRate = 0.5f });
            MonsterDict.Add(3102, new MonsterData { DataId = 3102, JobType = JobType.Warrior, MonsterType = MonsterType.Common, Name = "오크", Description = "강한 오크", Attack = 10, MaxHp = 50, Defense = 5, Speed = 5, Exp = 20, Gold = 2000, SkillIds = [], ItemIds = new int[] { 102 }, ItemDropRate = 0.5f });
            MonsterDict.Add(3103, new MonsterData { DataId = 3103, JobType = JobType.Warrior, MonsterType = MonsterType.Common, Name = "트롤", Description = "거대한 트롤", Attack = 15, MaxHp = 100, Defense = 10, Speed = 0, Exp = 30, Gold = 3000, SkillIds = [], ItemIds = new int[] { 103 }, ItemDropRate = 0.5f });

            MonsterDict.Add(3201, new MonsterData { DataId = 3201, JobType = JobType.Archer, MonsterType = MonsterType.Common, Name = "스켈레톤", Description = "뼈만 남은 스켈레톤", Attack = 5, MaxHp = 300, Defense = 0, Speed = 10, Exp = 60, Gold = 1000, SkillIds = [], ItemIds = new int[] { 201 }, ItemDropRate = 0.5f });
            MonsterDict.Add(3202, new MonsterData { DataId = 3202, JobType = JobType.Archer, MonsterType = MonsterType.Common, Name = "좀비", Description = "부활한 좀비", Attack = 10, MaxHp = 500, Defense = 5, Speed = 5, Exp = 80, Gold = 2000, SkillIds = [], ItemIds = new int[] { 202 }, ItemDropRate = 0.5f });

            MonsterDict.Add(3301, new MonsterData { DataId = 3203, JobType = JobType.Mage, MonsterType = MonsterType.Elite, Name = "마녀", Description = "마녀", Attack = 50, MaxHp = 300, Defense = 10, Speed = 30, Exp = 120, Gold = 3000, SkillIds = [], ItemIds = new int[] { 203 }, ItemDropRate = 0.5f });
            MonsterDict.Add(3302, new MonsterData { DataId = 3301, JobType = JobType.Mage, MonsterType = MonsterType.Elite, Name = "스파이더", Description = "거대한 스파이더", Attack = 60, MaxHp = 350, Defense = 30, Speed = 10, Exp = 130, Gold = 1000, SkillIds = [], ItemIds = new int[] { 301 }, ItemDropRate = 0.5f });

            MonsterDict.Add(3401, new MonsterData { DataId = 3401, JobType = JobType.Thief, MonsterType = MonsterType.Elite, Name = "원숭이", Description = "재빠른 원숭이", Attack = 30, MaxHp = 600, Defense = 15, Speed = 50, Exp = 140, Gold = 2000, SkillIds = [], ItemIds = new int[] { 401 }, ItemDropRate = 0.5f });
            MonsterDict.Add(3402, new MonsterData { DataId = 3402, JobType = JobType.Thief, MonsterType = MonsterType.Elite, Name = "도둑", Description = "도둑", Attack = 45, MaxHp = 700, Defense = 20, Speed = 55, Exp = 150, Gold = 3000, SkillIds = [], ItemIds = new int[] { 402 }, ItemDropRate = 0.5f });

            MonsterDict.Add(3501, new MonsterData { DataId = 3501, JobType = JobType.Mage, MonsterType = MonsterType.Boss, Name = "타락한 대마법사", Description = "이세계를 위협하는 악당", Attack = 100, MaxHp = 5000, Defense = 100, Speed = 150, Gold = 100000, SkillIds = [2103, 2104], ItemIds = [409, 410, 411, 412], ItemDropRate = 0.1f });
        }

        public void LoadAllItems()
        {
            int[] cheatItems = ItemDict.Values.Where(s => s.Type == ItemType.Consumable).Select(s => s.DataId).ToArray();
            int[] cheatEquips = ItemDict.Values.Where(s => s.Type == ItemType.Equipment).Select(s => s.DataId).ToArray();

            NpcDict[1101].SaleItemIds = cheatItems;
            NpcDict[1102].SaleItemIds = cheatItems;
            NpcDict[1103].SaleItemIds = cheatItems;

            NpcDict[1104].SaleItemIds = cheatEquips;
            NpcDict[1105].SaleItemIds = cheatEquips;
            NpcDict[1106].SaleItemIds = cheatEquips;

            NpcDict[1107].SaleItemIds = cheatEquips;
            NpcDict[1108].SaleItemIds = cheatEquips;
            NpcDict[1109].SaleItemIds = cheatEquips;

            NpcDict[1110].SaleItemIds = cheatEquips;
            NpcDict[1111].SaleItemIds = cheatEquips;
            NpcDict[1112].SaleItemIds = cheatEquips;
        }
    }
}
