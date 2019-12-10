using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class ReadPanel : Panel
    {
        public abstract string Read();
        public ReadPanel(OutputMatrix output) : base(output)
        {
            this.output = output;
            X = Y = 0;
            Width = Height = 0;
        }
    }
}
