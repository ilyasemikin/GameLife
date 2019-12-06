using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    struct GameMessage
    {
        public string text;
        public ConsoleColor bcolor;
        public ConsoleColor fcolor;
        public GameMessage(string text, ConsoleColor bcolor, ConsoleColor fcolor)
        {
            this.text = text;
            this.bcolor = bcolor;
            this.fcolor = fcolor;
        }
    }
}
