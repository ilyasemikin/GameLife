using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class OutputMatrixException : Exception
    {
        public OutputMatrixException(string message) : base(message)
        {

        }
    }
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
                throw new OutputMatrixException("Invalid size");
            matrix = new char[m, n];
        }
        public void SetChar(int x, int y, char c) => matrix[x, y] = c;
        public char GetChar(int x, int y) => matrix[x, y];
        public string GetLine(int line)
        {
            if (line < 0 || line >= Height)
                throw new OutputMatrixException(string.Format($"Invalid index {line}"));
            var ret = new StringBuilder();
            for (int x = 0; x < Width; x++)
                ret.Append(matrix[x, line]);
            return ret.ToString();
        }
        public void ShiftLineLeft(int line, int startIndex, int endIndex)
        {
            if (line < 0 || line >= Height)
                throw new OutputMatrixException(string.Format($"Invalid index {line}"));
            for (int x = startIndex; x < endIndex; x++)
                matrix[x, line] = matrix[x + 1, line];
        }
        public void ShiftLineRight(int line, int startIndex, int endIndex)
        {
            if (line < 0 || line >= Height)
                throw new OutputMatrixException(string.Format($"Invalid index {line}"));
            for (int x = endIndex - 1; x > startIndex; x--)
                matrix[x, line] = matrix[x - 1, line];
        }
    }
}
