using SpartaTextRPG.Skills;
using SpartaTextRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaTextRPG.Managers
{
    public static class TextManager
    {
        enum SystemConsoleType
        {
            LowLevel,
            MiddleLevel,
            HighLevel,
        }

        class SystemMessage
        {
            public DateTime Time { get; set; }
            public string Message { get; set; }
            public SystemConsoleType Type { get; set; }
        }

        private static int _writeLineCount = 0;

        private static Queue<SystemMessage> _messageQueue = new Queue<SystemMessage>();
        private static int _maxQueueCount = 8;
        private static int _maxGuideLineCount = 1;
        private static int _siteLineCount = 2;
        public static int FixedLineCount => _maxQueueCount + _maxGuideLineCount + _siteLineCount;
        private static int _endingCreditLineCount = 40;
        private static int _defaultLineCount = 8;

        public static void Init()
        {
            if (_messageQueue.Count == 0)
            {
                _maxQueueCount = _defaultLineCount;
                SystemMessage msg = new SystemMessage();
                msg.Message = "";
                msg.Type = SystemConsoleType.LowLevel;
                msg.Time = DateTime.Now;
                for (int i = 0; i < _maxQueueCount; i++)
                {
                    _messageQueue.Enqueue(msg);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }

                for (int i = 0; i < _maxGuideLineCount; i++)
                    Console.WriteLine(new string('-', Console.WindowWidth));

                CurrentSite(Defines.MapType.None, Defines.MapType.None, "");
            }
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top + FixedLineCount);
        }

        public static void InputWriteLine(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void InputWrite(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write(format, arg);
            Console.ResetColor();
        }
        public static string? InputReadLine()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string? input = Console.ReadLine();
            Console.ResetColor();
            _writeLineCount++;
            return input;
        }

        public static void InfoWriteLine(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void InfoWrite(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void LogoWriteLine(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void LogoWrite(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void BattleWriteLine(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void BattleWrite(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void BattleSelectWriteLine(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void BattleSelectWrite(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void BattleTurnWriteLine(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void BattleTurnWrite(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void MenuWriteLine(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void MenuWrite(string format, params object?[]? arg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void MenuSelectWriteLine(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void MenuSelectWrite(string format, params object?[]? arg)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Write(format, arg);
            Console.ResetColor();
        }

        public static void WriteLine()
        {
            Console.WriteLine();
            _writeLineCount++;
        }
        public static void WriteLine(string format, params object?[]? arg)
        {
            Write(format + "\n", arg);
        }
        public static void Write(string format, params object?[]? arg)
        {
            int prevTop = Console.CursorTop;

            if (format.Contains("\n"))
            {
                string[] lines = format.Split('\n');
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    Console.WriteLine(line, arg);
                    _writeLineCount += Console.CursorTop - prevTop;
                }
                return;
            }

            Console.Write(format, arg);
            if (Console.CursorTop > prevTop)
                _writeLineCount += Console.CursorTop - prevTop;
        }
        public static void Write(char character)
        {
            if (character == '\n')
            {
                Console.WriteLine();
                _writeLineCount++;
                return;
            }

            Console.Write(character);
        }

        public static void MapWrite(Defines.TileType tileType)
        {
            switch (tileType)
            {
                case Defines.TileType.Ground:
                case Defines.TileType.Exit1:
                case Defines.TileType.Exit2:
                case Defines.TileType.Exit3:
                case Defines.TileType.Exit4:
                case Defines.TileType.Enter1:
                case Defines.TileType.Enter2:
                case Defines.TileType.Enter3:
                case Defines.TileType.Enter4:
                case Defines.TileType.ItemShopEvent:
                case Defines.TileType.WeaponShopEvent:
                case Defines.TileType.ArmorShopEvent:
                case Defines.TileType.AccessoryShopEvent:
                case Defines.TileType.InnEvent:
                case Defines.TileType.BossEvent:
                case Defines.TileType.Sand:
                case Defines.TileType.Snow:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('■');
                    break;
                case Defines.TileType.RecallPoint:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('※');
                    break;
                case Defines.TileType.Wall:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write('▦');
                    break;
                case Defines.TileType.Tree:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write('♣');
                    break;
                case Defines.TileType.Rock:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write('◆');
                    break;
                case Defines.TileType.Water:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write('▩');
                    break;
                case Defines.TileType.ItemShopObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('＠');
                    break;
                case Defines.TileType.WeaponShopObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('♠');
                    break;
                case Defines.TileType.ArmorShopObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('▣');
                    break;
                case Defines.TileType.AccessoryShopObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('◎');
                    break;
                case Defines.TileType.InnObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('♨');
                    break;
                case Defines.TileType.BossObject:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write('☎');
                    break;
            }
            Console.ResetColor();
        }

        public static void HeroMapWrite(Defines.JobType heroType)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            switch (heroType)
            {
                case Defines.JobType.Warrior:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Defines.JobType.Archer:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Defines.JobType.Mage:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Defines.JobType.Thief:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
            }
            Console.Write('★');
            Console.ResetColor();
        }

        public static void Flush()
        {
            for (int i = 0; i < _writeLineCount; i++)
            {
                Console.SetCursorPosition(0, Math.Max(Console.CursorTop - 1, FixedLineCount));
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.CursorLeft = 0;
            _writeLineCount = 0;
        }

        public static void LWriteLine(string format, params object?[]? arg)
        {
            SystemMessage message = new SystemMessage();
            message.Type = SystemConsoleType.LowLevel;
            message.Message = string.Format(format, arg);
            message.Time = DateTime.Now;

            if (_messageQueue.Count >= _maxQueueCount)
                _messageQueue.Dequeue();

            _messageQueue.Enqueue(message);

            QueueWriteLine();
        }
        public static void MWriteLine(string format, params object?[]? arg)
        {
            SystemMessage message = new SystemMessage();
            message.Type = SystemConsoleType.MiddleLevel;
            message.Message = string.Format(format, arg);
            message.Time = DateTime.Now;

            if (_messageQueue.Count >= _maxQueueCount)
                _messageQueue.Dequeue();

            _messageQueue.Enqueue(message);

            QueueWriteLine();
        }
        public static void HWriteLine(string format, params object?[]? arg)
        {
            SystemMessage message = new SystemMessage();
            message.Type = SystemConsoleType.HighLevel;
            message.Message = string.Format(format, arg);
            message.Time = DateTime.Now;

            if (_messageQueue.Count >= _maxQueueCount)
                _messageQueue.Dequeue();

            _messageQueue.Enqueue(message);

            QueueWriteLine();
        }

        public static void CurrentSite(Defines.MapType mapType, Defines.MapType recallType, string name, string desc = "")
        {
            int currentTop = Console.CursorTop;
            int currentLeft = Console.CursorLeft;
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;

            Console.SetCursorPosition(0, _maxQueueCount + _maxGuideLineCount);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, _maxQueueCount + _maxGuideLineCount);
            string text = $"현재 위치 : {Util.MapTypeToString(mapType)}";
            if (!string.IsNullOrWhiteSpace(desc))
                text += $" ({desc})";
            text += $" | 귀환지 : {Util.MapTypeToString(recallType)}";
            Console.WriteLine(text);
            Console.ResetColor();
            Console.WriteLine(new string('=', Console.WindowWidth));
        }

        private static void QueueWriteLine()
        {
            Thread.Sleep(100);
            DateTime now = DateTime.Now;
            int currentTop = Console.CursorTop;
            int currentLeft = Console.CursorLeft;

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < _maxQueueCount; i++)
            {
                SystemMessage message = _messageQueue.ElementAt(i);

                switch (message.Type)
                {
                    case SystemConsoleType.HighLevel:
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case SystemConsoleType.LowLevel:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case SystemConsoleType.MiddleLevel:
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                }

                Console.CursorLeft = 0;
                Console.Write(new string(' ', Console.WindowWidth));
                Console.CursorLeft = 0;

                if (!string.IsNullOrWhiteSpace(message.Message))
                    Console.WriteLine($"[{message.Time.ToString("HH:mm:ss.fff")}] {message.Message}");
                else
                    Console.WriteLine();

                Console.ResetColor();
            }

            for (int i = 0; i < _maxGuideLineCount; i++)
                Console.WriteLine(new string('-', Console.WindowWidth));

            Console.SetCursorPosition(currentLeft, currentTop);
        }
        public static void GuideLine(char character = '=')
        {
            Console.WriteLine("".PadRight(Console.WindowWidth, character));
            _writeLineCount++;
        }

        public static void HWriteItems(string[] items, string comment, int selectedIndex = 0)
        {
            if (!string.IsNullOrWhiteSpace(comment))
                WriteLine(comment);

            Write(" | ");
            for (int i = 0; i < items.Length; i++)
            {
                if (selectedIndex == i)
                    MenuSelectWrite($"{items[i]}".PadRight(8));
                else
                    MenuWrite($"{items[i]}".PadRight(8));

                Write(" | ");

            }
            WriteLine();
            GuideLine('~');
        }
        public static void HWriteItems(string[] items, int selectedIndex = 0)
        {
            HWriteItems(items, "", selectedIndex);
        }
        public static void VWriteItems(string[]?[] items, string comment, int selectedIndex = 0)
        {
            WriteLine();
            if (!string.IsNullOrWhiteSpace(comment))
            {
                WriteLine(comment);
                WriteLine();
            }

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    WriteLine();
                    continue;
                }
                MenuWrite($"- ");
                for (int j = 0; j < items[i]!.Length; j++)
                {
                    if (selectedIndex == i && j == 0)
                        MenuSelectWrite($"{items[i]![j],-15}");
                    else
                        MenuWrite($"{items[i]![j],-15}");

                    if (j < items[i]!.Length - 1)
                        MenuWrite("\t| ");

                }

                WriteLine();
                WriteLine();
            }
            GuideLine('~');
        }
        public static void VWriteItems(string[]?[] items, int selectedIndex = 0)
        {
            VWriteItems(items, "", selectedIndex);
        }

        public static bool Confirm(string message, Action? yesAction = null, Action? noAction = null)
        {
            int index = 0;
            string[] strings = new string[] { "예", "아니오" };
            ConsoleKey key;
            do
            {
                Flush();
                WriteLine(message);
                WriteLine();
                HWriteItems(strings, index);

                key = Console.ReadKey(intercept: true).Key;
                if (key == Defines.LEFT_KEY)
                    index = (index - 1 + strings.Length) % strings.Length;
                else if (key == Defines.RIGHT_KEY)
                    index = (index + 1) % strings.Length;

                if (key == Defines.ACCEPT_KEY && index == 0)
                {
                    yesAction?.Invoke();
                }
                else if (key == Defines.ACCEPT_KEY && index == 1)
                {
                    noAction?.Invoke();
                }
            } while (key != Defines.ACCEPT_KEY);

            return index == 0;
        }
        public static void WriteBattleStart()
        {
            Flush();
            int size = 30;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            char fillChar = '■';

            // 나선 모양으로 그리기
            for (int i = 0; i < size; i++)
            {
                WriteLine(new string(fillChar, Console.WindowWidth / 2));
                Thread.Sleep(10);
            }

            Console.ResetColor();
        }
        private static void EmptySystemLog()
        {
            _messageQueue.Clear();
            SystemMessage msg = new SystemMessage();
            msg.Message = "";
            msg.Type = SystemConsoleType.LowLevel;
            msg.Time = DateTime.Now;
            for (int i = 0; i < _maxQueueCount; i++)
            {
                _messageQueue.Enqueue(msg);
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
        }
        public static void EndingCredit()
        {
            _maxQueueCount = _endingCreditLineCount;
            Flush();
            EmptySystemLog();
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            void emptyLine(int count)
            {
                for (int i = 0; i < count; i++)
                {
                    Thread.Sleep(500);
                    LWriteLine("");
                }
            }

            LWriteLine("이세계 텍스트 RPG");
            emptyLine(2);
            LWriteLine("제작 : 인생한방이조(7조) 김태호");
            emptyLine(2);
            LWriteLine("기    획 : 김태호");
            emptyLine(2);
            LWriteLine("각    본 : 김태호");
            emptyLine(2);
            LWriteLine("연    출 : 김태호");
            emptyLine(2);
            LWriteLine("개    발 : 김태호");
            emptyLine(2);
            LWriteLine("리 소 스 : 김태호");
            emptyLine(2);
            LWriteLine("고    생 : 김태호");
            emptyLine(2);
            LWriteLine("특별출연 : 르탄이, 아네린, 로끼, 채리니, 퇴실요정");
            emptyLine(2);
            LWriteLine("지    원 : 스파르타코딩클럽 내일배움캠프");
            emptyLine(2);
            LWriteLine("한 마 디 : 플레이 해주셔서 베리 땡큐 감사!");
            emptyLine(5);
            LWriteLine($"{Defines.ACCEPT_KEY} 키를 누르면 다음으로 넘어갑니다.");

            while (Console.ReadKey().Key != Defines.ACCEPT_KEY)
            {

            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            _maxQueueCount = _defaultLineCount;
            _messageQueue.Clear();
            Init();
            Flush();
        }
    }
}
