using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class RuinDungeon : DungeonBase
    {
        public override string Name => "폐허 던전";
        public override string Description => "실력이 어느정도 되는 사람들이 도전 할 수 있다.";
        public override Defines.MapType MapType => Defines.MapType.RuinDungeon;

        public override Npc[] Npcs { get; protected set; }

        public override int[] MonsterIds => [3203, 3301, 3401, 3402];
        public override int[] NpcIds => [];
        public override int BossId => 3502;

        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    break;
                case Defines.TileType.Exit2:
                    GameManager.Instance.EnterWorld<DesertField>(Defines.TileType.Enter2);
                    break;
                case Defines.TileType.Exit3:
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }
    }
}
