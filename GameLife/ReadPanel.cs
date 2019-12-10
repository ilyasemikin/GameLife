using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class ReadPanel : Panel
    {
        abstract public string Read();
        public ReadPanel(OutputMatrix output) : base(output)
        {
            this.output = output;
            X = Y = 0;
            Width = Height = 0;
        }
    }
}
