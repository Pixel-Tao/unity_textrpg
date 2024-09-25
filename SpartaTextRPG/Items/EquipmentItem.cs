using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Items
{
    public class EquipmentItem : ItemBase
    {
        public override Defines.ItemType ItemType { get; protected set; } = Defines.ItemType.Equipment;

        public Defines.JobType UseableHero { get; private set; }
        public Defines.EquipmentType EquipmentType { get; private set; }

        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Speed { get; private set; }
        public int RequiredLevel { get; private set; }

        public bool IsEquipped => Owner != null && Owner.IsEquipped(this);

        public EquipmentItem(CreatureBase owner)
        {
            Owner = owner;
        }

        public override ItemBase SetInfo(ItemData itemData, long uid = 0)
        {
            EquipmentItemData? data = itemData as EquipmentItemData;
            if (data == null)
                throw new Exception("장비 타입이 아닙니다.");

            DataId = data.DataId;
            Name = data.Name;
            Description = data.Description;
            EquipmentType = data.EquipmentType;
            Attack = data.Attack;
            Defense = data.Defense;
            Speed = data.Speed;
            RequiredLevel = data.RequiredLevel;
            UseableHero = data.HeroType;
            Price = data.Price;
            return this;
        }

        public override void AddCount(int count = 1)
        {
            // 장비는 중첩 할 수 없음. 
        }

        public override void RemoveCount(int count = 1)
        {
            // 장비는 중첩 할 수 없음. 
        }
    }
}
