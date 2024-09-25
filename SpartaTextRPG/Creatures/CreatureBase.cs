using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Creatures
{
    /// <summary>
    /// 모든 유닛의 기본적인 특성을 정의합니다.
    /// </summary>
    public abstract class CreatureBase
    {
        /// <summary>
        /// 유닛의 종류
        /// </summary>
        public virtual Defines.CreatureType CreatureType => Defines.CreatureType.None;
        public Defines.JobType JobType { get; protected set; } = Defines.JobType.None;
        public long Uid { get; protected set; }
        /// <summary>
        /// 유닛의 이름을 가져옵니다.
        /// </summary>
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }

        public int Hp { get; protected set; }

        // 능력치 아이템 + 버프 효과
        public int BonusMaxHp { get; protected set; }
        public int BonusAttack { get; protected set; }
        public int BonusDefense { get; protected set; }
        public int BonusSpeed { get; protected set; }

        public int DefaultMaxHp { get; protected set; }
        public int DefaultAttack { get; protected set; }
        public int DefaultDefense { get; protected set; }
        public int DefaultSpeed { get; protected set; }

        public virtual int MaxHp => BonusMaxHp + DefaultMaxHp + SBuffDefenseValue(DefaultMaxHp);
        public virtual int Attack => BonusAttack + DefaultAttack + SBuffAttackValue(DefaultAttack);
        public virtual int Defense => BonusDefense + DefaultDefense + SBuffDefenseValue(DefaultMaxHp);
        public virtual int Speed => BonusSpeed + DefaultSpeed + SBuffAttackValue(DefaultSpeed);
        /// <summary>
        /// 레벨
        /// </summary>
        public int Level { get; protected set; }
        public int Exp { get; protected set; }
        public int NextLevelExp { get; protected set; }

        public bool IsDead => Hp <= 0;
        public Defines.MapType CurrentMapType { get; protected set; }
        public Vector2Int CurrentPosition { get; protected set; }
        /// <summary>
        /// 스킬 버프 효과
        /// </summary>
        public Skill? SBuff { get; protected set; }
        public ConsumableItem? CBuff { get; protected set; }
        /// <summary>
        /// 크리쳐가 소유하고 있는 아이템
        /// </summary>
        public ItemInventory Inventory { get; protected set; }
        public SkillBook SkillBook { get; protected set; }

        protected Random rand = new Random();

        public CreatureBase()
        {
            Inventory = new ItemInventory(this);
            SkillBook = new SkillBook(this);
        }
        /// <summary>
        /// 데미지를 입음
        /// </summary>
        /// <param name="damage"></param>
        public virtual void OnDamaged(int damage, CreatureBase? attacker = null)
        {
            // 방어력이 데미지보다 크면 최소 1의 데미지는 입음
            Hp -= Math.Max(damage - Defense, 1);
            if (Hp < 0)
            {
                Hp = 0;
                OnDead();
            }
        }
        /// <summary>
        /// Heal 효과로 체력 회복
        /// </summary>
        /// <param name="heal"></param>
        public virtual void OnHealed(int heal, CreatureBase? healer = null)
        {
            Hp += heal;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }

        public virtual void OnDead()
        {

        }

        public virtual void Rest()
        {
            OnHealed(MaxHp);
            SkillBook?.Resets();
        }

        public virtual void SetBuffSkill(Skill skill)
        {
            SBuff = skill;
            TextManager.SystemWriteLine($"{skill.Name} 버프 스킬 효과를 받았습니다.");
        }
        /// <summary>
        /// 레벨업
        /// </summary>
        public virtual void SetLevel(int level)
        {
            if (level > Defines.CREATURE_MAX_LEVEL)
            {
                level = Defines.CREATURE_MAX_LEVEL;
                TextManager.SystemWriteLine("더 이상 레벨업 할 수 없습니다.");
                return;
            }

            // 레벨업 데이터 가져와서
            CreatureStatData statData = DataManager.Instance.CreatureStatDict[level];
            Level = level;
            NextLevelExp = statData.NextLevelExp;

            DefaultMaxHp = statData.MaxHp;
            DefaultAttack = statData.Attack;
            DefaultDefense = statData.Defense;
            DefaultSpeed = statData.Speed;

            BonusUpdate();
        }
        public virtual void LevelUp()
        {
            SetLevel(Level + 1);
            Exp = 0;
            OnHealed(MaxHp);
            SkillBook?.Resets();
        }
        public virtual void AddExp(int exp)
        {
            this.Exp += exp;
            if (this.Exp >= this.NextLevelExp)
            {
                LevelUp();
            }
        }
        public virtual void BonusUpdate()
        {
            BonusMaxHp = 0;
            if (CBuff != null)
            {
                switch (CBuff.ConsumableType)
                {
                    case Defines.ConsumableType.MaxHpBuff:
                        BonusMaxHp += CBuff.Value;
                        break;
                    case Defines.ConsumableType.AttackBuff:
                        BonusAttack += CBuff.Value;
                        break;
                    case Defines.ConsumableType.DefenseBuff:
                        BonusDefense += CBuff.Value;
                        break;
                    case Defines.ConsumableType.SpeedBuff:
                        BonusSpeed += CBuff.Value;
                        break;
                }
            }
        }
        public virtual int SBuffAttackValue(int defaultValue)
        {
            if (SBuff == null)
                return 0;

            switch(SBuff.SkillType)
            {
                case Defines.SkillType.AttackBuff:
                    return (int)(SBuff.BuffPerValue * defaultValue);
                default:
                    return 0;
            }
        }
        public virtual int SBuffDefenseValue(int defaultValue)
        {
            if (SBuff == null)
                return 0;

            switch (SBuff.SkillType)
            {
                case Defines.SkillType.DefenseBuff:
                    return (int)(SBuff.BuffPerValue * defaultValue);
                default:
                    return 0;
            }
        }
        public virtual void SetPosition(Defines.MapType mapType, Vector2Int? pos)
        {
            if (pos == null) return;
            CurrentMapType = mapType;
            CurrentPosition = new Vector2Int(pos.Value.X, pos.Value.Y);
        }
        public virtual bool IsEquipped(EquipmentItem eqItem)
        {
            if (eqItem == null) return false;

            return true;
        }

        public bool IsDoubleAttack()
        {
            if (JobType == Defines.JobType.Thief)
                return rand.Next(0, 100) < Defines.THIEF_DOUBLE_ATTACK_RATE;

            return false;
        }

        public void BuffClear()
        {
            if (SBuff != null)
            {
                TextManager.SystemWriteLine($"{SBuff!.Name} 버프 효과가 사라졌습니다.");
                SBuff = null;
            }
            if (CBuff != null)
            {
                TextManager.SystemWriteLine($"{CBuff!.Name} 버프 효과가 사라졌습니다.");
                CBuff = null;
            }
            BonusUpdate();
        }
    }
}
