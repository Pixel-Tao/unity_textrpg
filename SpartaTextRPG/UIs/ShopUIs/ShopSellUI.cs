using SpartaTextRPG.Creatures;
using SpartaTextRPG.Items;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.ShopUIs
{
    public class ShopSellUI : UIBase
    {
        public ShopSellUI(CreatureBase owner) : base(owner)
        {
        }

        public override Defines.MenuType[] Menus => [
            Defines.MenuType.SellItem,
            Defines.MenuType.Next,
            Defines.MenuType.Prev,
            Defines.MenuType.Exit,
        ];

        public override void Show(CreatureBase? visitor)
        {
            if (visitor == null) return;

            ItemBase[] items = visitor.Inventory.Items;
            Npc npc = (Npc)Owner;
            string message = npc.Message;
            while (true)
            {
                TextManager.Flush();
                TextManager.MenuWriteLine(message);
                ShowAllMenus();
                ShowGold(visitor.Inventory);
                ShowInventoryItems(visitor.Inventory);

                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectItem(key, items);
                if (key == Defines.ACCEPT_KEY)
                {
                    // 메뉴 선택
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Next:
                            NextPage(items.Length);
                            break;
                        case Defines.MenuType.Prev:
                            PrevPage();
                            break;
                        case Defines.MenuType.SellItem:
                            if (items.Length == 0)
                            {
                                TextManager.WarningWriteLine("판매할 아이템이 없습니다.");
                                return;
                            }

                            ItemBase item = GetSelectedItem(items);
                            if (item == null)
                            {
                                TextManager.WarningWriteLine("아이템을 선택해주세요.");
                                continue;
                            }
                            if (item.ItemType == Defines.ItemType.Equipment && item.CastItem<EquipmentItem>().IsEquipped)
                            {
                                TextManager.WarningWriteLine("장착중인 아이템은 판매할 수 없습니다.");
                                continue;
                            }
                            int price = (int)(item.Price * 0.5);
                            TextManager.Confirm($"{item.Name}을 {price}G에 판매 하시겠습니까?", () =>
                            {
                                // 절반가격에 판매
                                visitor.Inventory.AddGold(price);
                                visitor.Inventory.RemoveItem(item);
                                TextManager.SystemWriteLine($"{item.Name}을 판매하였습니다.");
                            });
                            break;
                        case Defines.MenuType.Exit:
                            TextManager.SystemWriteLine("상점을 나갑니다.");
                            GameManager.Instance.WakeUpWorld();
                            return;
                    }
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    TextManager.SystemWriteLine("상점을 나갑니다.");
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }
    }
}
