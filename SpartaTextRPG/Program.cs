using SpartaTextRPG.Creatures;
using SpartaTextRPG.Managers;
using SpartaTextRPG.UIs;
using SpartaTextRPG.Utils;

namespace SpartaTextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;
            Console.CursorVisible = false;


            DataManager.Instance.LoadData();
            TextManager.Init();
            TextManager.SystemWriteLine($"기본 조작 방법 - 이동 : {Util.KeyString(Defines.UP_KEY)},{Util.KeyString(Defines.DOWN_KEY)},{Util.KeyString(Defines.LEFT_KEY)},{Util.KeyString(Defines.RIGHT_KEY)} 키, 대화/선택 : {Util.KeyString(Defines.ACCEPT_KEY)} 키, 메뉴/취소 : {Util.KeyString(Defines.CANCEL_KEY)} 키");
            JobManager.Instance.Push(UIManager.Instance.GameTitle);

            while (!JobManager.Instance.IsExit)
            {
                JobManager.Instance.Flush();
            }
        }
    }
}
