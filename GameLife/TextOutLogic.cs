﻿using System;
using System.Collections.Generic;
using System.Linq;

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
                MaxLineLength = (value.Length == 0) ? 0 : value.Select(x => x.Length).Max();
            }
        }
        private int MaxLineLength { get; set; }
        private int CurrentLine { get; set; }
        private int CountLines { get => Text.Length; }
        private int Width { get => mainPanel.Width; }
        private int Height { get => mainPanel.Height; }
        private int Indent { get; set; }
        public TextOutLogic(MainPanel output, MessagePanel message) : base(output, message)
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
                if (CountLines - CurrentLine > Height)
                    CurrentLine++;
        }
        private void CommandEvent_Right(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                if (MaxLineLength - Indent > Width)
                    Indent++;
        }
        private void CommandEvent_Left(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                if (Indent > 0)
                    Indent--;
        }
        public override Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var emptyString = new string[0];
            var ret = new Dictionary<string, CommandEventDescription>()
            {
                { "arrow_up", new CommandEventDescription(emptyString, CommandEvent_Up) },
                { "arrow_down", new CommandEventDescription(emptyString, CommandEvent_Down) },
                { "arrow_left", new CommandEventDescription(emptyString, CommandEvent_Left) },
                { "arrow_right", new CommandEventDescription(emptyString, CommandEvent_Right) },
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
                    mainPanel.SetChar(x, y, (x < line.Length) ? line[x] : mainPanel.Space);
                y++;
            }
            var upperBorder = (CountLines == 0) ? 0 : CurrentLine + 1;
            var lowerBorder = CurrentLine + CountLines < Height ? CountLines : Height;
            var textMessage = $"Line ({upperBorder}-{lowerBorder})/{CountLines}";
            msgPanel.Message = new GameMessage(textMessage, ConsoleColor.DarkYellow, ConsoleColor.White, 0);
        }
        public override void Action()
        {
            return;
        }
    }
}
