using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class ConsoleFunctions
    {
        public static (ConsoleColor, ConsoleColor) ChangeColors(ConsoleColor bcolor, ConsoleColor fcolor)
        {
            var ret = (bcolor: Console.BackgroundColor, fcolor: Console.ForegroundColor);
            Console.BackgroundColor = bcolor;
            Console.ForegroundColor = fcolor;
            return ret;
        }
    }
}
