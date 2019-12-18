using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GameLife
{
    static class GameFigures
    {
        private static readonly Dictionary<string, (int, int)[]> figures;
        static GameFigures()
        {
            figures = new Dictionary<string, (int, int)[]>();
        }
        public static List<string> GetListNamesFigures() => figures.Select(x => x.Key).ToList();
        public static Dictionary<string, (int, int)[]> GetListFigures() => figures;
        public static int GetFiguresFromFile(string filename)
        {
            // Maybe need refactoring
            if (!File.Exists(filename))
                throw new FileNotFoundException("", filename);
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
                    if (!figures.ContainsKey(figure))
                        figures.Add(figure, dots.ToArray());
                    ret++;
                }
                else
                {
                    figures.Clear();
                    throw new FileStringParseException($"Line №{ret} has incorrect format in file {filename}");
                }
            }
            return ret;
        }
        public static void GetFiguresFromDirectory(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Directory not found {path}");
            var fileTypes = new Dictionary<string, Action<string>>()
            {
                { ".cells", ParsePlaintextFile },
                //{ ".rle",  ParseRLEFile },
            };
            foreach (var filePath in Directory.GetFiles(path))
            {
                var fileExt = Path.GetExtension(filePath).ToLower();
                if (fileTypes.ContainsKey(fileExt))
                    fileTypes[fileExt](filePath);
            }
        }
        private static void ParsePlaintextFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);
            var ret = new List<(int, int)>();
            int lineNumber = 0;
            foreach (var line in File.ReadAllLines(path))
            {
                if (line.Length == 0 || line[0] == '!')
                    continue;
                for (int i = 0; i < line.Length; i++)
                    if (line[i] == 'O')
                        ret.Add((i, lineNumber));
                lineNumber++;
            }
            var name = Path.GetFileNameWithoutExtension(path);
            if (!figures.ContainsKey(name))
                figures.Add(name, ret.ToArray());
        }
        private static void ParseRLEFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("", path);
            var figure = new StringBuilder();
            foreach (var line in File.ReadAllLines(path))
            {
                if (line.Length == 0 || line[0] == '#')
                    continue;
                
            }
        }
        public static (int, int)[] SearchFigure(string figure)
        {
            if (figures.ContainsKey(figure))
                return ((int, int)[])figures[figure].Clone();
            return null;
        }
    }
}
