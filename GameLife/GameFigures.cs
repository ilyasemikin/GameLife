using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameLife
{
    class GameFiguresException : Exception
    {
        public GameFiguresException(string msg) : base(msg)
        {

        }
    }

    static class GameFigures
    {
        static private readonly Dictionary<string, (int, int)[]> figures;
        static GameFigures()
        {
            figures = new Dictionary<string, (int, int)[]>();
        }
        static public int GetFiguresFromFile(string filename)
        {
            // Maybe need refactoring
            if (!File.Exists(filename))
                throw new GameFiguresException(string.Format($"File {filename} not exist"));
            var correctLinePattern = @"\D+={\s*(\(\s*\d+\s*,\s*\d+\s*\)\s*,\s*)*(\s*\(\s*\d+\s*,\s*\d+\s*\)\s*)?\s*}";
            var correctLineRegex = new Regex(correctLinePattern);
            var dotsPattern = @"(\d+\s*,\s*\d+)";
            var dotsRegex = new Regex(dotsPattern);
            int ret = 0;
            foreach (var line in File.ReadAllLines(filename))
            {
                if (correctLineRegex.IsMatch(line))
                {
                    var dots = new List<(int, int)>();
                    var figure = line.Substring(0, line.IndexOf('='));
                    var dotsMatch = dotsRegex.Match(line);
                    while (dotsMatch.Success)
                    {
                        var dotMatch = dotsMatch.Groups[0].Value;
                        var coordsStr = dotMatch.Split(new char[] { ' ', ',' });
                        var n1 = int.Parse(coordsStr[0]);
                        var n2 = int.Parse(coordsStr[1]);
                        dots.Add((n1, n2));
                        dotsMatch = dotsMatch.NextMatch();
                    }
                    figures.Add(figure, dots.ToArray());
                    ret++;
                }
                else
                {
                    figures.Clear();
                    throw new GameFiguresException(string.Format($"Line №{ret} has incorrect format in file {filename}"));
                }
            }
            return ret;
        }
        static public (int, int)[] SearchFigure(string figure)
        {
            if (figures.ContainsKey(figure))
                return ((int, int)[])figures[figure].Clone();
            return null;
        }
    }
}
