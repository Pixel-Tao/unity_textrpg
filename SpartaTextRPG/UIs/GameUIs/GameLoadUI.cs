using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.UIs.GameUIs
{
    public class GameLoadUI : UIBase
    {
        public override Defines.MenuType[] Menus => throw new NotImplementedException();

        public override void Show(CreatureBase? visitor = null)
        {
            TextManager.SystemWriteLine("게임을 불러옵니다. 잠시만 기다려주세요.");
            Thread.Sleep(500);
            SaveHeroData data = SaveManager.Instance.Load();
            if (data == null)
            {
                TextManager.ErrorWriteLine("저장된 데이터가 없습니다.");
                return;
            }

            TextManager.Flush();
            GameManager.Instance.LoadHero(data);
            TextManager.SystemWriteLine("게임 불러오기가 완료되었습니다.");
            GameManager.Instance.EnterWorld(data.MapType, data.Position);
        }
    }
}
