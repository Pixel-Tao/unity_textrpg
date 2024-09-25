using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Skills
{
    public class SkillBook
    {
        public CreatureBase Owner { get; private set; }
        public List<Skill> Skills { get; private set; }

        private SkillBook() { }
        public SkillBook(CreatureBase owner)
        {
            Skills = new List<Skill>();
            Owner = owner;
        }

        public void InitSkills(int[] skillIds)
        {
            Skills = new List<Skill>();
            // TODO: Implement InitSkills
            foreach (int skillId in skillIds)
            {
                Skill skill = new Skill(Owner);
                skill.SetInfo(skillId);
                Skills.Add(skill);
            }
        }
        public void SetInfo(SaveSkillData[] skillDatas)
        {
            Skills = new List<Skill>();
            foreach (SaveSkillData data in skillDatas)
            {
                Skill skill = new Skill(Owner);
                skill.SetInfo(data.DataId);
                skill.CurrentCastCount = data.Count;
                Skills.Add(skill);
            }
        }
        public void LoadBuffSkill(int buffSkillId)
        {
            Skill? buffSkill = Skills.FirstOrDefault(s => s.DataId == buffSkillId);
            if (buffSkill == null)
            {
                Console.WriteLine("잘못된 버프 스킬 정보입니다.");
                return;
            }

            Owner.SetBuffSkill(buffSkill);
        }

        public void UseSkill(int skillDataId, CreatureBase[] targets)
        {
            if (skillDataId < 1)
            {
                Console.WriteLine("잘못된 스킬 정보입니다.");
                return;
            }

            foreach (Skill skill in Skills)
            {
                if (skill.DataId == skillDataId)
                {
                    UseSkill(skill, targets);
                    return;
                }
            }

        }
        public void UseSkill(Skill skill, CreatureBase[] targets)
        {
            if (skill.CurrentCastCount <= 0)
            {
                Console.WriteLine("스킬 사용 가능 횟수가 없습니다.");
                return;
            }

            skill.Use(targets);
        }
        public Skill? GetSkill(int skillDataId)
        {
            foreach (Skill skill in Skills)
            {
                if (skill.DataId == skillDataId)
                    return skill;
            }

            return null;
        }
        public bool CanUseSkill(int skillDataId)
        {
            foreach (Skill skill in Skills)
            {
                if (skill.DataId == skillDataId)
                    return skill.CurrentCastCount > 0 && skill.RequiredLevel <= Owner.Level;
            }

            return false;
        }

        public void Resets()
        {
            foreach (Skill skill in Skills)
            {
                skill.Reset();
            }
        }
    }
}
