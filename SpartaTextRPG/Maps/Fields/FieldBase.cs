using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public abstract class FieldBase : MapBase
    {
        public FieldBase()
        {
            MapTile[] maps = GenerateMap(MapData);
            MapTiles = maps;
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
