using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class CaveDungeon : DungeonBase
    {
        public override string Name => "동굴 던전";
        public override string Description => "초보자들이 도전할 수 있는 던전입니다.";
        public override Defines.MapType MapType => Defines.MapType.CaveDungeon;

        public override Npc[] Npcs { get; protected set; }

        public override int[] MonsterIds => [3101, 3102, 3103];
        public override int[] NpcIds => [];
        public override int BossId => 3501;

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
    }
}
