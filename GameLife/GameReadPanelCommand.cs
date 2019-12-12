using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    sealed class GameReadPanelCommand : ReadPanel
    {
        public override int Width { get; set; }
        public override int Height
        {
            get => 1;
            set
            {
                return;
            }
        }
        public GameReadPanelCommand(OutputMatrix output) : base(output)
        {

        }
        public override string Read()
        {
            if (Console.ReadKey(true).KeyChar != ':')
                return null;
            var input = new StringBuilder();
            int indent = 0;
            output.SetChar(X, Y, ':');
            Write();
            while (true)
            {
                var c = Console.ReadKey(true);
                switch (c.Key)
                {
                    case ConsoleKey.Escape:
                        Clear();
                        Write();
                        return null;
                    case ConsoleKey.Enter:
                        Clear();
                        Write();
                        return (input.Length == 0) ? null : input.ToString();
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.Delete:
                    case ConsoleKey.Tab:
                        continue;
                    case ConsoleKey.Backspace:
                        if (input.Length > 0)
                        {
                            if (indent > 0)
                            {
                                indent--;
                                output.SetChar(X + Width - 1, Y, Space);
                                output.ShiftLineRight(Y, X + 1, X + Width);
                                output.SetChar(X + 1, Y, input[indent]);
                            }
                            else
                                output.SetChar(X + input.Length, Y, Space);
                            input.Remove(input.Length - 1, 1);
                        }
                        break;
                    default:
                        input.Append(c.KeyChar);
                        if (input.Length >= Width)
                        {
                            output.ShiftLineLeft(Y, X + 1, X + Width - 1);
                            output.SetChar(X + Width - 1, Y, input[input.Length - 1]);
                            indent++;
                        }
                        else
                            output.SetChar(X + input.Length, Y, c.KeyChar);
                        break;
                }
                Write();
            }
        }
    }
}
