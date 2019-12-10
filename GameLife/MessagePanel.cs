using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class MessagePanel : Panel
    {
        abstract public GameMessage Message { get; set; }
        abstract public GameMessage StandartMessage { get; set; }
        public MessagePanel(OutputMatrix output) : base(output)
        {

        }
    }
}
