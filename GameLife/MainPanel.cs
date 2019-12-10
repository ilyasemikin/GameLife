using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class MainPanel : Panel
    {
        public MainPanel(OutputMatrix output) : base(output)
        {

        }
        public abstract void SetChar(int x, int y, char c);
    }
}
