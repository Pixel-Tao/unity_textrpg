using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG.Maps
{
    public class DesertField : FieldBase
    {
        public override string Name => "사막 지역";

        public override string Description => "왠만 해서는 살아남기 힘든 지역이다.";

        public override Defines.MapType MapType => Defines.MapType.DesertField;

        public override Npc[] Npcs { get; protected set; }

        public override int[] MonsterIds => [3201, 3202];
        public override int[] NpcIds => [];
        public override int BossId => 0;

        public override void Leave(Defines.TileType exitType)
        {
            base.Leave(exitType);
            switch (exitType)
            {
                case Defines.TileType.Exit1:
                    GameManager.Instance.EnterWorld<MidTown>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit2:
                    GameManager.Instance.EnterWorld<RuinDungeon>(Defines.TileType.Enter2);
                    break;
                case Defines.TileType.Exit3:
                    // 미구현
                    TextManager.MWriteLine("미구현");
                    GameManager.Instance.EnterWorld<MidTown>(Defines.TileType.Enter1);
                    break;
                case Defines.TileType.Exit4:
                    break;

            }
        }
    }
}
