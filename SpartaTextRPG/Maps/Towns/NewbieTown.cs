using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class NewbieTown : TownBase
    {
        public override string Name => "초보자 마을";
        public override string Description => "초보자들을 위한 마을입니다.";
        public override Defines.MapType MapType => Defines.MapType.NewbieTown;

        public override int[] NpcIds => new int[] { 1101, 1104, 1107, 1110, 1113 };
        public override int[] MonsterIds => [];
        public override int BossId => 0;

        public override Npc[] Npcs { get; protected set; }

        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    GameManager.Instance.EnterWorld<ForestField>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit2:
                    break;
                case Defines.TileType.Exit3:
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }
    }
}
