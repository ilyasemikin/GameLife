using System.Collections.Generic;

namespace GameLife
{
    interface ICommandEvent
    {
        Dictionary<string, CommandEventDescription> GetCommandEvents();
    }
}
