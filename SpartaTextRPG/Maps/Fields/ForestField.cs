using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class ForestField : MapBase
    {
        public override string Name => "초보자의 숲";

        public override string Description => "초보자들을 위한 사냥터 입니다.";

        public override Defines.MapType MapType => Defines.MapType.ForestField;

        public override Npc[] Npcs { get; protected set; }

        public override int[][] MapData { get; protected set; } = new int[][]
        {
            [ 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 81, 81, 81, 81, 81, 81, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 72, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 81, 81, 81, 81, 81, 81, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 51, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 51, 41, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 51, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 72, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 72, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 52 ],
            [ 91, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 42, 52 ],
            [ 91, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 52 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 72, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 72, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 72, 11, 91 ],
            [ 91, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 81, 81, 81, 81, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 43, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 53, 53, 53, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91 ],
        };

        public override int[] MonsterIds => [ 3101, 3102, 3103 ];
        public override int[] NpcIds => [];
        public override int BossId => 0;

        public ForestField()
        {
            MapTile[] maps = GenerateMap(MapData);
            MapTiles = maps;
        }

        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    GameManager.Instance.EnterWorld<NewbieTown>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit2:
                    TextManager.LWriteLine("다음 마을은 구현되지 않았습니다.");
                    GameManager.Instance.EnterWorld<NewbieTown>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit3:
                    GameManager.Instance.EnterWorld<CaveDungeon>(Defines.TileType.Enter3);
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }


        protected override void Job()
        {
            if (Visitor?.CurrentPosition == null) return;
            base.Job();

            while (true)
            {
                DrawMap();
                // 키 입력 받고 움직여야함.

                ConsoleKey key = Console.ReadKey(intercept: true).Key;
                Vector2Int? direction = null;
                if (key == Defines.UP_KEY)
                {
                    direction = new Vector2Int(0, -1);
                }
                else if (key == Defines.DOWN_KEY)
                {
                    direction = new Vector2Int(0, 1);
                }
                else if (key == Defines.LEFT_KEY)
                {
                    direction = new Vector2Int(-1, 0);
                }
                else if (key == Defines.RIGHT_KEY)
                {
                    direction = new Vector2Int(1, 0);
                }
                else if (key == Defines.CANCEL_KEY)
                {
                    // 설정창 불러오기
                    GameManager.Instance.SleepWorld(this);
                    GameManager.Instance.PlayerMenu();
                    return;
                }
                else if (key == Defines.ACCEPT_KEY)
                {
                    // Npc 불러오기
                    continue;
                }
                if (direction == null)
                    continue;
                MapTile prevTile = MapTiles.FirstOrDefault(v => v.Position.Compare(Visitor.CurrentPosition));
                Vector2Int? nextPos = Visitor.CurrentPosition + direction;
                MapTile? tile = MapTiles.FirstOrDefault(v => v.Position.Compare(nextPos));
                if (tile == null)
                {
                    TextManager.HWriteLine("잘못된 이동입니다.");
                    continue;
                }
                else if (tile.Value.CanMoveToTile())
                {
                    Visitor.SetPosition(MapType, nextPos);
                    if (tile.Value.IsEventArea())
                        OnEvent(Visitor, prevTile, tile.Value);
                    else if (tile.Value.IsExit())
                    {
                        OnExit(Visitor, prevTile, tile.Value);
                        return;
                    }
                    else if (RandomBattle(5))
                        return;
                }
            }
        }
    }
}
