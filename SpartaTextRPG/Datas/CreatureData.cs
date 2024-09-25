using SpartaTextRPG.Maps;
using SpartaTextRPG.Items;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Datas
{
    public class CreatureStatData
    {
        public int Level;
        public int MaxHp;
        public int Attack;
        public int Defense;
        public int Speed;
        public int NextLevelExp;
    }

    public class NpcData
    {
        public int DataId;
        public Defines.NpcType NpcType;
        public string? Name;
        public string? Description;
        public int[]? SaleItemIds;
        public string[]? Messaages;
    }

    public class JobData
    {
        public Defines.JobType JobType;
        public string? Name;
        public string? Description;
        public int[]? SkillIds;
        public int MaxHp;
        public int Attack;
        public int Defense;
        public int Speed;
    }

    public class MonsterData
    {
        public int DataId;
        public Defines.MonsterType MonsterType;
        public Defines.JobType JobType;
        public string? Name;
        public string? Description;
        public int MaxHp;
        public int Attack;
        public int Defense;
        public int Speed;
        public int Exp;
        public int Gold;
        public float ItemDropRate;
        public int[]? SkillIds;
        public int[]? ItemIds;
        public string[]? Messages;
    }
}
