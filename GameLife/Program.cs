using System.IO;
namespace GameLife
{
    class Program
    {
        static void Main()
        {
            if (File.Exists("figures_list"))
                GameFigures.GetFiguresFromFile("figures_list");
            if (Directory.Exists("figures"))
                GameFigures.GetFiguresFromDirectory("figures");
            GameEngine.Run();
        }
    }
}
