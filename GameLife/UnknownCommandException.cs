using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class UnknownCommandException : SystemException
    {
        public UnknownCommandException(string message) : base(message)
        {

        }
    }
}
