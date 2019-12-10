using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    interface ICommandEvent
    {
        Dictionary<string, CommandEventDescription> GetCommandEvents();
    }
}
