using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.UIs.GameUIs
{
    public class GameStartUI : UIBase
    {
        public override Defines.MenuType[] Menus => [];

        public override void Show(CreatureBase? visitor = null)
        {
            TextManager.SystemWriteLine("새로운 캐릭터를 생성합니다.");
            TextManager.SystemWriteLine("안내에 따라 키를 입력해 주세요.");

            TextManager.Flush();
            TextManager.InfoWriteLine("캐릭터의 이름을 입력해 주세요.");
            TextManager.InputWrite("이름 : ");
            string? name = "";
            do
            {
                name = TextManager.InputReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    break;

                TextManager.SystemWriteLine("잘못된 이름입니다. 다시 입력해 주세요.");
            } while (true);
            TextManager.SystemWriteLine($"이름 : {name}");

            Defines.JobType[] heroTypes = [
                Defines.JobType.Warrior,
                Defines.JobType.Archer,
                Defines.JobType.Mage,
                Defines.JobType.Thief
            ];
            string[] jobTexts = heroTypes.Select(x => Util.HeroTypeToString(x)).ToArray();
            int selectedHeroIndex = 0;

            JobData data = DataManager.Instance.JobDict[heroTypes[selectedHeroIndex]];

            while (true)
            {
                TextManager.Flush();
                TextManager.InfoWriteLine("캐릭터의 직업을 선택해 주세요.");
                TextManager.InfoWriteLine(data.Description);
                TextManager.HWriteItems(jobTexts, selectedHeroIndex);
                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                if (key == Defines.RIGHT_KEY)
                    selectedHeroIndex = (selectedHeroIndex + 1) % heroTypes.Length;
                else if (key == Defines.LEFT_KEY)
                    selectedHeroIndex = (selectedHeroIndex - 1 + heroTypes.Length) % heroTypes.Length;
                else if (key == Defines.ACCEPT_KEY)
                    break;

                data = DataManager.Instance.JobDict[heroTypes[selectedHeroIndex]];
            }

            TextManager.Flush();
            TextManager.SystemWriteLine($"직업 : {data.Name}");
            TextManager.SystemWriteLine("월드 진입을 기다리는 중...");
            Thread.Sleep(500);
            TextManager.SystemWriteLine("캐릭터 생성이 완료되었습니다.");
            Thread.Sleep(500);
            TextManager.SystemWriteLine("게임을 즐겨주세요!");

            // 캐릭터 생성
            GameManager.Instance.CreateHero(name, heroTypes[selectedHeroIndex]);
            GameManager.Instance.EnterWorld<NewbieTown>();
        }
    }
}
