using System;

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
