using System;
using System.IO;
namespace GameLife
{
    class Program
    {
        static void Main()
        {
            if (File.Exists("figures"))
                GameFigures.GetFiguresFromFile("figures");
            GameEngine.Latency = 60;
            GameEngine.Run();
        }
    }
}
