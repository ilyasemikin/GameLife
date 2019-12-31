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
            var nextGenCells = new List<CellPoint>();
            var field = new bool[Width, Height];
            foreach (var cell in cells)
                field[cell.X, cell.Y] = true;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    var countNeight = CalculateCountNeightborsCell(field, x, y);
                    if (field[x, y] && (countNeight == 2 || countNeight == 3))
                        nextGenCells.Add(new CellPoint(x, y));
                    if (countNeight == 3)
                        nextGenCells.Add(new CellPoint(x, y));
                }
            cells = nextGenCells;
        }
        public int CalculateCountNeightborsCell(bool[,] field, int x, int y)
        {
            var ret = 0;
            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;
                    var cx = x + dx;
                    var cy = y + dy;
                    if (cx < 0 || cx == Width)
                        cx = Width - Abs(cx);
                    if (cy < 0 || cy == Height)
                        cy = Height - Abs(cy);
                    if (field[cx, cy])
                        ret++;
                }
            return ret;
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
                var foundFigure = GameFigures.SearchFigure(name);
                if (foundFigure == null)
                    throw new FigureNotFoundException(name);
                var figureDots = foundFigure.Select(x => (x.Item1 + point.X, x.Item2 + point.Y));
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
