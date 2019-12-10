using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameCommands
    {
        static private readonly Dictionary<string, Action<string[]>> avaliableCommands;
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
                // Maybe temporary
                { "window", PrintWindowSize },
            };
        }
        static public void TryParseCommand(string input)
        {
            var words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var command = words[0];
            if (avaliableCommands.ContainsKey(command))
                avaliableCommands[command](words);
            else
                throw new UnknownCommandException($"Unknown command: {command}");
        }
        static private bool IsCorrectParams(string[] argv, int expectedArgvLength) { 
            if (argv.Length == 0)
                throw new ArgumentException();
            if (argv.Length - 1 != expectedArgvLength)
                throw new CountCommandParamsException(argv[0], expectedArgvLength, argv.Length - 1);
            return true;
        }
        static private bool IsCorrectMinParams(string[] argv, int minExpectedArgvlength)
        {
            if (argv.Length == 0)
                throw new ArgumentException();
            if (argv.Length - 1 < minExpectedArgvlength)
                throw new CountCommandParamsException(
                    argv[0],
                    minExpectedArgvlength,
                    argv.Length - 1,
                    CountCommandParamsException.Comprasion.MORE);
            return true;
        }
        /// <summary>
        /// Парсинг координат клеток начиная со startIndex до endIndex
        /// </summary>
        static private CellPoint[] ParseCellPoints(string[] argv, int startIndex, int endIndex)
        {
            var countArgv = endIndex - startIndex;
            if (countArgv % 2 != 0)
                throw new CountCommandParamsException(argv[0] + " dots",  countArgv + 1 / 2, countArgv);
            var countDots = countArgv / 2;
            var ret = new CellPoint[countDots];
            for (int i = 0; i < countDots; i++)
            {
                var index = startIndex + 2 * i;
                if (!int.TryParse(argv[index], out int x) || !int.TryParse(argv[index + 1], out int y))
                    throw new ArgumentException($"(x = {argv[index]}), y = {argv[index + 1]}");
                if (!GameEngine.IsCorrectCoordinate(x, y))
                    throw new ArgumentOutOfRangeException($"(x = {x}, y = {y})");
                ret[i] = new CellPoint(x, y);
            }
            return ret;
        }
        static private void AddLiveCell(string[] argv)
        {
            if (IsCorrectMinParams(argv, 2))
                GameEngine.AddLivingCells(ParseCellPoints(argv, 1, argv.Length));
        }
        static private void DeleteLiveCell(string[] argv)
        {
            if (IsCorrectMinParams(argv, 2))
                GameEngine.DeleteLivingCells(ParseCellPoints(argv, 1, argv.Length));
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
                var cells = ParseCellPoints(argv, 2, argv.Length);
                var figure = GameFigures.SearchFigure(figureName);
                if (figure == null)
                    throw new FigureNotFoundException(figureName);
                foreach (var cell in cells)
                {
                    for (int i = 0; i < figure.Length; i++)
                        figure[i] = (figure[i].Item1 + cell.X, figure[i].Item2 + cell.Y);
                    foreach (var item in figure)
                        GameEngine.AddLivingCell(new CellPoint(item.Item1, item.Item2));
                }
                GameIO.SetMessage(new GameMessage($"Figure{(cells.Length == 1 ? "" : "s")} {figureName} added", ConsoleColor.DarkGreen, ConsoleColor.White, 20));
            }
        }
        static private void PrintWindowSize(string[] argv)
        {
            if (IsCorrectParams(argv, 0))
                GameIO.SetMessage(new GameMessage($"Window size: {Console.WindowWidth}x{Console.WindowHeight}", ConsoleColor.DarkGreen, ConsoleColor.White, 20));
        }
    }
}
