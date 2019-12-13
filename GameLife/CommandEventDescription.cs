using System;

namespace GameLife
{
    struct CommandEventDescription
    {
        public string description;
        public Action<string[]> func;
        public CommandEventDescription(string desc, Action<string[]> func)
        {
            description = desc;
            this.func = func;
        }
    }
}
