using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class WorkLogic : ICommandEvent
    {
        protected MainPanel mainPanel;
        protected MessagePanel msgPanel;
        public WorkLogic(MainPanel main, MessagePanel message)
        {
            mainPanel = main;
            msgPanel = message;
        }
        public abstract void Draw();
        public abstract void Action();
        public abstract Dictionary<string, CommandEventDescription> GetCommandEvents();
    }
}
