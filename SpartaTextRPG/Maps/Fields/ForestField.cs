using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class ForestField  : FieldBase
    {
        public override string Name => "초보자의 숲";

        public override string Description => "초보자들을 위한 사냥터 입니다.";

        public override Defines.MapType MapType => Defines.MapType.ForestField;

        public override Npc[] Npcs { get; protected set; }

        public override int[] MonsterIds => [3101, 3102];
        public override int[] NpcIds => [];
        public override int BossId => 0;


        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    GameManager.Instance.EnterWorld<NewbieTown>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit2:
                    GameManager.Instance.EnterWorld<MidTown>(Defines.TileType.Enter2);
                    break;
                case Defines.TileType.Exit3:
                    GameManager.Instance.EnterWorld<CaveDungeon>(Defines.TileType.Enter3);
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }
    }
}
