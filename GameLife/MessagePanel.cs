using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class MessagePanel : Panel
    {
        public abstract GameMessage Message { get; set; }
        public abstract GameMessage StandartMessage { get; set; }
        public MessagePanel(OutputMatrix output) : base(output)
        {

        }
    }
}
