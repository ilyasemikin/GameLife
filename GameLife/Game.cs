using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using static System.Math;

namespace GameLife
{
    static public class GameEngine
    {
        static List<CellPoint> cells = new List<CellPoint>();
        static private int _latency = 100;
        static public int Width { get => GameIO.GetFieldSize().Item1; }
        static public int Height { get => GameIO.GetFieldSize().Item2; }
        static public int Latency
        {
            get => _latency;
            set => _latency = (value > 10) ? value : 75;
        }
        static public bool StopGame { get; set; } = false;
        static GameEngine()
        {

        }
        static public bool IsCorrectCoordinate(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;
        static public bool CellPointCorrect(CellPoint cell) => IsCorrectCoordinate(cell.X, cell.Y);
        static public void AddLivingCells(CellPoint[] cls)
        {
            foreach (var cell in cls)
                AddLivingCell(cell);
        }
        static public void AddLivingCell(CellPoint cell)
        {
            if (!cells.Contains(cell) && CellPointCorrect(cell))
                cells.Add(cell);
        }
        static public void AddLivingCell(int x, int y) => cells.Add(new CellPoint(x, y));
        static public void DeleteLivingCells(CellPoint[] cells)
        {
            foreach (var cell in cells)
                DeleteLivingCell(cell);
        }
        static public void DeleteLivingCell(CellPoint cell) => cells.Remove(cell);
        static public void DeleteLivingCell(int x, int y) => DeleteLivingCell(new CellPoint(x, y));
        static public void ClearField() => cells.Clear();
        // Add check cell correct
        static public void DrawField()
        {
            foreach (var cell in cells)
                GameIO.AddCellPoint(cell.X, cell.Y, '*');
        }
        static public void CreateNextGeneration()
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
        static private void SetCountNeightborsCells(List<CellPoint> cells)
        {
            foreach (var cell in cells)
                cell.CountNeighbors = CalculateCountNeightborsCell(cells, cell);
        }
        static private int CalculateCountNeightborsCell(List<CellPoint> cells, CellPoint cell)
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
        static public void Run()
        {
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                        DrawField();
                        GameIO.Write();
                        if (!StopGame)
                            CreateNextGeneration();
                        Thread.Sleep(Latency);
                }
                if (Console.ReadKey(true).KeyChar == ':')
                {
                    var input = GameIO.ReadCommand();
                    // TODO: Refactor
                    if (input == null)
                        continue;
                    if (input == "q")
                        break;
                    try
                    {
                        GameCommands.TryParseCommand(input);
                    }
                    catch (Exception e)
                    {
                        GameIO.SetMessage(new GameMessage(e.Message, ConsoleColor.Red, ConsoleColor.White));
                    }
                }
            }
        }
    }
}
