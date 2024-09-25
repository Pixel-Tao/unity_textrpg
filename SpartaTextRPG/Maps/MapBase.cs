using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Maps
{
    public abstract class MapBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract Defines.MapType MapType { get; }
        public abstract Npc[] Npcs { get; protected set; }
        public abstract int[][] MapData { get; protected set; }
        public abstract int[] MonsterIds { get; }
        public abstract int[] NpcIds { get; }
        public abstract int BossId { get; }

        public CreatureBase? Visitor { get; private set; }
        public MapTile[] MapTiles { get; protected set; } = [];

        private Random rand = new Random();

        protected MapTile[] GenerateMap(int[][] data)
        {
            List<MapTile> tiles = new List<MapTile>();
            for (int y = 0; y < data.Length; y++)
            {
                for (int x = 0; x < data[y].Length; x++)
                {
                    Defines.TileType tileType = (Defines.TileType)data[y][x];

                    tiles.Add(new MapTile(x, y, tileType));
                }
            }

            return tiles.ToArray();
        }
        public virtual void Enter(CreatureBase? visitor, Vector2Int? pos)
        {
            if (visitor == null)
            {
                TextManager.ErrorWriteLine($"접근할 수 없습니다.");
                return;
            }

            Visitor = visitor;
            Visitor?.SetPosition(MapType, pos);

            TextManager.SystemWriteLine($"{visitor.Name}님이 {Name}에 입장하였습니다.");
            TextManager.CurrentSite(MapType, Name, Description);

            JobManager.Instance.Push(Job);
        }
        public virtual void Enter(CreatureBase? visitor, Defines.TileType enterType = Defines.TileType.Enter1)
        {
            MapTile? tile = MapTiles?.FirstOrDefault(s => s.TileType == enterType);
            Enter(visitor, tile?.Position);
        }
        public virtual void WakeUp()
        {
            JobManager.Instance.Push(Job);
            TextManager.SystemWriteLine("월드로 돌아왔습니다.");
        }
        public virtual void Leave(Defines.TileType tileType)
        {
            TextManager.Flush();
            TextManager.SystemWriteLine($"{Name}에서 떠났습니다.");
        }

        protected void DrawMap()
        {
            TextManager.Flush();
            Hero hero = Visitor as Hero;
            int y = 0;
            bool flag = false;
            foreach (var tile in MapTiles)
            {
                flag = false;
                if (tile.Position.Y > y)
                {
                    TextManager.WriteLine();
                    y++;
                }

                if (tile.Position.Compare(hero?.CurrentPosition))
                {
                    TextManager.HeroMapWrite(hero.JobType);
                    flag = true;
                }
                if (flag) continue;
                TextManager.MapWrite(tile.TileType);
            }
            TextManager.WriteLine();
        }

        protected virtual void Job()
        {
            TextManager.SystemWriteLine($"기본 조작 방법 - 이동 : {Util.KeyString(Defines.UP_KEY)},{Util.KeyString(Defines.DOWN_KEY)},{Util.KeyString(Defines.LEFT_KEY)},{Util.KeyString(Defines.RIGHT_KEY)} 키, 대화/선택 : {Util.KeyString(Defines.ACCEPT_KEY)} 키, 메뉴/취소 : {Util.KeyString(Defines.CANCEL_KEY)} 키");
        }

        public virtual void OnEvent(CreatureBase visitor, MapTile prevTile, MapTile nextTile)
        {
            if (prevTile.TileType == nextTile.TileType)
                return;

            Npc? npc = Npcs.FirstOrDefault(x => x.TileType == nextTile.TileType);
            string shopName = npc?.Name ?? "알 수 없음";

            TextManager.SystemWriteLine($"{shopName}에게 방문했습니다. {Util.KeyString(Defines.ACCEPT_KEY)} 키를 눌러 대화를 할 수 있습니다.");
        }

        public void OnRecallPoint(CreatureBase visitor, MapTile prevTile, MapTile nextTile)
        {
            if (prevTile.TileType == nextTile.TileType)
                return;

            TextManager.SystemWriteLine($"귀환지에 도착했습니다. {Util.KeyString(Defines.ACCEPT_KEY)} 키를 눌러 귀환지를 설정 할 수 있습니다.");
        }

        public void OnExit(CreatureBase visitor, MapTile prevTile, MapTile nextTile)
        {
            if (prevTile.TileType == nextTile.TileType)
                return;

            Leave(nextTile.TileType);
        }

        
        protected bool RandomBattle(float percent = 10)
        {
            // 필드, 던전에서 이동중에 확률로 전투를 진행하도록 함.

            if (Visitor == null) return false;

            // percent 확률로 전투
            if (rand.Next(0, 100) < percent)
            {
                // 전투 시작
                GameManager.Instance.BattleStart(this, GetRandomMonsters());
                return true;
            }

            return false;
        }

        protected int[] GetRandomMonsters()
        {
            int monsterCount = rand.Next(1, MonsterIds.Length + 1);

            int[] randMonsterIds = new int[monsterCount];
            for (int i = 0; i < monsterCount; i++)
            {
                randMonsterIds[i] = MonsterIds[rand.Next(0, MonsterIds.Length)];
            }

            return randMonsterIds;
        }

    }
}
