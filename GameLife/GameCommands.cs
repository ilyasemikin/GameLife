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
            var words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = words[0];
            if (avaliableCommands.ContainsKey(command))
                avaliableCommands[command](words);
            else
                throw new GameCommandsException(string.Format($"Unknown command: {command}"));
        }
        static private bool IsCorrectParams(string[] argv, int expectedArgvLength) { 
            if (argv.Length == 0)
                throw new Exception();
            if (argv.Length - 1 != expectedArgvLength)
                throw new GameCommandsException(string.Format($"{argv[0]}: excpected {expectedArgvLength} parametrs, but get {argv.Length - 1}"));
            return true;
        }
        static private bool IsCorrectMinParams(string[] argv, int minExpectedArgvlength)
        {
            if (argv.Length == 0)
                throw new Exception();
            if (argv.Length - 1 < minExpectedArgvlength)
                throw new GameCommandsException(string.Format($"{argv[0]}: excpected minimum {minExpectedArgvlength} parametrs, but get {argv.Length - 1}"));
            return true;
        }
        /// <summary>
        /// Парсинг координат клеток начиная со startIndex до конца
        /// </summary>
        static private CellPoint[] ParseCellPoints(string[] argv, int startIndex)
        {
            var countArgv = argv.Length - startIndex;
            if (countArgv % 2 != 0)
                throw new Exception();
            var countDots = countArgv / 2;
            var ret = new CellPoint[countDots];
            for (int i = 0; i < countDots; i++)
            {
                int x, y;
                var index = startIndex + 2 * i;
                if (!int.TryParse(argv[index], out x) || !int.TryParse(argv[index + 1], out y))
                    throw new Exception();
                if (!GameEngine.IsCorrectCoordinate(x, y))
                    throw new Exception();
                ret[i] = new CellPoint(x, y);
            }
            return ret;
        }
        static private void AddLiveCell(string[] argv)
        {
            if (IsCorrectMinParams(argv, 2))
                GameEngine.AddLivingCells(ParseCellPoints(argv, 1));
        }
        static private void DeleteLiveCell(string[] argv)
        {
            if (IsCorrectMinParams(argv, 2))
                GameEngine.DeleteLivingCells(ParseCellPoints(argv, 1));
        }
        static private void StartGame(string[] argv)
        {
            if (IsCorrectParams(argv, 0))
                GameEngine.StopGame = false;
        }
        static private void StopGame(string[] argv)
        {
            if (IsCorrectParams(argv, 0))
                GameEngine.StopGame = true;
        }
        static private void ClearField(string[] argv)
        {
            if (IsCorrectParams(argv, 0))
                GameEngine.ClearField();
        }
        static private void PlaceFigure(string[] argv)
        {
            if (IsCorrectMinParams(argv, 3))
            {
                var figureName = argv[1];
                var cells = ParseCellPoints(argv, 2);
                var figure = GameFigures.SearchFigure(figureName);
                if (figure == null)
                    throw new GameCommandsException(string.Format($"Figure {figureName} not exist"));
                foreach (var cell in cells)
                {
                    for (int i = 0; i < figure.Length; i++)
                        figure[i] = (figure[i].Item1 + cell.X, figure[i].Item2 + cell.Y);
                    foreach (var item in figure)
                        GameEngine.AddLivingCell(new CellPoint(item.Item1, item.Item2));
                }
                GameIO.SetMessage(new GameMessage(string.Format($"Figure{(cells.Length == 1 ? "" : "s")} {figureName} added"), ConsoleColor.DarkGreen, ConsoleColor.White), 20);
            }
        }
    }
}
