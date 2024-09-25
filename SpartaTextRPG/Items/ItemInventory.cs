using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Items
{
    public class ItemInventory
    {
        public CreatureBase Owner { get; private set; }
        public ItemBase[] Items { get; private set; }

        public int Gold { get; private set; }

        public ItemInventory(CreatureBase owner)
        {
            Owner = owner;
            if (owner.CreatureType == Defines.CreatureType.Hero)
            {
                Hero hero = (Hero)owner;
                switch (hero.JobType)
                {
                    case Defines.JobType.Warrior:
                        Items = new ItemBase[Defines.WARRIOR_INVENTORY_SIZE];
                        break;
                    case Defines.JobType.Archer:
                        Items = new ItemBase[Defines.ARCHER_INVENTORY_SIZE];
                        break;
                    case Defines.JobType.Mage:
                        Items = new ItemBase[Defines.MAGE_INVENTORY_SIZE];
                        break;
                    case Defines.JobType.Thief:
                        Items = new ItemBase[Defines.THIEF_INVENTORY_SIZE];
                        break;
                }
            }
            else
            {
                Items = new ItemBase[Defines.DEFAULT_INVENTORY_SIZE];
            }
        }
        public void SetInfo(SaveInventoryData data)
        {
            Gold = data.Gold;
            Items = new ItemBase[data.Items.Count];
            for (int i = 0; i < data.Items.Count; i++)
            {
                SaveItemData itemData = data.Items[i];
                if (itemData == null)
                {
                    Items[i] = null;
                    continue;
                }
                if (DataManager.Instance.ItemDict.TryGetValue(itemData.DataId, out ItemData itemInfo))
                {
                    switch (itemInfo.Type)
                    {
                        case Defines.ItemType.Consumable:
                            ConsumableItem consumableItem = new ConsumableItem(Owner);
                            consumableItem.SetInfo(itemInfo);
                            consumableItem.AddCount(itemData.Count);
                            Items[i] = consumableItem;
                            break;
                        case Defines.ItemType.Equipment:
                            EquipmentItem equipmentItem = new EquipmentItem(Owner);
                            equipmentItem.SetInfo(itemInfo);
                            Items[i] = equipmentItem;
                            break;
                        case Defines.ItemType.Etc:
                            EtcItem etcItem = new EtcItem(Owner);
                            etcItem.SetInfo(itemInfo);
                            etcItem.AddCount(itemData.Count);
                            Items[i] = etcItem;
                            break;
                    }
                }
            }
        }
        public void LoadEuippedItem(int dataId)
        {
            if (Owner.CreatureType != Defines.CreatureType.Hero)
                return;

            Hero hero = (Hero)Owner;
            ItemBase? item = Items.FirstOrDefault(s => s?.DataId == dataId);
            if (item == null)
            {
                //TextManager.WarningWriteLine("해당 아이템을 찾을 수 없습니다.");
                return;
            }
            if (item.ItemType != Defines.ItemType.Equipment)
            {
                TextManager.MWriteLine("장비 아이템이 아닙니다.");
                return;
            }
            hero.Equip(item.CastItem<EquipmentItem>());
        }
        public void LoadConsumableItem(int dataId)
        {
            if (Owner.CreatureType != Defines.CreatureType.Hero)
                return;

            Hero hero = (Hero)Owner;
            ItemBase? item = Items.FirstOrDefault(s => s?.DataId == dataId);
            if (item == null)
            {
                if (DataManager.Instance.ItemDict.TryGetValue(dataId, out ItemData itemData))
                {
                    ConsumableItem consumableItem = new ConsumableItem(Owner);
                    consumableItem.SetInfo(itemData);
                    item = consumableItem;
                }
                else
                {
                    TextManager.MWriteLine("해당 아이템을 찾을 수 없습니다.");
                    return;
                }

            }
            if (item.ItemType != Defines.ItemType.Consumable)
            {
                TextManager.MWriteLine("소비 아이템이 아닙니다.");
                return;
            }
            hero.SetConsumeableItem(item.CastItem<ConsumableItem>());

        }

        public void SetItems(ItemBase[] items)
        {
            Items = items;
        }
        public void SetItems(int[] itemIds)
        {
            foreach (int itemId in itemIds)
            {
                if (DataManager.Instance.ItemDict.TryGetValue(itemId, out ItemData itemData))
                {
                    switch (itemData.Type)
                    {
                        case Defines.ItemType.Consumable:
                            ConsumableItem weapon = new ConsumableItem(Owner);
                            weapon.SetInfo(itemData);
                            AddItem(weapon);
                            break;
                        case Defines.ItemType.Equipment:
                            EquipmentItem armor = new EquipmentItem(Owner);
                            armor.SetInfo(itemData);
                            AddItem(armor);
                            break;
                        case Defines.ItemType.Etc:
                            EtcItem potion = new EtcItem(Owner);
                            potion.SetInfo(itemData);
                            AddItem(potion);
                            break;
                    }
                }
            }
        }
        public void SetGold(int gold)
        {
            Gold = gold;
        }
        public void AddGold(int gold)
        {
            TextManager.LWriteLine($"{Owner.Name}님이 {gold}G를 획득했습니다.");
            Gold += gold;
        }
        public void RemoveGold(int gold)
        {
            TextManager.LWriteLine($"{Owner.Name}님이 {gold}G를 사용했습니다.");
            Gold -= gold;
        }
        public void AddItem(ItemBase item, int count = 1)
        {
            int emptySlotIndex = Array.IndexOf(Items, null);
            if (item.ItemType == Defines.ItemType.Equipment && emptySlotIndex != -1)
            {
                // 장비 아이템은 중첩되지않으므로 빈 슬롯에 추가
                Items[emptySlotIndex] = item;
                return;
            }
            // 중첩 가능한 아이템이 있는지 먼저 확인
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i]?.DataId == item.DataId)
                {
                    // 있으면 count만큼 추가
                    Items[i].AddCount(count);
                    return;
                }
            }
            // 중첩 가능한 아이템이 없으면 빈슬롯에 추가
            if (emptySlotIndex != -1)
                Items[emptySlotIndex] = item;
        }
        public void AddItem(ItemData item, int count = 1)
        {
            if (DataManager.Instance.ItemDict.TryGetValue(item.DataId, out ItemData itemData))
            {
                switch (itemData.Type)
                {
                    case Defines.ItemType.Consumable:
                        ConsumableItem consumableItem = new ConsumableItem(Owner);
                        consumableItem.SetInfo(itemData);
                        consumableItem.AddCount(count);
                        AddItem(consumableItem);
                        break;
                    case Defines.ItemType.Equipment:
                        EquipmentItem equipmentItem = new EquipmentItem(Owner);
                        equipmentItem.SetInfo(itemData);
                        AddItem(equipmentItem);
                        break;
                    case Defines.ItemType.Etc:
                        EtcItem etcItem = new EtcItem(Owner);
                        etcItem.SetInfo(itemData);
                        etcItem.AddCount(count);
                        AddItem(etcItem);
                        break;
                }
            }
        }
        public void RemoveItem(int dataId, int count = 1)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i]?.DataId == dataId)
                {
                    switch (Items[i].ItemType)
                    {
                        case Defines.ItemType.Consumable:
                            ConsumableItem consumableItem = Items[i].CastItem<ConsumableItem>();
                            consumableItem.RemoveCount(count);
                            if (consumableItem.Count < 1)
                            {
                                Items[i] = null;
                            }
                            return;
                        case Defines.ItemType.Etc:
                            EtcItem etcItem = Items[i].CastItem<EtcItem>();
                            etcItem.RemoveCount(count);
                            if (etcItem.Count < 1)
                            {
                                Items[i] = null;
                            }
                            return;
                        default:
                            Items[i] = null;
                            return;
                    }
                }
            }
        }
        public void RemoveItem(ItemBase item, int count = 1)
        {
            RemoveItem(item.DataId, count);
        }
        public void SortItems()
        {
            Items = Items.OrderBy(x => x == null).ThenBy(x => x?.ItemType).ThenBy(x => x?.Name).ThenBy(x => x?.DataId).ToArray();
            TextManager.LWriteLine("아이템을 정렬했습니다.");
        }
        public bool IsFull()
        {
            // 아이템이 null인게 있는지 확인
            // 있으면 Full이 아님 = false
            if (Items.Any(s => s == null)) return false;
            // 없으면 Full 임 = true
            return true;
        }
        public bool IsFullCount(ItemBase item)
        {
            if (item.ItemType == Defines.ItemType.Equipment)
                return HasEquipmentItem(item);
            else if (item.ItemType == Defines.ItemType.Consumable)
                return Items.FirstOrDefault(s => s?.DataId == item.DataId)?.CastItem<ConsumableItem>().Count >= Defines.MAX_CONSUMABLE_COUNT;
            else if (item.ItemType == Defines.ItemType.Etc)
                return Items.FirstOrDefault(s => s?.DataId == item.DataId)?.CastItem<EtcItem>().Count >= Defines.MAX_ETC_COUNT;

            return false;
        }
        public bool IsEquipped(ItemBase item)
        {
            if (item.ItemType != Defines.ItemType.Equipment)
                return false;

            return Owner.IsEquipped(item.CastItem<EquipmentItem>());
        }
        public bool HasItem(ItemBase? item)
        {
            if (item == null) return false;

            return Items.Aggregate(false, (acc, x) => acc || x?.DataId == item.DataId);
        }
        public bool HasEquipmentItem(ItemBase item)
        {
            if (item == null) return false;
            if (item.ItemType != Defines.ItemType.Equipment)
                return false;

            return Items.Aggregate(false, (acc, x) => acc || (x?.DataId == item.DataId));
        }
        public bool HasItemData(ItemData itemData)
        {
            return Items.Aggregate(false, (acc, x) => acc || x.DataId == itemData.DataId);
        }
        public int HasItemCount(ItemBase item)
        {
            if (item.ItemType == Defines.ItemType.Consumable)
            {
                return Items.FirstOrDefault(s => s?.DataId == item.DataId)?.CastItem<ConsumableItem>().Count ?? 0;
            }

            return Items.Count(s => s.DataId == item.DataId);
        }
        public int ItemTotalCount()
        {
            return Items.Count(s => s != null);
        }
        public ItemBase? GetItem(int dataId)
        {
            return Items.FirstOrDefault(s => s?.DataId == dataId);
        }

        public bool IsEmpty()
        {
            return Items.All(s => s == null);
        }

        public T[] GetItems<T>() where T : ItemBase
        {
            return Items.Where(s => s != null && s.GetType() == typeof(T))
                .Select(s => s.CastItem<T>())
                .ToArray();
        }
    }
}
