using System;

namespace GameLife
{
    static class CommandsFunctions
    {
        public static bool IsCorrectParams(string[] argv, int expectedArgvLength)
        {
            if (argv.Length == 0)
                throw new ArgumentException();
            if (argv.Length - 1 != expectedArgvLength)
                throw new CountCommandParamsException(argv[0], expectedArgvLength, argv.Length - 1);
            return true;
        }
        public static bool IsCorrectMinParams(string[] argv, int minExpectedArgvlength)
        {
            if (argv.Length == 0)
                throw new ArgumentException();
            if (argv.Length - 1 < minExpectedArgvlength)
                throw new CountCommandParamsException(
                    argv[0],
                    minExpectedArgvlength,
                    argv.Length - 1,
                    CountCommandParamsException.Comprasion.MORE);
            return true;
        }
        /// <summary>
        /// Парсинг координат клеток начиная со startIndex до endIndex
        /// </summary>
        public static CellPoint[] ParseCellPoints(string[] argv, int startIndex, int endIndex)
        {
            var countArgv = endIndex - startIndex;
            if (countArgv % 2 != 0)
                throw new CountCommandParamsException(argv[0] + " dots", countArgv + 1 / 2, countArgv);
            var countDots = countArgv / 2;
            var ret = new CellPoint[countDots];
            for (int i = 0; i < countDots; i++)
            {
                var index = startIndex + 2 * i;
                if (!int.TryParse(argv[index], out int x) || !int.TryParse(argv[index + 1], out int y))
                    throw new ArgumentException($"Incorrect coordinate");
                ret[i] = new CellPoint(x, y);
            }
            return ret;
        }
    }
}
