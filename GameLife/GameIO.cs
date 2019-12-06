using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameIOException : Exception
    {
        public GameIOException(string message) : base(message)
        {

        }
    }
    static class GameIO
    {
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        static public int MinWidth { get; private set; }
        static public int MinHeight { get; private set; }
        static public char Space { get; set; }
        static private char[,] output;
        static GameIO()
        {
            MinWidth = 20;
            MinHeight = 20;
            Space = ' ';
            Resize(Console.WindowWidth, Console.WindowHeight);
        }
        static public void SetChar(int x, int y, char c)
        {
            output[x, y] = c;
        }
        static public char GetChar(int x, int y)
        {
            return output[x, y];
        }
        static public void Resize(int width, int height)
        {
            if (Width < 0 || Height < 0)
                throw new GameIOException("Negative resize");
            Width = width;
            Height = height;
            output = new char[Width, Height];
        }
        static public void Write()
        {
            WriteField();
            ClearField();
        }
        static public void WriteField()
        {
            for (int y = 0; y < Height; y++)
            {
                Console.SetCursorPosition(0, y);
                for (int x = 0; x < Width; x++)
                    Console.Write(output[x, y]);
            }
            Console.SetCursorPosition(0, 0);
        }
        static public void WriteCommand()
        {
            Console.SetCursorPosition(0, Height - 1);
            for (int x = 0; x < Height; x++)
                Console.Write(output[x, Height - 1]);
        }
        static public string ReadCommand()
        {
            var input = new StringBuilder();
            int indent = 0;
            SetChar(0, Height - 1, ':');
            WriteCommand();
            while (true)
            {
                var c = Console.ReadKey(true);
                switch(c.Key)
                {
                    case ConsoleKey.Escape:
                        ClearCommand();
                        WriteCommand();
                        return null;
                    case ConsoleKey.Enter:
                        ClearCommand();
                        WriteCommand();
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
                                SetChar(Width - 1, Height - 1, Space);
                                for (int x = Width - 1; x > 1; x--)
                                    output[x, Height - 1] = output[x - 1, Height - 1];
                                output[1, Height - 1] = input[indent];
                            }
                            else
                                SetChar(input.Length, Height - 1, Space);
                            input.Remove(input.Length - 1, 1);
                        }
                        break;
                    default:
                        input.Append(c.KeyChar);
                        if (input.Length >= Width)
                        {
                            for (int x = 1; x < Width - 1; x++)
                                output[x, Height - 1] = output[x + 1, Height - 1];
                            output[Width - 1, Height - 1] = input[input.Length - 1];
                            indent++;
                        }
                        else
                            SetChar(input.Length, Height - 1, c.KeyChar);
                        break;
                }
                WriteCommand();
            }
        }

        static private void ClearOutput()
        {
            ClearField();
            ClearCommand();
        }
        static private void ClearField()
        {
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height - 1; y++)
                    output[x, y] = Space;
        }
        static private void ClearCommand()
        {
            for (int x = 0; x < Width; x++)
                output[x, Height - 1] = Space;
        }
    }
}
