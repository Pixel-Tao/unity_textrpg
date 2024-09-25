using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class MidTown : TownBase
    {
        public override string Name => "중급자 마을";
        public override string Description => "중급자들을 위한 마을입니다.";
        public override Defines.MapType MapType => Defines.MapType.MidTown;

        public override int[] NpcIds => new int[] { 1102, 1105, 1108, 1111, 1113 };
        public override int[] MonsterIds => [];
        public override int BossId => 0;

        public override Npc[] Npcs { get; protected set; }

        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    GameManager.Instance.EnterWorld<DesertField>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit2:
                    GameManager.Instance.EnterWorld<ForestField>(Defines.TileType.Enter2);
                    break;
                case Defines.TileType.Exit3:
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }
    }
}
