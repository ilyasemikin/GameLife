using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class WorkLogic : ICommandEvent
    {
        protected MainPanel panel;
        public WorkLogic(MainPanel panel)
        {
            this.panel = panel;
        }
        public abstract void Draw();
        public abstract void Action();
        public abstract Dictionary<string, CommandEventDescription> GetCommandEvents();
    }
}
