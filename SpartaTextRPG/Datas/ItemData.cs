using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Datas
{
    public class ItemData
    {
        public int DataId;
        public string Name;
        public string Description;
        public int Price;
        public Defines.ItemType Type;
    }
    public class ConsumableItemData : ItemData
    {
        public Defines.ConsumableType ConsumableType;
        public int Value;
    }
    public class EquipmentItemData : ItemData
    {
        public Defines.JobType HeroType;
        public Defines.EquipmentType EquipmentType;
        public int Attack;
        public int Defense;
        public int Speed;
        public int RequiredLevel;
    }
    public class EtcItemData : ItemData
    {
        public Defines.EtcType EtcType;
    }
}
