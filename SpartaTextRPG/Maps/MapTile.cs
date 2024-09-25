using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public struct Vector2Int
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool Compare(Vector2Int? other)
        {
            return other != null && this.X == other?.X && this.Y == other?.Y;
        }
        public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new Vector2Int(a.X + b.X, a.Y + b.Y);
        public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new Vector2Int(a.X - b.X, a.Y - b.Y);
    }

    public struct MapTile
    {
        public Vector2Int Position { get; private set; }

        public Defines.TileType TileType { get; set; } = Defines.TileType.None;
        public MapTile(int x, int y, Defines.TileType type)
        {
            this.Position = new Vector2Int(x, y);
            this.TileType = type;
        }

        public bool CanMoveToTile()
        {
            return Util.CanMoveToTile(TileType);
        }
        public bool IsEventArea()
        {
            switch (TileType)
            {
                case Defines.TileType.ItemShopEvent:
                case Defines.TileType.WeaponShopEvent:
                case Defines.TileType.ArmorShopEvent:
                case Defines.TileType.AccessoryShopEvent:
                case Defines.TileType.InnEvent:
                    return true;

                default:
                    return false;
            }
        }
        public bool IsRecallPoint()
        {
            return TileType == Defines.TileType.RecallPoint;
        }
        public bool IsExit()
        {
            switch (TileType)
            {
                case Defines.TileType.Exit1:
                case Defines.TileType.Exit2:
                case Defines.TileType.Exit3:
                case Defines.TileType.Exit4:
                    return true;

                default:
                    return false;
            }
        }
    }
}
