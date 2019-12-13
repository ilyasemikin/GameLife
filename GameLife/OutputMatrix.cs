using System;
using System.Text;

namespace GameLife
{
    class OutputMatrix
    {
        private char[,] matrix;
        public int Width { get => matrix.GetLength(0); }
        public int Height { get => matrix.GetLength(1); }
        public OutputMatrix(int m, int n)
        {
            Resize(m, n);
        }
        public void Resize(int m, int n)
        {
            if (m <= 0 || n <= 0)
                throw new ArgumentOutOfRangeException($"(m = {m}, n = {n})");
            matrix = new char[m, n];
        }
        public void SetChar(int x, int y, char c) => matrix[x, y] = c;
        public char GetChar(int x, int y) => matrix[x, y];
        public string GetLine(int line) => GetLine(line, 0, Width);
        public string GetLine(int line, int startIndex, int endIndex)
        {
            if (line < 0 || line >= Height)
                throw new ArgumentOutOfRangeException($"line = {line}");
            var ret = new StringBuilder();
            for (int x = startIndex; x < endIndex; x++)
                ret.Append(matrix[x, line]);
            return ret.ToString();
        }
        public void ShiftLineLeft(int line, int startIndex, int endIndex)
        {
            if (line < 0 || line >= Height)
                throw new ArgumentOutOfRangeException($"line = {line}");
            for (int x = startIndex; x < endIndex; x++)
                matrix[x, line] = matrix[x + 1, line];
        }
        public void ShiftLineRight(int line, int startIndex, int endIndex)
        {
            if (line < 0 || line >= Height)
                throw new ArgumentOutOfRangeException($"line = {line}");
            for (int x = endIndex - 1; x > startIndex; x--)
                matrix[x, line] = matrix[x - 1, line];
        }
    }
}
