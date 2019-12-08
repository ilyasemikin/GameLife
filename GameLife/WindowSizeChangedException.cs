using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class WindowSizeChangedException : SystemException
    {
        public WindowSizeChangedException(string message) : base(message)
        {

        }
    }
}
