using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GamePanelTextOut : MainPanel
    {
        public override int Width { get; set; }
        public override int Height { get; set; }
        public GamePanelTextOut(OutputMatrix output) : base(output)
        {

        }
    }
}