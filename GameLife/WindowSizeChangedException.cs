using System;

namespace GameLife
{
    class WindowSizeChangedException : SystemException
    {
        public WindowSizeChangedException(string message) : base(message)
        {

        }
    }
}
