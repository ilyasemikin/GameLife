using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    sealed class TextOutLogic : WorkLogic
    {
        public string[] _text;
        public string[] Text
        {
            get => _text;
            set
            {
                _text = value;
                MaxLineLength = value.Select(x => x.Length).Max();
            }
        }
        private int MaxLineLength { get; set; }
        private int CurrentLine { get; set; }
        private int CountLines { get => Text.Length; }
        private int Width { get => panel.Width; }
        private int Height { get => panel.Height; }
        private int Indent { get; set; }
        public TextOutLogic(MainPanel output) : base(output)
        {
            CurrentLine = 0;
        }
        private void CommandEvent_Up(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                if (CurrentLine > 0)
                    CurrentLine--;
        }
        private void CommandEvent_Down(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
            {
                if (CountLines - CurrentLine > Height)
                    CurrentLine++;
            }
        }
        private void CommandEvent_Right(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
            {
                if (MaxLineLength - Indent > Width)
                    Indent++;
            }
        }
        private void CommandEvent_Left(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
            {
                if (Indent > 0)
                    Indent--;
            }
        }
        public override Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var ret = new Dictionary<string, CommandEventDescription>()
            {
                { "arrow_up", new CommandEventDescription("", CommandEvent_Up) },
                { "arrow_down", new CommandEventDescription("", CommandEvent_Down) },
                { "arrow_left", new CommandEventDescription("", CommandEvent_Left) },
                { "arrow_right", new CommandEventDescription("", CommandEvent_Right) },
            };
            return ret;
        }
        public override void Draw()
        {
            int y = 0;
            for (int i = CurrentLine; i < CountLines; i++)
            {
                if (y >= Height)
                    break;
                var line = Text[i];
                line = (line.Length < Indent) ? "" : line.Substring(Indent);
                for (int x = 0; x < Width; x++)
                    panel.SetChar(x, y, (x < line.Length) ? line[x] : panel.Space);
                y++;
            }
        }
        public override void Action()
        {
            return;
        }
    }
}
