using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Items
{
    public class EtcItem : ItemBase
    {
        public override Defines.ItemType ItemType { get; protected set; } = Defines.ItemType.Etc;
        public Defines.EtcType EtcType { get; private set; }
        public int Count { get; private set; }

        public EtcItem(CreatureBase owner)
        {
            Owner = owner;
        }

        public override ItemBase SetInfo(ItemData itemData, long uid = 0)
        {
            EtcItemData data = itemData as EtcItemData;
            if (data == null)
                throw new ArgumentException("ItemData is not EtcItemData");

            DataId = data.DataId;
            Name = data.Name;
            Description = data.Description;
            Price = data.Price;
            EtcType = data.EtcType;
            return this;
        }

        public override void AddCount(int count = 1)
        {
            Count += count;
        }
        public override void RemoveCount(int count = 1)
        {
            Count -= count;
            if (Count < 0) Count = 0;
        }
    }
}
