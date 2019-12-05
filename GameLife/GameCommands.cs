using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameCommandsException : Exception
    {
        public GameCommandsException(string message) : base(message)
        {

        }
    }
    static class GameCommands
    {
        static private Dictionary<string, Action<string[]>> avaliableCommands;
        static GameCommands()
        {
            avaliableCommands = new Dictionary<string, Action<string[]>>
            {
                { "add", AddLiveCell },
                { "delete", DeleteLiveCell },
                { "start", StartGame },
                { "stop", StopGame },
                { "clear", ClearField },
            };
        }
        static public void TryParseCommand(string input)
        {
            var words = input.Split();
            var command = words[0];
            if (avaliableCommands.ContainsKey(command))
            {
                words = words.Skip(1)
                             .ToArray();
                avaliableCommands[command](words);
            }
            else
                throw new GameCommandsException(string.Format($"Unknown command: {command}"));
        }
        static private void AddLiveCell(string[] argv)
        {
            if (argv.Length == 2)
            {
                int x, y;
                if (!int.TryParse(argv[0], out x) || !int.TryParse(argv[1], out y))
                    throw new GameCommandsException("Incorrect arguments command 'add'");
                if (x < 0 || x > GameEngine.Width)
                    throw new GameCommandsException("Incorrect value x");
                if (y < 0 || y > GameEngine.Height)
                    throw new GameCommandsException("Incorrect value y");
                GameEngine.AddLivingCell(new CellPoint(x, y));
            }
            else
                throw new GameCommandsException("Command 'add' required 2 arguments");
        }
        static private void DeleteLiveCell(string[] argv)
        {
            if (argv.Length == 2)
            {
                int x, y;
                if (!int.TryParse(argv[0], out x) || !int.TryParse(argv[1], out y))
                    throw new GameCommandsException("Incorrect arguments command 'delete'");
                if (x < 0 || x > GameEngine.Width)
                    throw new GameCommandsException("Incorrect value x");
                if (y < 0 || y > GameEngine.Height)
                    throw new GameCommandsException("Incorrect value y");
                GameEngine.DeleteLivingCell(new CellPoint(x, y));
            }
            else
                throw new GameCommandsException("Command 'delete' required 2 arguments");
        }
        static private void StartGame(string[] argv)
        {
            if (argv.Length == 0)
                GameEngine.StopGame = false;
            else
                throw new GameCommandsException("Command 'start' not required arguments");
        }
        static private void StopGame(string[] argv)
        {
            if (argv.Length == 0)
                GameEngine.StopGame = true;
            else
                throw new GameCommandsException("Command 'stop' not required arguments");
        }
        static private void ClearField(string[] argv)
        {
            if (argv.Length == 0)
                GameEngine.ClearField();
            else
                throw new GameCommandsException("Command 'clear' not required arguments");
        }
    }
}
