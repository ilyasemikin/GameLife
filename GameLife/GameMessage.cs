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
        public int ticks;
        public GameMessage(string text, ConsoleColor bcolor, ConsoleColor fcolor, int ticks)
        {
            this.text = text;
            this.bcolor = bcolor;
            this.fcolor = fcolor;
            this.ticks = ticks;
        }
        public static bool operator ==(GameMessage msg1, GameMessage msg2) => Equals(msg1, msg2);
        public static bool operator !=(GameMessage msg1, GameMessage msg2) => !(msg1 == msg2);
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;
            GameMessage p = (GameMessage)obj;
            return (text == p.text) && (bcolor == p.bcolor) && (fcolor == p.fcolor);
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}
