using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    struct CommandEventDescription
    {
        public string description;
        public Func<string[], object> func;
        public CommandEventDescription(string desc, Func<string[], object> func)
        {
            description = desc;
            this.func = func;
        }
    }
}
