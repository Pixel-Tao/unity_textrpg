using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.UIs;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Creatures
{
    public class Npc : CreatureBase
    {
        public override Defines.CreatureType CreatureType => Defines.CreatureType.NPC;
        public Defines.TileType TileType { get; protected set; }
        public Defines.NpcType NpcType { get; protected set; }

        private Random random = new Random();

        public string[] Messages { get; protected set; }
        public string Message => Messages[random.Next(Messages.Length)];

        public List<ItemBase> SaleItems { get; protected set; } = new List<ItemBase>();
        public UIBase? Menu { get; protected set; }

        public Npc(NpcData data)
        {
            SetLevel(1);
            NpcType = data.NpcType;
            Name = data.Name;
            Description = data.Description;
            Messages = data.Messaages ?? ["어서오세요."];
            switch (NpcType)
            {
                case Defines.NpcType.ItemShopNpc:
                    TileType = Defines.TileType.ItemShopEvent;
                    break;
                case Defines.NpcType.WeaponShopNpc:
                    TileType = Defines.TileType.WeaponShopEvent;
                    break;
                case Defines.NpcType.ArmorShopNpc:
                    TileType = Defines.TileType.ArmorShopEvent;
                    break;
                case Defines.NpcType.AccessoryShopNpc:
                    TileType = Defines.TileType.AccessoryShopEvent;
                    break;
                case Defines.NpcType.InnNpc:
                    TileType = Defines.TileType.InnEvent;
                    break;
            }
            if (data.SaleItemIds != null)
                InitSaleItems(data.SaleItemIds);
        }

        private void InitSaleItems(int[] itemIds)
        {
            SaleItems.Clear();
            foreach (int itemId in itemIds)
            {
                if (DataManager.Instance.ItemDict.TryGetValue(itemId, out ItemData? itemData) == false)
                {
                    Console.WriteLine($"ItemData is null. itemId: {itemId}");
                    continue;
                }
                if (itemData.Type == Defines.ItemType.Consumable)
                    SaleItems.Add(new ConsumableItem(this).SetInfo(itemData));
                else if (itemData.Type == Defines.ItemType.Equipment)
                    SaleItems.Add(new EquipmentItem(this).SetInfo(itemData));
                else if (itemData.Type == Defines.ItemType.Etc)
                    SaleItems.Add(new EtcItem(this).SetInfo(itemData));
            }
        }
        public virtual void Enter(CreatureBase visitor)
        {
            UIManager.Instance.ShowShopVisitJob(this, visitor);
        }

    }
}
