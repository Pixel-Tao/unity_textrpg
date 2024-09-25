using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Datas
{
    public class SkillData
    {
        public int DataId;
        public string Name;
        public string Description;
        public Defines.SkillType SkillType;
        public Defines.JobType HeroType;
        public double DamagePerValue;
        public double HealPerValue;
        public double BuffPerValue;
        public int RequiredLevel;
        public int ComboCount;
        public int TargetCount;
        public int DefaultMaxCastCount;
    }
}
