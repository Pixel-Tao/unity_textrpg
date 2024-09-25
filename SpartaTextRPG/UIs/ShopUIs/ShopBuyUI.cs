using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
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
    public class ShopBuyUI : UIBase
    {
        public ShopBuyUI(CreatureBase owner) : base(owner)
        {
        }

        public override Defines.MenuType[] Menus => [
            Defines.MenuType.BuyItem,
            Defines.MenuType.Next,
            Defines.MenuType.Prev,
            Defines.MenuType.Exit,
        ];

        public override void Show(CreatureBase? visitor)
        {
            if (visitor == null) return;

            Npc npc = (Npc)Owner;
            ItemBase[] saleItems = npc.SaleItems.ToArray();
            string message = npc.Message;

            while (true)
            {
                TextManager.Flush();
                TextManager.MenuWriteLine(npc.Message);
                ShowAllMenus();
                ShowGold(visitor.Inventory);
                ShowNpcItems(saleItems, visitor.Inventory);

                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectItem(key, saleItems);

                if (key == Defines.ACCEPT_KEY)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.BuyItem:
                            ItemBase[] items = npc.SaleItems.ToArray();
                            if (items.Length == 0)
                            {
                                TextManager.MWriteLine("구매 가능한 아이템이 없습니다.");
                                break;
                            }

                            ItemBase item = GetSelectedItem(items);
                            if (CanBuy(visitor, item) == false)
                                continue;

                            if (DataManager.Instance.ItemDict.TryGetValue(item.DataId, out ItemData itemData) == false)
                            {
                                TextManager.HWriteLine("아이템 데이터를 찾지 못했습니다.");
                                continue;
                            }

                            visitor.Inventory?.RemoveGold(item.Price);
                            visitor.Inventory?.AddItem(itemData);
                            TextManager.LWriteLine($"{item.Name}을 구매했습니다.");
                            break;
                        case Defines.MenuType.Exit:
                            TextManager.LWriteLine("상점을 나갑니다.");
                            GameManager.Instance.WakeUpWorld();
                            return;
                        case Defines.MenuType.Next:
                            NextPage(npc.SaleItems.Count);
                            break;
                        case Defines.MenuType.Prev:
                            PrevPage();
                            break;
                    }
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    TextManager.LWriteLine("상점을 나갑니다.");
                    GameManager.Instance.WakeUpWorld();
                    return;
                }
            }
        }

        bool CanBuy(CreatureBase visitor, ItemBase item)
        {
            if (visitor.Inventory.Gold < item.Price)
            {
                TextManager.MWriteLine("골드가 부족합니다.");
                return false;
            }
            else if (visitor.Inventory.CanTake(item))
            {
                return false;
            }

            return true;
        }
    }
}
