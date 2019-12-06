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
        static public int Width { get => matrix.Width; }
        static public int Height { get => matrix.Height; }
        static public int MinWidth { get; private set; }
        static public int MinHeight { get; private set; }
        static public char Space { get; set; }
        static private OutputMatrix matrix;
        static GameIO()
        {
            MinWidth = 20;
            MinHeight = 20;
            Space = ' ';
            matrix = new OutputMatrix(1, 1);
            Resize(Console.WindowWidth, Console.WindowHeight);
        }
        static public void Resize(int width, int height)
        {
            if (Width < 0 || Height < 0)
                throw new GameIOException("Negative resize");
            matrix.Resize(width, height);
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
                    Console.Write(matrix.GetChar(x, y));
            }
            Console.SetCursorPosition(0, 0);
        }
        static public void WriteCommand()
        {
            Console.SetCursorPosition(0, Height - 1);
            for (int x = 0; x < Width; x++)
                Console.Write(matrix.GetChar(x, Height - 1));
            Console.SetCursorPosition(0, 0);
        }
        static public string ReadCommand()
        {
            var input = new StringBuilder();
            int indent = 0;
            matrix.SetChar(0, Height - 1, ':');
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
                                matrix.SetChar(Width - 1, Height - 1, Space);
                                matrix.ShiftLineRight(Height - 1, 1, Width);
                                matrix.SetChar(1, Height - 1, input[indent]);
                            }
                            else
                                matrix.SetChar(input.Length, Height - 1, Space);
                            input.Remove(input.Length - 1, 1);
                        }
                        break;
                    default:
                        input.Append(c.KeyChar);
                        if (input.Length >= Width)
                        {
                            matrix.ShiftLineLeft(Height - 1, 1, Width - 1);
                            matrix.SetChar(Width - 1, Height - 1, input[input.Length - 1]);
                            indent++;
                        }
                        else
                            matrix.SetChar(input.Length, Height - 1, c.KeyChar);
                        break;
                }
                WriteCommand();
            }
        }
        static public void AddCellPoint(int x, int y, char c)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
                throw new GameIOException("Invalid coordinate");
            matrix.SetChar(x, y, c);
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
                    matrix.SetChar(x, y, Space);
        }
        static private void ClearCommand()
        {
            for (int x = 0; x < Width; x++)
                matrix.SetChar(x, Height - 1, Space);
        }
    }
}
