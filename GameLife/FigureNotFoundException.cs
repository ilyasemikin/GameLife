using System;

namespace GameLife
{
    class FigureNotFoundException : SystemException
    {
        public FigureNotFoundException(string figureName = null)
            : base($"Figure {(figureName ?? "")} not fount")
        {

        }
    }
}
