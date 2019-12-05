﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using static System.Math;

namespace GameLife
{
    public class CellPoint
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int CountNeighbors { get; set; }

        public CellPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            CellPoint p = (CellPoint)obj;
            return (X == p.X) && (Y == p.Y);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    static public class GameEngine
    {
        static List<CellPoint> cells = new List<CellPoint>();

        static private int _latency = 100;
        static public int Width { get; set; }
        static public int Height { get; set; }
        static public int Latency
        {
            get => _latency;
            set => _latency = (value > 10) ? value : 500;
        }
        static public bool StopGame { get; set; } = false;
        static GameEngine()
        {

        }
        static public void AddLivingCells(CellPoint[] cls)
        {
            foreach (var cell in cls)
                AddLivingCell(cell);
        }
        static public void AddLivingCell(CellPoint cell)
        {
            if (!cells.Contains(cell))
                cells.Add(cell);
        }
        static public void AddLivingCell(int x, int y)
        {
            cells.Add(new CellPoint(x, y));
        }

        static public void DeleteLivingCell(CellPoint cell)
        {
            cells.Remove(cell);
        }

        static public void DeleteLivingCell(int x, int y)
        {
            DeleteLivingCell(new CellPoint(x, y));
        }

        static public void ClearField()
        {
            cells.Clear();
        }

        static public void DrawField(int x = 0, int y = 0)
        {
            Console.CursorVisible = false;
            Console.Clear();
            foreach(var cell in cells)
            {
                Console.SetCursorPosition(x + cell.X, y + cell.Y);
                Console.Write('*');
            }
        }
        static public void CreateNextGeneration()
        {
            SetCountNeightborsCells(cells);
            var nextGeneratironCells = cells.Where(x => x.CountNeighbors == 2 || x.CountNeighbors == 3)
                                            .ToList();
            foreach (var cell in cells)
            {
                var offsets = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
                for (int i = 0; i < offsets.Length; i++)
                {
                    var anotherCell = new CellPoint(cell.X + offsets[i].Item1, cell.Y + offsets[i].Item2);
                    if (anotherCell.X >= 0 && anotherCell.Y >= 0 && anotherCell.X < Width && anotherCell.Y < Height)
                    {
                        CalculateCountNeightborsCell(cells, anotherCell);
                        if (anotherCell.CountNeighbors == 3 && !nextGeneratironCells.Contains(anotherCell))
                            nextGeneratironCells.Add(anotherCell);
                    }
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
            cell.CountNeighbors = cells.Where(x => Abs(cell.X - x.X) <= 1 && Abs(cell.Y - x.Y) <= 1)
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
                        if (!StopGame)
                            CreateNextGeneration();
                        Thread.Sleep(Latency);
                }
                if (Console.ReadKey(true).KeyChar == ':')
                {
                    Console.SetCursorPosition(0, Console.WindowHeight - 1);
                    Console.CursorVisible = true;
                    Console.Write(':');
                    var input = Console.ReadLine();
                    if (input == "q")
                        break;
                    try
                    {
                        GameCommands.TryParseCommand(input);
                    }
                    catch (GameCommandsException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
