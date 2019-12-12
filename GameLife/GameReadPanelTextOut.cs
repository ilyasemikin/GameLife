using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameReadPanelTextOut : ReadPanel
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
        public GameReadPanelTextOut(OutputMatrix output) : base(output)
        {

        }
        public override void Write()
        {
            var text = "Press q to exit";
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, (x < text.Length) ? text[x] : Space);
            base.Write();
        }
        public override string Read()
        {
            var c = Console.ReadKey(true);
            switch(c.Key)
            {
                case ConsoleKey.DownArrow:
                    return "arrow_down";
                case ConsoleKey.UpArrow:
                    return "arrow_up";
                case ConsoleKey.LeftArrow:
                    return "arrow_left";
                case ConsoleKey.RightArrow:
                    return "arrow_right";
                case ConsoleKey.Q:
                    return c.KeyChar.ToString();
                default:
                    return null;
            }
        }
    }
}
