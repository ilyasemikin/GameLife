using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class Program
    {
        static void AddTestCells()
        {
            var cells = new CellPoint[] {
                new CellPoint(1, 1),
                new CellPoint(1, 2),
                new CellPoint(2, 1),
                new CellPoint(2, 2),
                new CellPoint(5, 5),
                new CellPoint(5, 6),
                new CellPoint(5, 7)
            };
            GameEngine.AddLivingCells(cells);
        }
        static void Main(string[] args)
        {
            AddTestCells();
            GameFigures.GetFiguresFromFile("figures");
            GameEngine.Latency = 150;
            GameEngine.Run();
        }
    }
}
