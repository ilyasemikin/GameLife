using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameIO
    {
        static private int Width = 1920;
        static private int Height = 1920;
        static private char[,] output;
        static GameIO()
        {
            output = new char[Width, Height];
        }
        static public void Show() => Show(Console.WindowWidth, Console.WindowHeight);
        static public void Show(int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < width; x++)
                    Console.Write(output[x, y]);
            }
            Console.SetCursorPosition(0, 0);
            ClearOutput();
        }
        static public void SetChar(int x, int y, char c)
        {
            output[x, y] = c;
        }
        static public char GetChar(int x, int y)
        {
            return output[x, y];
        }
        static public string ReadCommand() => ReadCommand(Console.WindowWidth, Console.WindowHeight);
        static public string ReadCommand(int width, int height)
        {
            var input = new StringBuilder();
            int indent = 0;
            Console.SetCursorPosition(0, height - 1);
            Console.Write(':');
            while (true)
            {
                var c = Console.ReadKey(true);
                switch(c.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.Enter:
                        return input.ToString();
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Delete:
                        continue;
                    case ConsoleKey.Backspace:
                        if (input.Length > 0)
                        {
                            if (indent > 0)
                            {
                                indent--;
                                Console.SetCursorPosition(width - 1, height - 1);
                            }
                            else
                                Console.SetCursorPosition(input.Length, height - 1);
                            Console.Write(' ');
                            input.Remove(input.Length - 1, 1);
                        }
                        break;
                    default:
                        input.Append(c.KeyChar);
                        if (input.Length + 1 >= height)
                            indent++;
                        break;
                }
                Console.SetCursorPosition(1, height - 1);
                Console.Write(input.ToString(indent, input.Length - indent));
            }
        }

        static private void ClearOutput()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                    output[x, y] = ' ';
        }
    }
}
