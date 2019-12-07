﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameReadPanelCommand : GameReadPanel
    {
        override public char Space { get; set; }
        override public int X { get; set; }
        override public int Y { get; set; }
        override public int Width { get; set; }
        override public int Height { get => 1; }
        public GameReadPanelCommand(OutputMatrix output) : base(output)
        {

        }
        override public void Write()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(output.GetLine(Y, X, X + Width));
            Console.SetCursorPosition(0, 0);
        }
        override public void Clear()
        {
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, Space);
        }
        override public string Read()
        {
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