using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameInput
    {
        static public string Read()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            //Console.CursorVisible = true;
            Console.Write(':');
            StringBuilder input = new StringBuilder();
            int indent = 0;
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                switch(key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Delete:
                        continue;
                    case ConsoleKey.Backspace:
                        input.Remove(input.Length - 1, 1);
                        if (indent > 0)
                            indent--;
                        break;
                    default:
                        input.Append(key.KeyChar);
                        break;
                }
                Console.SetCursorPosition(1, Console.WindowHeight - 1);
                Console.Write(input);
            }
            return input.ToString();
        }
    }
}
