using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameFigures
    {
        static public readonly Dictionary<string, (int, int)[]> figures;

        static private readonly (int, int)[] Block = new (int, int)[] { (0, 0), (0, 1), (1, 0), (1, 1) };
        static GameFigures()
        {
            figures = new Dictionary<string, (int, int)[]>()
            {
                { "block", Block },
            };
        }
        static public (int, int)[] SearchFigure(string figure)
        {
            if (figures.ContainsKey(figure))
                return figures[figure];
            return null;
        }
    }
}
