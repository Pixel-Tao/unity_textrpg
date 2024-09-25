using SpartaTextRPG.Creatures;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.PlayerUIs
{
    public class PlayerInventoryUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.UseOrEquip,
            Defines.MenuType.Next,
            Defines.MenuType.Prev,
            Defines.MenuType.Sort,
            Defines.MenuType.Drop,
            Defines.MenuType.Back,
        ];

        public PlayerInventoryUI(CreatureBase player) : base(player)
        {
            pageSize = 10;
        }

        public override void Show(CreatureBase? visitor = null)
        {
            Hero hero = Owner as Hero;
            if (hero == null)
            {
                TextManager.HWriteLine("Hero is null");
                return;
            }

            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();
                ShowGold(hero.Inventory);
                ShowInventoryItems(hero.Inventory);
                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectItem(key, hero.Inventory.Items);
                if (Defines.ACCEPT_KEY == key)
                {
                    ItemBase item = GetSelectedItem(hero.Inventory.Items);
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.UseOrEquip:
                            // 아이템 사용/장착
                            if (item == null)
                            {
                                TextManager.MWriteLine("아이템이 없습니다.");
                                continue;
                            }
                            if (item.ItemType == Defines.ItemType.Consumable)
                            {
                                ConsumableItem ci = item.CastItem<ConsumableItem>();
                                TextManager.Confirm($"{(ci.IsBuff ? "버프아이템 효과는 하나만 적용할 수 있습니다. " : "")}아이템을 사용하시겠습니까?",
                                () =>
                                {
                                    hero.Consume(ci);
                                });
                            }
                            else if (item.ItemType == Defines.ItemType.Equipment)
                            {
                                EquipmentItem eq = item.CastItem<EquipmentItem>();
                                if (eq.IsEquipped)
                                    hero.Unequip(eq.EquipmentType);
                                else
                                    hero.Equip(eq);
                                break;
                            }

                            break;
                        case Defines.MenuType.Drop:
                            // 아이템 버리기
                            if (item == null)
                            {
                                TextManager.MWriteLine("아이템이 없습니다.");
                                continue;
                            }
                            TextManager.Confirm("아이템을 버리시겠습니까?", () =>
                            {
                                if (item.ItemType == Defines.ItemType.Equipment && item.CastItem<EquipmentItem>().IsEquipped)
                                {
                                    TextManager.MWriteLine("장착중인 아이템은 버릴 수 없습니다.");
                                    return;
                                }
                                hero.Inventory.RemoveItem(item);
                            });
                            break;
                        case Defines.MenuType.Next:
                            NextPage(hero.Inventory.Items.Length);
                            break;
                        case Defines.MenuType.Prev:
                            PrevPage();
                            break;
                        case Defines.MenuType.Sort:
                            // 아이템 정렬
                            hero.Inventory.SortItems();
                            break;
                        case Defines.MenuType.Back:
                            TextManager.Flush();
                            return;
                    }
                }
                else if(key == Defines.CANCEL_KEY)
                {
                    TextManager.Flush();
                    return;
                }
            }
        }
    }
}
