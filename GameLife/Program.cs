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
            GameFigures.GetFiguresFromFile("figures");
            GameEngine.Latency = 150;
            GameEngine.Run();
        }
    }
}
