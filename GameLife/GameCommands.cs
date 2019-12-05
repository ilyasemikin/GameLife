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
                { "place", PlaceFigure },
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
                if (!GameEngine.CellPointCorrect(new CellPoint(x, y)))
                    throw new GameCommandsException("Incorrect coordinate");
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
                if (!GameEngine.CellPointCorrect(new CellPoint(x, y)))
                    throw new GameCommandsException("Incorrect coordinate");
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

        static private void PlaceFigure(string[] argv)
        {
            if (argv.Length == 3)
            {
                var figureName = argv[0];
                int x, y;
                if (!int.TryParse(argv[1], out x) || !int.TryParse(argv[2], out y))
                    throw new GameCommandsException("Incorrect arguments command 'place'");
                if (!GameEngine.CellPointCorrect(new CellPoint(x, y)))
                    throw new GameCommandsException("Incorrect cordinate");
                var figure = GameFigures.SearchFigure(figureName);
                if (figure == null)
                    throw new GameCommandsException(string.Format($"Figure {figureName} not exist"));
                // TODO: Refactor
                for (int i = 0; i < figure.Length; i++)
                    figure[i] = (figure[i].Item1 + x, figure[i].Item2 + y);
                foreach (var item in figure)
                    GameEngine.AddLivingCell(new CellPoint(item.Item1, item.Item2));
            }
            else
                throw new GameCommandsException("Command 'place' required 3 arguments");
        }
    }
}
