using SpartaTextRPG.Creatures;
using SpartaTextRPG.Datas;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class CaveDungeon : MapBase
    {
        public override string Name => "초보자의 숲";
        public override string Description => "초보자들을 위한 사냥터 입니다.";
        public override Defines.MapType MapType => Defines.MapType.CaveDungeon;

        public override Npc[] Npcs { get; protected set; }
        public override int[][] MapData { get; protected set; } = new int[][]
        {
            [ 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91 ],
            [ 91, 11, 43, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91 ],
            [ 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 91, 91, 91, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 71, 71, 71, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 91, 91, 91, 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 72, 72, 72, 72, 72, 72, 72, 72, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 91, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 72, 72, 72, 72, 72, 72, 72, 72, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 71, 71, 71, 71, 71, 71, 71, 71, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 81, 81, 11, 11, 11, 81, 81, 11, 11, 91, 71, 71, 71, 71, 71, 71, 71, 71, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 91, 91, 91, 11, 11, 91, 81, 11, 11, 81, 81, 11, 11, 11, 81, 81, 11, 11, 91, 71, 71, 71, 71, 71, 71, 71, 71, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 81, 81, 26, 26, 26, 81, 81, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 81, 26, 26, 26, 26, 26, 81, 11, 11, 91, 11, 11, 11, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 91, 81, 11, 11, 81, 26, 26, 36, 26, 26, 81, 11, 11, 91, 11, 11, 11, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 91, 91, 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 81, 81, 81, 81, 81, 81, 81, 11, 11, 91, 11, 11, 11, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 81, 81, 91, 91, 11, 11, 91, 11, 11, 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 81, 91, 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 91, 11, 11, 11, 11, 11, 91, 11, 11, 11, 11, 11, 91, 81, 81, 91, 91, 11, 11, 91, 11, 11, 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 11, 11, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 72, 72, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 72, 72, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 11, 91 ],
            [ 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 91, 53, 53, 53, 91, 91 ],

        };
        public override int[] MonsterIds => [3201, 3202, 3203, 3301, 3401, 3402];
        public override int[] NpcIds => [];
        public override int BossId => 3501;

        public CaveDungeon()
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
                    break;
                case Defines.TileType.Exit2:
                    break;
                case Defines.TileType.Exit3:
                    GameManager.Instance.EnterWorld<ForestField>(Defines.TileType.Enter3);
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

                MapTile prevTile = MapTiles.FirstOrDefault(v => v.Position.Compare(Visitor.CurrentPosition));
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
                    if (prevTile.TileType == Defines.TileType.BossEvent)
                    {
                        // Boss와 전투하기
                        if (DataManager.Instance.MonsterDict.TryGetValue(BossId, out MonsterData data) == false)
                        {
                            TextManager.HWriteLine("보스 정보를 찾을 수 없습니다.");
                            continue;
                        }
                        string alert = Defines.LAST_BOSS_ID == data.DataId 
                            ? $"{data.Name}를 물리치면 게임이 종료됩니다. 보스와 전투를 진행하시겠습니까?" 
                            : "보스와 전투를 진행하시겠습니까?";
                        if (TextManager.Confirm(alert))
                        {
                            GameManager.Instance.BattleStart(this, [data.DataId]);
                            return;
                        }
                    }
                    continue;
                }
                if (direction == null)
                    continue;
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

        public override void OnEvent(CreatureBase visitor, MapTile prevTile, MapTile nextTile)
        {
            if (prevTile.TileType == nextTile.TileType)
                return;

            if (nextTile.TileType != Defines.TileType.BossEvent)
                base.OnEvent(visitor, prevTile, nextTile);

            if (DataManager.Instance.MonsterDict.TryGetValue(BossId, out MonsterData data) == false)
            {
                TextManager.HWriteLine("보스 정보를 찾을 수 없습니다.");
                return;
            }

            TextManager.LWriteLine($"{data.Name} 보스에게 도달했습니다. {Util.KeyString(Defines.ACCEPT_KEY)} 키를 눌러 보스와 전투를 할 수 있습니다.");
        }
    }
}
