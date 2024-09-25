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

namespace SpartaTextRPG.UIs.BattleUIs
{
    public class BattleItemUI : UIBase
    {
        public override Defines.MenuType[] Menus => [
            Defines.MenuType.Use,
            Defines.MenuType.Next,
            Defines.MenuType.Prev,
            Defines.MenuType.Back,
        ];

        public BattleItemUI(CreatureBase owner) : base(owner)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            while (true)
            {
                TextManager.Flush();
                ShowAllMenus();
                ShowInventoryItems(Owner.Inventory);

                ConsoleKey key = InputKey();
                SelectMenu(key);
                SelectItem(key, Owner.Inventory.Items);
                if (key == Defines.ACCEPT_KEY)
                {
                    switch (Menus[selectedMenuIndex])
                    {
                        case Defines.MenuType.Use:
                            ItemBase? item = GetSelectedItem(Owner.Inventory.Items);
                            if (item == null)
                            {
                                TextManager.WarningWriteLine("아이템을 선택해주세요.");
                                continue;
                            }
                            if (item.ItemType != Defines.ItemType.Consumable)
                            {
                                TextManager.WarningWriteLine("전투중에 사용할 수 없는 아이템입니다.");
                                continue;
                            }
                            // selectedItemIndex 는 SelectMenu 함수에서 선택 했으므로 그냥 return 하면 됨. 
                            if (TextManager.Confirm($"{item.Name} 아이템을 사용하시겠습니까?"))
                                return;
                            else
                                continue;
                        case Defines.MenuType.Next:
                            NextPage(Owner.Inventory.Items.Length);
                            break;
                        case Defines.MenuType.Prev:
                            PrevPage();
                            break;
                        case Defines.MenuType.Back:
                            return;
                    }
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    return;
                }
            }
        }

        public int GetSelectedItemId()
        {
            ItemBase? item = GetSelectedItem(Owner.Inventory.Items);
            if (item == null)
                return 0;

            if (item.ItemType != Defines.ItemType.Consumable)
                return 0;

            return item.DataId;
        }
    }
}
