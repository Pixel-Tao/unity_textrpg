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
    public class ConsumableItem : ItemBase
    {
        public override Defines.ItemType ItemType { get; protected set; } = Defines.ItemType.Consumable;
        public Defines.ConsumableType ConsumableType { get; private set; }

        public int Count { get; private set; }
        public int Value { get; private set; }

        public bool IsBuff => ConsumableType == Defines.ConsumableType.AttackBuff ||
                                ConsumableType == Defines.ConsumableType.DefenseBuff ||
                                ConsumableType == Defines.ConsumableType.MaxHpBuff ||
                                ConsumableType == Defines.ConsumableType.SpeedBuff ||
                                ConsumableType == Defines.ConsumableType.HpRegenBuff;

        public ConsumableItem(CreatureBase owner)
        {
            Owner = owner;
        }

        public virtual void RemoveItem()
        {
            RemoveCount(1);
            if (Count < 1)
            {
                Owner.Inventory.RemoveItem(this);
            }
        }

        public override ItemBase SetInfo(ItemData itemData, long uid = 0)
        {
            ConsumableItemData data = itemData as ConsumableItemData;
            if (data == null)
                throw new ArgumentException("ItemData is not ConsumableItemData");

            DataId = data.DataId;
            Name = data.Name;
            Description = string.Format(data.Description, data.Value);
            ConsumableType = data.ConsumableType;
            Value = data.Value;
            Price = data.Price;
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
