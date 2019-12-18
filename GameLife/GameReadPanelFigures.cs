using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameReadPanelFigures : ReadPanel
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
        public GameReadPanelFigures(OutputMatrix output) : base(output)
        {

        }
        public override string Read()
        {
            var c = Console.ReadKey(true);
            switch (c.Key)
            {
                case ConsoleKey.UpArrow:
                    return "arrow_up";
                case ConsoleKey.DownArrow:
                    return "arrow_down";
                case ConsoleKey.Q:
                    return c.KeyChar.ToString();
                default:
                    return null;
            }
        }
    }
}
