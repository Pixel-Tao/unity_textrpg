using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Skills
{
    public class Skill
    {
        public CreatureBase? Owner { get; protected set; }

        public int DataId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Defines.SkillType SkillType { get; protected set; }
        /// <summary>
        /// 데미지를 공격력의 퍼센트 만큼 피해를 줌
        /// </summary>
        public double DamagePerValue { get; protected set; }
        /// <summary>
        /// 힐을 공격력의 퍼센트 만큼 회복함
        /// </summary>
        public double HealPerValue { get; protected set; }
        /// <summary>
        /// 버프 퍼센트만큼 능력치를 상승시킴
        /// </summary>
        public double BuffPerValue { get; protected set; }
        /// <summary>
        /// 연속 공격 횟수
        /// </summary>
        public int ComboCount { get; protected set; }
        /// <summary>
        /// 사용 가능 레벨
        /// </summary>
        public int RequiredLevel { get; protected set; }
        public int DefaultMaxCastCount { get; protected set; }

        /// <summary>
        /// 스킬 사용 가능 최대 횟수
        /// </summary>
        public int MaxCastCount { get; protected set; }
        public int TargetCount { get; protected set; }
        /// <summary>
        /// 스킬 사용 가능 횟수
        /// </summary>
        public int CurrentCastCount { get; set; }
        private Skill() { }
        public Skill(CreatureBase owner)
        {
            this.Owner = owner;
        }

        public void SetInfo(int skillDataId)
        {
            if (DataManager.Instance.SkillDict.TryGetValue(skillDataId, out SkillData data) == false)
            {
                TextManager.WarningWriteLine($"스킬 데이터를 찾을 수 없습니다. id: {skillDataId}");
                return;
            }
            DataId = data.DataId;
            Name = data.Name;
            SkillType = data.SkillType;
            DamagePerValue = data.DamagePerValue;
            HealPerValue = data.HealPerValue;
            BuffPerValue = data.BuffPerValue;
            ComboCount = data.ComboCount;
            RequiredLevel = data.RequiredLevel;
            CurrentCastCount = data.DefaultMaxCastCount;
            MaxCastCount = data.DefaultMaxCastCount;
            DefaultMaxCastCount = data.DefaultMaxCastCount;
            TargetCount = data.TargetCount;

            Description = data.Description
                .Replace("{DamagePerValue}", (DamagePerValue * 100).ToString("F0"))
                .Replace("{HealPerValue}", (HealPerValue * 100).ToString("F0"))
                .Replace("{BuffPerValue}", (BuffPerValue * 100).ToString("F0"))
                .Replace("{ComboCount}", ComboCount.ToString())
                .Replace("{TargetCount}", TargetCount.ToString());

            Reset();
        }

        public void Reset()
        {
            if (Owner.CreatureType == Defines.CreatureType.Hero)
            {
                Hero hero = (Hero)Owner;
                MaxCastCount = DefaultMaxCastCount + (hero.JobType == Defines.JobType.Mage ? DefaultMaxCastCount * 2 : 0);
            }
            CurrentCastCount = MaxCastCount;
        }
        public bool Use(CreatureBase[]? targets = null)
        {
            if (Owner.Level < RequiredLevel)
            {
                TextManager.SystemWriteLine("레벨이 부족합니다.");
                return false;
            }
            if (CurrentCastCount <= 0)
            {
                TextManager.SystemWriteLine("스킬 사용 가능 횟수가 없습니다.");
                return false;
            }

            if (Owner.CreatureType == Defines.CreatureType.Hero)
                TextManager.SystemWriteLine($"{Owner.Name}님이 {Name} 스킬을 사용했습니다.");
            else
                TextManager.SystemWriteLine($"{Owner.Name}이(가) {Name} 스킬을 사용했습니다.");

            switch (SkillType)
            {
                case Defines.SkillType.Attack:
                    if (targets != null && targets.Length > 0)
                    {
                        UseAttackSkill(targets[0]);
                    }
                    break;
                case Defines.SkillType.AOE:
                    if (targets != null)
                    {
                        foreach (var target in targets)
                            UseAttackSkill(target);
                    }
                    break;
                case Defines.SkillType.Heal:
                    UseHealSkill(Owner);
                    break;
                case Defines.SkillType.DefenseBuff:
                case Defines.SkillType.AttackBuff:
                case Defines.SkillType.HealBuff:
                    UseBuffSkill();
                    break;
            }

            CurrentCastCount--;

            return true;
        }

        private void UseAttackSkill(CreatureBase target)
        {
            if (Owner.Level < RequiredLevel)
            {
                TextManager.SystemWriteLine("레벨이 부족합니다.");
                return;
            }
            if (CurrentCastCount <= 0)
            {
                TextManager.SystemWriteLine("스킬 사용 가능 횟수가 없습니다.");
                return;
            }
            int value = (int)(Owner.Attack * DamagePerValue);
            target.OnDamaged(value, Owner);
        }

        private void UseHealSkill(CreatureBase target)
        {
            if (Owner.Level < RequiredLevel)
            {
                TextManager.SystemWriteLine("레벨이 부족합니다.");
                return;
            }
            if (CurrentCastCount <= 0)
            {
                TextManager.SystemWriteLine("스킬 사용 가능 횟수가 없습니다.");
                return;
            }
            int value = (int)(Owner.Attack * HealPerValue);
            target.OnHealed(value, Owner);
        }

        private void UseBuffSkill()
        {
            Owner.SetBuffSkill(this);
        }

        public void AddCount(int count = 1)
        {
            CurrentCastCount += count;
        }

    }
}
