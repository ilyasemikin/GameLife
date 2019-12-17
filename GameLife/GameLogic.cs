using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace GameLife
{
    sealed class GameLogic : WorkLogic
    {
        private List<CellPoint> cells;
        private char CellPointSymbol { get; set; }
        private bool StopGame { get; set; }
        private int Width { get => mainPanel.Width; }
        private int Height { get => mainPanel.Height; }
        public GameLogic(MainPanel main, MessagePanel message) : base(main, message)
        {
            CellPointSymbol = '*';
            StopGame = false;
            cells = new List<CellPoint>();
        }
        public bool IsCorrectCoordinate(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
        public bool CellPointCorrect(CellPoint cell) => IsCorrectCoordinate(cell.X, cell.Y);
        public void AddLivingCells(CellPoint[] cls)
        {
            foreach (var cell in cls)
                AddLivingCell(cell);
        }
        public void AddLivingCell(CellPoint cell)
        {
            if (!cells.Contains(cell) && CellPointCorrect(cell))
                cells.Add(cell);
        }
        public void DeleteLivingCells(CellPoint[] cells)
        {
            foreach (var cell in cells)
                DeleteLivingCell(cell);
        }
        public void DeleteLivingCell(CellPoint cell) => cells.Remove(cell);
        public void ClearField() => cells.Clear();
        public void CreateNextGeneration()
        {
            SetCountNeightborsCells(cells);
            var nextGeneratironCells = cells.Where(x => x.CountNeighbors == 2 || x.CountNeighbors == 3)
                                            .ToList();
            foreach (var cell in cells)
            {
                var offsets = new (int, int)[] { (-1, -1), (0, -1), (1, -1), (-1, 0), (1, 0), (-1, 1), (0, 1), (1, 1) };
                for (int i = 0; i < offsets.Length; i++)
                {
                    var anotherCell = new CellPoint(cell.X + offsets[i].Item1, cell.Y + offsets[i].Item2);
                    if (anotherCell.X == -1 || anotherCell.X == Width)
                        anotherCell.X = Width - Abs(anotherCell.X);
                    if (anotherCell.Y == -1 || anotherCell.Y == Height)
                        anotherCell.Y = Height - Abs(anotherCell.Y);
                    CalculateCountNeightborsCell(cells, anotherCell);
                    if (anotherCell.CountNeighbors == 3 && !nextGeneratironCells.Contains(anotherCell))
                        nextGeneratironCells.Add(anotherCell);
                }
            }
            cells = nextGeneratironCells;
        }
        private void SetCountNeightborsCells(List<CellPoint> cells)
        {
            foreach (var cell in cells)
                cell.CountNeighbors = CalculateCountNeightborsCell(cells, cell);
        }
        private int CalculateCountNeightborsCell(List<CellPoint> cells, CellPoint cell)
        {
            Func<CellPoint, bool> onCenter = (x => Abs(cell.X - x.X) <= 1 && Abs(cell.Y - x.Y) <= 1);
            Func<CellPoint, bool> onLRBorder = (x => Abs(cell.X - x.X) == Width - 1 && Abs(cell.Y - x.Y) <= 1);
            Func<CellPoint, bool> onTBBorder = (x => Abs(cell.X - x.X) <= 1 && Abs(cell.Y - x.Y) == Height - 1);
            Func<CellPoint, bool> onCorner = (x => Abs(cell.X - x.X) == Width - 1 && Abs(cell.Y - x.Y) == Height - 1);

            if (cell.X == 0 || cell.X == Width - 1 || cell.Y == 0 || cell.Y == Height - 1)
                cell.CountNeighbors = cells.Where(x => onCenter(x) || onLRBorder(x) || onTBBorder(x) || onCorner(x))
                                           .Count();
            else
                cell.CountNeighbors = cells.Where(x => onCenter(x))
                                           .Count();
            if (cells.Contains(cell))
                --cell.CountNeighbors;
            return cell.CountNeighbors;
        }
        private void CommandEvent_AddCells(string[] argv)
        {
            if (CommandsFunctions.IsCorrectMinParams(argv, 2))
                AddLivingCells(CommandsFunctions.ParseCellPoints(argv, 1, argv.Length));
        }
        private void CommandEvent_RemoveCells(string[] argv)
        {
            if (CommandsFunctions.IsCorrectMinParams(argv, 2))
                AddLivingCells(CommandsFunctions.ParseCellPoints(argv, 1, argv.Length));
        }
        private void CommandEvent_PlaceFigure(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 3))
            {
                var name = argv[1];
                var point = CommandsFunctions.ParseCellPoints(argv, 2, argv.Length)[0];
                var figureDots = GameFigures.SearchFigure(name).Select(x => (x.Item1 + point.X, x.Item2 + point.Y));
                foreach (var dot in figureDots)
                    if (!IsCorrectCoordinate(dot.Item1, dot.Item2))
                        break;
                foreach (var dot in figureDots)
                {
                    var x = dot.Item1 % Width;
                    var y = dot.Item2 % Height;
                    AddLivingCell(new CellPoint(x, y));
                }
            }
        }
        private void CommandEvent_Start(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                StopGame = false;
        }
        private void CommandEvent_Stop(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                StopGame = true;
        }
        private void CommandEvent_Clear(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                ClearField();
        }
        public override Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var addDesc = new string[]
            {
                "add of living cells",
                "usage: add x1 y1 x2 y2 . . .",
            };
            var removeDesc = new string[] {
                "remove of living cells",
                "usage: remove x1 y1 x2 y2 . . .",
            };
            var placeDesc = new string[]
            {
                "place figure",
                "usage: place {figure name} x y",
            };
            var startDesc = new string[]
            {
                "start game"
            };
            var stopDesc = new string[]
            {
                "stop game"
            };
            var clearDesc = new string[]
            {
                "clear game field"
            };
            var ret = new Dictionary<string, CommandEventDescription>()
            {
                { "add", new CommandEventDescription(addDesc, CommandEvent_AddCells) },
                { "remove", new CommandEventDescription(removeDesc, CommandEvent_RemoveCells) },
                { "place", new CommandEventDescription(placeDesc, CommandEvent_PlaceFigure) },
                { "start", new CommandEventDescription(startDesc, CommandEvent_Start) },
                { "stop", new CommandEventDescription(stopDesc, CommandEvent_Stop) },
                { "clear", new CommandEventDescription(clearDesc, CommandEvent_Clear) },
            };
            return ret;
        }
        public override void Draw()
        {
            foreach (var cell in cells)
            {
                if (cell.X >= Width || cell.Y >= Height)
                {
                    ClearField();
                    throw new WindowSizeChangedException("Cell point can't displayed. Clear field");
                }
                mainPanel.SetChar(cell.X, cell.Y, CellPointSymbol);
            }
        }
        public override void Action()
        {
            if (!StopGame)
                CreateNextGeneration();
        }
    }
}
