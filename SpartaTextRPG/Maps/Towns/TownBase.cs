using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class TownBase : MapBase
    {
        public override string Name => "초보자 마을";
        public override string Description => "초보자들을 위한 마을입니다.";
        public override Defines.MapType MapType => Defines.MapType.NewbieTown;

        public override int[][] MapData { get; protected set; } = new int[][]
        {
            [ 91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91],
            [ 91,  91,  91,  91,  91,  91,  11,  91,  91,  91,  91,  91,  11,  11,  11,  11,  11,  11,  11,  91],
            [ 91,  72,  72,  72,  72,  91,  11,  91,  23,  23,  33,  91,  11,  11,  11,  11,  11,  11,  11,  91],
            [ 91,  72,  22,  22,  22,  11,  11,  11,  23,  23,  23,  91,  11,  11,  11,  11,  11,  11,  11,  51],
            [ 91,  72,  22,  32,  22,  11,  11,  11,  11,  11,  91,  91,  11,  11,  11,  11,  11,  11,  41,  51],
            [ 91,  72,  22,  22,  22,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  51],
            [ 91,  91,  91,  91,  91,  11,  11,  11,  11,  11,  11,  11,  11,  71,  11,  11,  11,  11,  11,  91],
            [ 91,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  71,  11,  11,  11,  72,  72,  91],
            [ 91,  11,  71,  71,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  21,  21,  81,  91],
            [ 91,  11,  71,  71,  11,  11,  11,  11,  11,  11,  11,  12,  11,  11,  11,  11,  21,  31,  81,  91],
            [ 91,  11,  11,  11,  11,  11,  11,  11,  71,  71,  11,  11,  11,  11,  11,  11,  21,  21,  81,  91],
            [ 91,  11,  11,  11,  11,  11,  11,  11,  71,  71,  11,  11,  11,  11,  11,  11,  91,  91,  91,  91],
            [ 91,  11,  11,  11,  71,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  71,  71,  11,  11,  91],
            [ 91,  11,  11,  11,  72,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  71,  11,  11,  11,  91],
            [ 91,  11,  11,  11,  11,  11,  11,  11,  11,  11,  11,  91,  91,  11,  11,  91,  91,  11,  11,  91],
            [ 91,  11,  11,  11,  11,  11,  91,  11,  11,  91,  11,  91,  25,  25,  25,  25,  91,  71,  71,  91],
            [ 91,  11,  11,  11,  11,  11,  91,  24,  24,  91,  72,  91,  25,  25,  25,  25,  91,  71,  71,  91],
            [ 91,  11,  11,  11,  11,  11,  91,  24,  34,  91,  72,  91,  91,  91,  35,  91,  91,  71,  71,  91],
            [ 91,  11,  11,  11,  11,  11,  91,  91,  91,  91,  72,  91,  91,  91,  91,  91,  91,  71,  71,  91],
            [ 91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91,  91],
        };

        public override int[] NpcIds => new int[] { 1101, 1104, 1107, 1110, 1113 };
        public override int[] MonsterIds => [];
        public override int BossId => 0;

        public override Npc[] Npcs { get; protected set; }

        public TownBase()
        {
            MapTile[] maps = GenerateMap(MapData);
            MapTiles = maps;

            LoadNpc();
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
                MapTile prevTile = MapTiles.FirstOrDefault(v => v.Position.Compare(Visitor.CurrentPosition));
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
                else if (key == Defines.ACCEPT_KEY && prevTile.IsEventArea())
                {
                    // Npc 불러오기
                    Npc? npc = Npcs.FirstOrDefault(x => x.TileType == prevTile.TileType);
                    if (npc == null)
                    {
                        TextManager.HWriteLine("NPC를 찾을 수 없습니다.");
                        continue;
                    }

                    GameManager.Instance.SleepWorld(this);
                    if (npc.NpcType == Defines.NpcType.InnNpc)
                        UIManager.Instance.ShowShopInn(npc, Visitor);
                    else
                        UIManager.Instance.ShowShopVisit(npc, Visitor);
                    return;
                }
                else if (key == Defines.ACCEPT_KEY && prevTile.IsRecallPoint())
                {
                    GameManager.Instance.SaveRecallPoint(MapType);
                    TextManager.CurrentSite(MapType, GameManager.Instance.SavedRecallPoint, Name, Description);
                    continue;
                }

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
                        // 이벤트 영역에 도달 했다.
                        OnEvent(Visitor, prevTile, tile.Value);
                    else if (tile.Value.TileType == Defines.TileType.RecallPoint)
                        // 귀환지에 도달했다.
                        OnRecallPoint(Visitor, prevTile, tile.Value);
                    else if (tile.Value.IsExit())
                    {
                        // 출구에 도달했다.
                        OnExit(Visitor, prevTile, tile.Value);
                        return;
                    }
                }
            }
        }
    }
}
