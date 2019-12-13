using System.Collections.Generic;

namespace GameLife
{
    sealed class CommandHandler
    {
        private Dictionary<string, CommandEventDescription> availableCommands;
        public CommandHandler(Dictionary<string, CommandEventDescription> commands)
        {
            availableCommands = commands;
        }
        public void TryParseCommand(string[] input)
        {
            var command = input[0];
            if (availableCommands.ContainsKey(command))
                availableCommands[command].func(input);
            else
                throw new UnknownCommandException($"Unknown command: {command}");
        }
    }
}
