using SpartaTextRPG.Maps;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Datas
{
    [Serializable]
    public class SaveHeroData
    {
        public long Uid;
        public string Name;
        public int Level;
        public int Exp;
        public int Hp;
        public Defines.JobType JobType;
        public int EuqippedWeaponId;
        public int EuqippedArmorId;
        public int EuqippedAccessoryId;
        public int EuqippedSubWeaponId;
        public int ConsumableItemId;
        public int BuffSkillId;
        public Defines.MapType MapType;
        public Vector2Int Position;
        public SaveInventoryData InventoryData;
        public List<SaveSkillData> Skills;
    }
    [Serializable]
    public class SaveInventoryData
    {
        public int Gold;
        public List<SaveItemData> Items;
    }
    [Serializable]
    public class SaveItemData
    {
        public long Uid;
        public int Count;
        public int DataId;
    }
    [Serializable]
    public class SaveSkillData
    {
        public int DataId;
        public int Count;
    }
}
