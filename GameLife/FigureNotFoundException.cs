using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class FigureNotFoundException : SystemException
    {
        public FigureNotFoundException(string figureName = null)
            : base($"Figure {(figureName ?? "")} not fount")
        {

        }
    }
}
