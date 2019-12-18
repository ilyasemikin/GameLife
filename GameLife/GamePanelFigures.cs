using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GamePanelFigures : MainPanel
    {
        public override int Width { get; set; }
        public override int Height { get; set; }
        public ConsoleColor DisplayBackColor { get; set; }
        public ConsoleColor DisplayForeColor { get; set; }
        public int DisplayStartPosition { get; set; }
        public GamePanelFigures(OutputMatrix output) : base(output)
        {

        }
        public override void Write()
        {
            WritePart(0, DisplayStartPosition);
            var lastColors = ConsoleFunctions.ChangeColors(
                    DisplayBackColor,
                    DisplayForeColor);
            WritePart(DisplayStartPosition, Width - DisplayStartPosition - 1);
            ConsoleFunctions.ChangeColors(
                    lastColors.Item1,
                    lastColors.Item2);
            Console.SetCursorPosition(0, 0);
        }
    }
}
