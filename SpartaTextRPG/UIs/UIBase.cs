using SpartaTextRPG.Creatures;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs
{
    public abstract class UIBase
    {
        public abstract Defines.MenuType[] Menus { get; }
        public string[] MenuTexts => Menus.Select(x => Util.MenuTypeToString(x)).ToArray();

        public CreatureBase Owner { get; set; }
        public int selectedMenuIndex { get; protected set; }
        public int selectedItemIndex { get; protected set; }
        public int selectedSkillIndex { get; protected set; }
        public int page { get; protected set; } = 0;
        public int pageSize { get; protected set; } = 5;

        public UIBase()
        {
        }
        public UIBase(CreatureBase owner)
        {
            Owner = owner;
        }

        public abstract void Show(CreatureBase? visitor = null);

        public ConsoleKey InputKey()
        {
            ConsoleKeyInfo info = Console.ReadKey(intercept: true);
            return info.Key;
        }

        public void NextPage(int total)
        {
            page = Math.Min(page + 1, (total - 1) / pageSize);
        }
        public void PrevPage()
        {
            page = Math.Max(page - 1, 0);
        }

        public void SelectMenu(ConsoleKey key)
        {
            if (key == Defines.RIGHT_KEY)
                selectedMenuIndex = (selectedMenuIndex + 1) % Menus.Length;
            else if (key == Defines.LEFT_KEY)
                selectedMenuIndex = (selectedMenuIndex - 1 + Menus.Length) % Menus.Length;
        }
        public void ShowAllMenus()
        {
            TextManager.HWriteItems(MenuTexts, selectedMenuIndex);
        }
        public Defines.MenuType GetSelectedMenu()
        {
            return Menus[selectedMenuIndex];
        }
        public void SelectItem(ConsoleKey key, ItemBase[] items)
        {
            ItemBase[] takeItems = items.Skip(page * pageSize).Take(pageSize).ToArray();

            if (key == Defines.UP_KEY)
                selectedItemIndex = (selectedItemIndex - 1 + takeItems.Length) % takeItems.Length;
            else if (key == Defines.DOWN_KEY)
                selectedItemIndex = (selectedItemIndex + 1) % takeItems.Length;
        }

        public void ShowNpcItems(ItemBase[] items, ItemInventory? visitorInventory = null)
        {
            ItemBase[] takeItems = items.Skip(page * pageSize).Take(pageSize).ToArray();
            if (selectedItemIndex > takeItems.Length - 1)
                selectedItemIndex = takeItems.Length - 1;
            int emptySlotCount = visitorInventory?.ItemTotalCount() ?? 0;
            int inventorySlotCount = visitorInventory?.Items.Length ?? 0;
            string pageText = $"[아이템 목록 (페이지: {page + 1}/{(items.Length - 1) / pageSize + 1}) | 인벤토리 슬롯 수: {emptySlotCount}/{inventorySlotCount}]";
            List<string[]> texts = new List<string[]>();
            foreach (ItemBase item in takeItems)
            {
                if (item == null)
                {
                    continue;
                }

                string name = item.Name;
                string hasItem = "";
                string job = "전체";
                string gold = item.Price + "G";
                string desc = item.Description;

                switch (item?.ItemType)
                {
                    case Defines.ItemType.Consumable:
                        ConsumableItem consumableItem = (ConsumableItem)item;
                        hasItem = $"{(visitorInventory?.HasItemCount(consumableItem) ?? 0)}개";
                        break;
                    case Defines.ItemType.Equipment:
                        EquipmentItem equipmentItem = (EquipmentItem)item;
                        name = (visitorInventory?.IsEquipped(item) ?? false) ? $"[E]{name}" : name;
                        hasItem = (visitorInventory?.HasItem(equipmentItem) ?? false) ? "보유" : "미보유";
                        job = Util.HeroTypeToString(equipmentItem.UseableHero);
                        break;
                    case Defines.ItemType.Etc:
                        EtcItem etcItem = (EtcItem)item;
                        hasItem = $"{(visitorInventory?.HasItemCount(etcItem) ?? 0)}개";
                        break;
                }
                texts.Add([name, job, hasItem, gold, desc]);
            }
            TextManager.VWriteItems(texts.ToArray(), pageText, selectedItemIndex);
        }
        public void ShowInventoryItems(ItemInventory? visitorInventory)
        {
            if (visitorInventory == null) return;

            ItemBase[] takeItems = visitorInventory.Items.Skip(page * pageSize).Take(pageSize).ToArray();
            if (selectedItemIndex > takeItems.Length - 1)
                selectedItemIndex = takeItems.Length - 1;

            int emptySlotCount = visitorInventory.ItemTotalCount();
            int inventorySlotCount = visitorInventory.Items.Length;
            string pageText = $"[아이템 목록 (페이지: {page + 1}/{(inventorySlotCount - 1) / pageSize + 1})] | 인벤토리 슬롯 수: {emptySlotCount}/{inventorySlotCount}]";
            List<string[]> texts = new List<string[]>();
            foreach (ItemBase item in takeItems)
            {
                if (item == null)
                {
                    texts.Add([""]);
                    continue;
                }

                string name = item.Name;
                string hasItem = "";
                string job = "전체";
                //string gold = item.Price + "G";
                string desc = item.Description;

                switch (item?.ItemType)
                {
                    case Defines.ItemType.Consumable:
                        ConsumableItem consumableItem = (ConsumableItem)item;
                        hasItem = $"{(visitorInventory?.HasItemCount(consumableItem) ?? 0)}개";
                        break;
                    case Defines.ItemType.Equipment:
                        EquipmentItem equipmentItem = (EquipmentItem)item;
                        name = equipmentItem.IsEquipped ? $"[E]{name}" : name;
                        hasItem = (visitorInventory?.HasItem(equipmentItem) ?? false) ? "보유" : "미보유";
                        job = Util.HeroTypeToString(equipmentItem.UseableHero);
                        break;
                    case Defines.ItemType.Etc:
                        EtcItem etcItem = (EtcItem)item;
                        hasItem = $"{(visitorInventory?.HasItemCount(etcItem) ?? 0)}개";
                        break;
                }
                texts.Add([name, job, hasItem, desc]);

            }
            TextManager.VWriteItems(texts.ToArray(), pageText, selectedItemIndex);
        }
        public ItemBase GetSelectedItem(ItemBase[] items)
        {
            int index = (page * pageSize) + selectedItemIndex;

            return items[index];
        }

        public void SelectSkill(ConsoleKey key, Skill[] skills)
        {
            if (key == Defines.UP_KEY)
                selectedSkillIndex = (selectedSkillIndex - 1 + skills.Length) % skills.Length;
            else if (key == Defines.DOWN_KEY)
                selectedSkillIndex = (selectedSkillIndex + 1) % skills.Length;
        }
        public void ShowSkills(Skill[] skills)
        {
            string[]?[] skillTexts = new string[skills.Length][];
            for (int i = 0; i < skills.Length; i++)
            {
                Skill skill = skills[i];
                skillTexts[i] = new string[] { skill.Name,
                    Owner.Level >= skill.RequiredLevel ? $"Lv.{skill.RequiredLevel}" : "레벨 부족",
                    $"{skill.CurrentCastCount}/{skill.MaxCastCount}회 남음",
                    skill.Description };
            }
            TextManager.VWriteItems(skillTexts, selectedSkillIndex);
        }
        public Skill? GetSelectedSkill(Skill[] skills)
        {
            if(selectedSkillIndex < 0 || selectedSkillIndex >= skills.Length)
                return null;

            return skills[selectedSkillIndex];
        }

        public void ShowGold(ItemInventory? inventory)
        {
            TextManager.MenuWriteLine($"보유 골드 : {inventory?.Gold ?? 0} G");
        }
    }
}
