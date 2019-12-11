using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    sealed class CommandHandler
    {
        private Dictionary<string, CommandEventDescription> availableCommands;
        public CommandHandler(Dictionary<string, CommandEventDescription> commands)
        {
            availableCommands = commands;
        }
        public void TryParseCommand(string input)
        {
            var words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = words[0];
            if (availableCommands.ContainsKey(command))
                availableCommands[command].func(words);
            else
                throw new UnknownCommandException($"Unknown command: {command}");
        }
    }
}
