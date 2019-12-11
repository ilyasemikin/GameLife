using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class CommandHandler
    {
        protected Dictionary<string, CommandEventDescription> availableCommands;
        public CommandHandler(Dictionary<string, CommandEventDescription> commands)
        {
            availableCommands = commands;
        }
        public abstract void TryParseCommand(string input);
    }
}
