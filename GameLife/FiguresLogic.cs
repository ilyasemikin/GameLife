using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class FiguresLogic : WorkLogic
    {
        private struct FigureDescription
        {
            public string name;
            public (int, int)[] dots;
            public FigureDescription(string name, (int, int)[] dots)
            {
                this.name = name;
                this.dots = dots;
            }
        }
        private List<FigureDescription> Figures;
        private int SelectedFigure { get; set; }
        private int TopFigure { get; set; }
        private int CountFigures { get => Figures.Count; }
        private int Width { get => mainPanel.Width; }
        private int Height { get => mainPanel.Height; }
        private int WidthNames { get => Width / 2; }
        private int WidthDisplay { get => Width - (WidthNames + 1); }
        public FiguresLogic(MainPanel main, MessagePanel msg) : base(main, msg)
        {
            Figures = new List<FigureDescription>();
        }
        public void CommandEvent_Up(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
            {
                if (SelectedFigure > 0)
                {
                    SelectedFigure--;
                    if (SelectedFigure < TopFigure)
                        TopFigure--;
                }
            }
        }
        public void CommandEvent_Down(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
            {
                if (SelectedFigure < CountFigures - 1)
                {
                    SelectedFigure++;
                    if (SelectedFigure >= Height + TopFigure)
                        TopFigure++;
                }
            }
        }
        public override Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var emptyArray = new string[0];
            var ret = new Dictionary<string, CommandEventDescription>()
            {
                { "arrow_up", new CommandEventDescription(emptyArray, CommandEvent_Up) },
                { "arrow_down", new CommandEventDescription(emptyArray, CommandEvent_Down) },
            };
            return ret;
        }
        public override void Draw()
        {
            if (Figures.Count == 0)
            {
                mainPanel.Clear();
                msgPanel.Message = new GameMessage("Figures not found",
                    ConsoleColor.DarkRed,
                    ConsoleColor.White,
                    0);
                return;
            }
            int y = 0;
            for (int i = TopFigure; i < CountFigures; i++)
            {
                if (y >= Height)
                    break;
                var line = (i == SelectedFigure) ? '>' + Figures[i].name : Figures[i].name;
                if (line.Length >= WidthNames)
                    line = line.Substring(0, WidthNames);
                for (int x = 0; x < WidthNames; x++)
                    mainPanel.SetChar(x, y, (x < line.Length) ? line[x] : mainPanel.Space);
                y++;
            }
            ((GamePanelFigures)mainPanel).DisplayBackColor = ConsoleColor.DarkGreen;
            ((GamePanelFigures)mainPanel).DisplayForeColor = ConsoleColor.White;
            ((GamePanelFigures)mainPanel).DisplayStartPosition = WidthNames;
            foreach (var item in Figures[SelectedFigure].dots)
                if (item.Item1 < WidthDisplay && item.Item2 < Height)
                    mainPanel.SetChar(item.Item1 + WidthDisplay + 1, item.Item2, '*');
        }
        public override void Action()
        {
            return;
        }
        public void ClearFigures() => Figures.Clear();
        public void AddFigure(string name, (int, int)[] dots)
        {
            Figures.Add(new FigureDescription(name, dots));
        }
    }
}
