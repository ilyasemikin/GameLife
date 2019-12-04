using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine.AddLivingCell(1, 1);
            GameEngine.AddLivingCell(1, 2);
            GameEngine.AddLivingCell(1, 3);
            GameEngine.AddLivingCell(10, 10);
            GameEngine.AddLivingCell(10, 11);
            GameEngine.AddLivingCell(11, 10);
            GameEngine.AddLivingCell(11, 11);
            GameEngine.Width = Console.WindowWidth;
            GameEngine.Height = Console.WindowHeight;
            GameEngine.Latency = 250;
            GameEngine.Run();
        }
    }
}
