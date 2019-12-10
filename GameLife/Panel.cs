using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class Panel
    {
        protected OutputMatrix output;
        public char Space { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        abstract public int Width { get; set; }
        abstract public int Height { get; set; }
        abstract public void Write();
        abstract public void Clear();
        public Panel(OutputMatrix output)
        {
            this.output = output;
            X = Y = 0;
            Width = Height = 0;
        }
    }
}
