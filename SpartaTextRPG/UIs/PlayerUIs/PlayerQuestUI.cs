using SpartaTextRPG.Creatures;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.PlayerUIs
{
    public class PlayerQuestUI : UIBase
    {
        public override Defines.MenuType[] Menus => throw new NotImplementedException();

        public PlayerQuestUI(CreatureBase player) : base(player)
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            throw new NotImplementedException();
        }
    }
}
