using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.GameUIs
{
    public class GameSaveUI : UIBase
    {
        public override Defines.MenuType[] Menus => throw new NotImplementedException();

        public GameSaveUI()
        {
        }

        public override void Show(CreatureBase? visitor = null)
        {
            TextManager.SystemWriteLine("게임을 저장합니다. 잠시만 기다려주세요.");
            Thread.Sleep(500);
            SaveManager.Instance.Save();
            TextManager.SystemWriteLine("게임 저장이 완료되었습니다.");
        }
    }
}
