using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Maps;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Managers
{
    public class GameManager
    {
        private static GameManager? _instance = null;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameManager();

                return _instance;
            }
        }

        public Hero? Hero { get; private set; }
        public MapBase? EnterdMap { get; private set; }
        public Defines.MapType SavedRecallPoint { get; private set; }
        public void CreateHero(string name, Defines.JobType job)
        {
            Hero = new Hero();
            Hero.SetInfo(job, name);
            SavedRecallPoint = Defines.MapType.NewbieTown;
        }

        public void LoadHero(SaveHeroData data)
        {
            Hero = new Hero();
            Hero.SetInfo(data);
            SavedRecallPoint = data.RecallPoint;
        }
        public void EnterWorld(Defines.MapType mapType, Vector2Int pos)
        {
            if (mapType == Defines.MapType.None)
            {
                TextManager.ErrorWriteLine("맵 정보가 없습니다.");
                JobManager.Instance.Push(UIManager.Instance.GameTitle);
                return;
            }
            JobManager.Instance.Push(() =>
            {
                string className = "SpartaTextRPG.Maps." + mapType.ToString();
                Type type = Type.GetType(className);
                MapBase map = Activator.CreateInstance(type) as MapBase;
                map.Enter(Hero, pos);
            });
        }
        public void EnterWorld(Defines.MapType mapType, Defines.TileType enterType)
        {
            JobManager.Instance.Push(() =>
            {
                string className = "SpartaTextRPG.Maps." + mapType.ToString();
                Type type = Type.GetType(className);
                MapBase map = Activator.CreateInstance(type) as MapBase;
                map.Enter(Hero, enterType);
            });
        }
        public void EnterWorld<T>(Defines.TileType enterType = Defines.TileType.Enter1) where T : MapBase, new()
        {
            JobManager.Instance.Push(() =>
            {
                MapBase map = new T();
                map.Enter(Hero, enterType);
            });
        }

        public void SleepWorld<T>(T map) where T : MapBase
        {
            EnterdMap = map;
        }

        public void WakeUpWorld()
        {
            if (EnterdMap != null)
            {
                TextManager.Flush();
                JobManager.Instance.Push(EnterdMap.WakeUp);
                EnterdMap = null;
            }
        }

        public void PlayerMenu()
        {
            if (Hero == null)
            {
                TextManager.ErrorWriteLine("플레이어 정보가 없습니다.");
                return;
            }

            UIManager.Instance.ShowPlayerMenu(Hero);
        }

        public void BattleStart<T>(T map, int[] monsterIds) where T : MapBase
        {
            EnterdMap = map;
            // TODO: 전투 시작
            TextManager.SystemWriteLine("몬스터와 조우 했습니다.");
            TextManager.WriteBattleStart();

            // 몬스터를 생성
            List<Monster> monsters = new List<Monster>();
            foreach (int id in monsterIds)
            {
                if (DataManager.Instance.MonsterDict.TryGetValue(id, out MonsterData data) == false)
                    continue;

                Monster monster = new Monster();
                monster.SetInfo(data);
                monsters.Add(monster);
            }
            // 전투 UI 표시
            UIManager.Instance.ShowBattleStart(Hero, monsters.ToArray());
        }

        public void SaveRecallPoint(Defines.MapType mapType)
        {
            SavedRecallPoint = mapType;
            TextManager.SystemWriteLine("귀환지를 저장하였습니다.");
        }
        public void Recall()
        {
            EnterWorld(SavedRecallPoint, Defines.TileType.RecallPoint);
        }
        public void GameEnd()
        {
            TextManager.Confirm(
                "엄청난 위협으로 세상을 구해냈습니다.\n"
                + "제작자가 보스를 추가하지 않는 한 세상은 평화로울 것입니다.\n"
                + "더이 상 진행하지 않고 게임을 종료하시겠습니까?", () =>
            {
                EnterdMap = null;
                TextManager.SystemWriteLine("타이틀 화면으로 돌아갑니다.");
                JobManager.Instance.Push(UIManager.Instance.GameTitle);
            });
        }
        public void GameExit()
        {
            TextManager.Confirm("정말로 종료하시겠습니까?", () =>
            {
                EnterdMap = null;
                TextManager.SystemWriteLine("타이틀 화면으로 돌아갑니다.");
                JobManager.Instance.Push(UIManager.Instance.GameTitle);
            });
        }

        public void GameOver()
        {
            TextManager.SystemWriteLine("플레이어가 사망하여 귀환지로 이동합니다.");
            EnterdMap = null;
            EnterWorld(SavedRecallPoint, Defines.TileType.RecallPoint);
        }

    }
}
