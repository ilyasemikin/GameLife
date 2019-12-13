using System;

namespace GameLife
{
    class UnknownCommandException : SystemException
    {
        public UnknownCommandException(string message) : base(message)
        {

        }
    }
}
