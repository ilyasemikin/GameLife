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
        static public bool operator ==(GameMessage msg1, GameMessage msg2) => Equals(msg1, msg2);
        static public bool operator !=(GameMessage msg1, GameMessage msg2) => !(msg1 == msg2);
        override public bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;
            GameMessage p = (GameMessage)obj;
            return (text == p.text) && (bcolor == p.bcolor) && (fcolor == p.fcolor);
        }
        override public int GetHashCode() => base.GetHashCode();
    }
}
