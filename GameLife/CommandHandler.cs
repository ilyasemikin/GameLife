using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class CommandHandler
    {
        protected Panel[] panels;
        protected WorkLogic logic;
        protected Dictionary<string, CommandEventDescription> availableCommands;
        public CommandHandler(Dictionary<string, CommandEventDescription> commands, Panel[] panels, WorkLogic logic)
        {
            availableCommands = commands;
            this.panels = panels;
            this.logic = logic;
        }
        public abstract void TryParseCommand(string input);
    }
}
