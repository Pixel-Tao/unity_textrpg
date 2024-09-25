using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Creatures
{
    public class Hero : CreatureBase
    {
        public override Defines.CreatureType CreatureType => Defines.CreatureType.Hero;

        public int JobMaxHp { get; protected set; }
        public int JobAttack { get; protected set; }
        public int JobDefense { get; protected set; }
        public int JobSpeed { get; protected set; }

        public override int MaxHp => BonusMaxHp + DefaultMaxHp + JobMaxHp + SBuffDefenseValue(DefaultMaxHp);
        public override int Attack => BonusAttack + DefaultAttack + JobAttack + SBuffAttackValue(DefaultAttack);
        public override int Defense => BonusDefense + DefaultDefense + JobDefense + SBuffDefenseValue(DefaultMaxHp);
        public override int Speed => BonusSpeed + DefaultSpeed + JobSpeed + SBuffAttackValue(DefaultSpeed);

        public EquipmentItem? EWeapon { get; protected set; }
        public EquipmentItem? ESubWeapon { get; protected set; }
        public EquipmentItem? EArmor { get; protected set; }
        public EquipmentItem? EAccessory { get; protected set; }

        public void SetInfo(Defines.JobType jobType, string name)
        {
            this.Uid = Util.GenerateUid();
            this.Name = name;

            this.JobType = jobType;
            this.Inventory = new ItemInventory(this);
            this.Inventory.SetGold(100000);
            this.SkillBook = new SkillBook(this);

            if (DataManager.Instance.JobDict.TryGetValue(jobType, out JobData? jobData) == false)
            {
                TextManager.ErrorWriteLine("잘못된 직업입니다.");
                return;
            }

            Description = jobData.Description;
            JobMaxHp = jobData.MaxHp;
            JobAttack = jobData.Attack;
            JobDefense = jobData.Defense;
            JobSpeed = jobData.Speed;

            SkillBook.InitSkills(jobData.SkillIds ?? []);

            SetLevel(1);
            base.OnHealed(MaxHp);
        }
        public void SetInfo(SaveHeroData data)
        {
            JobData jobData = DataManager.Instance.JobDict[data.JobType];
            CreatureStatData statData = DataManager.Instance.CreatureStatDict[data.Level];

            this.Uid = data.Uid;
            this.Name = data.Name;
            this.Description = jobData.Description;
            this.Level = data.Level;
            this.Exp = data.Exp;
            this.NextLevelExp = statData.NextLevelExp;
            this.JobType = data.JobType;
            this.Hp = data.Hp;

            this.Description = jobData.Description;
            this.JobMaxHp = jobData.MaxHp;
            this.JobAttack = jobData.Attack;
            this.JobDefense = jobData.Defense;
            this.JobSpeed = jobData.Speed;
            this.Inventory = new ItemInventory(this);
            this.Inventory.SetInfo(data.InventoryData);
            this.Inventory.LoadEuippedItem(data.EuqippedWeaponId);
            this.Inventory.LoadEuippedItem(data.EuqippedSubWeaponId);
            this.Inventory.LoadEuippedItem(data.EuqippedArmorId);
            this.Inventory.LoadEuippedItem(data.EuqippedAccessoryId);
            this.Inventory.LoadConsumableItem(data.ConsumableItemId);
            this.SkillBook = new SkillBook(this);
            this.SkillBook.SetInfo(data.Skills.ToArray());
            this.SkillBook.LoadBuffSkill(data.BuffSkillId);

            SetLevel(Level);
            SetPosition(data.MapType, data.Position);
            BonusUpdate();
        }
        public override void OnDamaged(int damage, CreatureBase? attacker = null)
        {
            base.OnDamaged(damage, attacker);
            if (attacker == null)
                TextManager.SystemWriteLine($"{Name}님이 {damage}의 피해를 입었습니다.");
            else
                TextManager.SystemWriteLine($"{Name}님이 {attacker.Name}에 의해 {damage}의 피해를 입었습니다.");
        }
        public override void OnHealed(int heal, CreatureBase? healer = null)
        {
            base.OnHealed(heal, healer);
            if (healer == null)
                TextManager.SystemWriteLine($"{Name}님이 {heal} 회복되었습니다.");
            else
                TextManager.SystemWriteLine($"{Name}님이 {healer.Name}에 의해 체력이 {heal} 회복되었습니다.");
        }
        public override void OnDead()
        {
            TextManager.SystemWriteLine($"{Name}님이 사망하였습니다.");
        }

        public override void AddExp(int exp)
        {
            this.Exp += exp;
            TextManager.SystemWriteLine($"{Name}님이 {exp}의 경험치를 획득했습니다.");
            if (this.Exp >= this.NextLevelExp)
            {
                LevelUp();
            }
        }
        public override void SetLevel(int level)
        {
            // 레벨업 데이터 가져와서
            Level = level;
            CreatureStatData statData = DataManager.Instance.CreatureStatDict[level];
            DefaultMaxHp = statData.MaxHp;
            DefaultAttack = statData.Attack;
            DefaultDefense = statData.Defense;
            DefaultSpeed = statData.Speed;
            NextLevelExp = statData.NextLevelExp;
            BonusUpdate();
        }
        public override void LevelUp()
        {
            base.LevelUp();
            TextManager.SystemWriteLine("레벨이 올랐습니다! 상태를 회복합니다!");
        }
        public override void BonusUpdate()
        {
            base.BonusUpdate();

            BonusAttack = 0;
            BonusAttack += EWeapon?.Attack ?? 0;
            BonusAttack += ESubWeapon?.Attack ?? 0;
            BonusAttack += EArmor?.Defense ?? 0;
            BonusAttack += EAccessory?.Speed ?? 0;

            BonusDefense = 0;
            BonusDefense += EWeapon?.Defense ?? 0;
            BonusDefense += ESubWeapon?.Defense ?? 0;
            BonusDefense += EArmor?.Defense ?? 0;
            BonusDefense += EAccessory?.Defense ?? 0;

            BonusSpeed = 0;
            BonusSpeed += EWeapon?.Speed ?? 0;
            BonusSpeed += ESubWeapon?.Speed ?? 0;
            BonusSpeed += EArmor?.Speed ?? 0;
            BonusSpeed += EAccessory?.Speed ?? 0;

        }

        public void Equip(EquipmentItem equipment)
        {
            // 장비 착용
            if (equipment.UseableHero != JobType)
            {
                TextManager.SystemWriteLine("사용 가능한 직업이 아닙니다.");
                return;
            }

            if (equipment.RequiredLevel > Level)
            {
                TextManager.SystemWriteLine("착용 가능 레벨이 부족합니다.");
                return;
            }

            Unequip(equipment.EquipmentType);

            switch (equipment.EquipmentType)
            {
                case Defines.EquipmentType.Weapon:
                    EWeapon = equipment;
                    break;
                case Defines.EquipmentType.SubWeapon:
                    ESubWeapon = equipment;
                    break;
                case Defines.EquipmentType.Armor:
                    EArmor = equipment;
                    break;
                case Defines.EquipmentType.Accessory:
                    EAccessory = equipment;
                    break;
            }

            BonusAttack += equipment.Attack;
            BonusDefense += equipment.Defense;
            BonusSpeed += equipment.Speed;
            BonusUpdate();
            TextManager.SystemWriteLine($"{equipment.Name}을(를) 장착하였습니다.");
        }
        public void Unequip(Defines.EquipmentType equipmentType)
        {
            EquipmentItem? prevEquipment = null;
            switch (equipmentType)
            {
                case Defines.EquipmentType.Weapon:
                    if (EWeapon != null)
                    {
                        BonusAttack -= EWeapon.Attack;
                        prevEquipment = EWeapon;
                        EWeapon = null;
                    }
                    break;
                case Defines.EquipmentType.SubWeapon:
                    if (ESubWeapon != null)
                    {
                        BonusAttack -= ESubWeapon.Attack;
                        prevEquipment = ESubWeapon;
                        ESubWeapon = null;
                    }
                    break;
                case Defines.EquipmentType.Armor:
                    if (EArmor != null)
                    {
                        BonusDefense -= EArmor.Defense;
                        prevEquipment = EArmor;
                        EArmor = null;
                    }
                    break;
                case Defines.EquipmentType.Accessory:
                    if (EAccessory != null)
                    {
                        BonusSpeed -= EAccessory.Speed;
                        prevEquipment = EAccessory;
                        EAccessory = null;
                    }
                    break;
            }

            if (prevEquipment != null)
            {
                TextManager.SystemWriteLine($"{prevEquipment.Name}을(를) 장착해제 하였습니다.");
                BonusUpdate();
            }
        }
        public void Consume(ConsumableItem? consumable)
        {
            // 소비 아이템 사용
            TextManager.SystemWriteLine($"{consumable.Name}을(를) 사용하였습니다.");
            SetConsumeableItem(consumable);
            consumable.RemoveItem(); // 소비아이템 감소시킴
        }
        public void SetConsumeableItem(ConsumableItem? consumable)
        {
            // 소비 아이템 효과만 적용
            if (consumable == null) return;

            switch (consumable.ConsumableType)
            {
                case Defines.ConsumableType.Heal:
                    OnHealed(consumable.Value);
                    break;
                case Defines.ConsumableType.HpRegenBuff:
                    CBuff = consumable;
                    break;
                case Defines.ConsumableType.MaxHpBuff:
                    CBuff = consumable;
                    BonusMaxHp += consumable.Value;
                    break;
                case Defines.ConsumableType.AttackBuff:
                    CBuff = consumable;
                    BonusAttack += consumable.Value;
                    break;
                case Defines.ConsumableType.DefenseBuff:
                    CBuff = consumable;
                    BonusDefense += consumable.Value;
                    break;
                case Defines.ConsumableType.SpeedBuff:
                    CBuff = consumable;
                    BonusSpeed += consumable.Value;
                    break;
            }

            if (consumable.ConsumableType != Defines.ConsumableType.Heal)
            {
                TextManager.SystemWriteLine($"{Util.ConsumableTypeToString(consumable.ConsumableType)} 효과가 생겼습니다. 해당 효과는 던전 클리어 후 사라집니다.");
            }

            BonusUpdate();
        }

        public override bool IsEquipped(EquipmentItem eqItem)
        {
            if (eqItem == null) return false;

            return EWeapon?.DataId == eqItem.DataId
                || ESubWeapon?.DataId == eqItem.DataId
                || EArmor?.DataId == eqItem.DataId
                || EAccessory?.DataId == eqItem.DataId;
        }

        public void MovePosition(ConsoleKey key)
        {
            switch (key)
            {
                case Defines.DOWN_KEY:
                    CurrentPosition = new Vector2Int(CurrentPosition.X, CurrentPosition.Y - 1);
                    break;
                case Defines.UP_KEY:
                    CurrentPosition = new Vector2Int(CurrentPosition.X, CurrentPosition.Y + 1);
                    break;
                case Defines.LEFT_KEY:
                    CurrentPosition = new Vector2Int(CurrentPosition.X - 1, CurrentPosition.Y);
                    break;
                case Defines.RIGHT_KEY:
                    CurrentPosition = new Vector2Int(CurrentPosition.X + 1, CurrentPosition.Y);
                    break;
            }
        }

    }
}
