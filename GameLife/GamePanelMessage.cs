﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GamePanelMessage : MessagePanel
    {
        private GameMessage _message;
        private int MessageShowedTicks { get; set; }
        override public int Width { get; set; }
        override public int Height
        {
            get => 1;
            set
            {
                return;
            }
        }
        override public GameMessage StandartMessage { get; set; }
        override public GameMessage Message
        {
            get => _message;
            set
            {
                _message = value;
                MessageShowedTicks = _message.ticks;
            }
        }
        public GamePanelMessage(OutputMatrix output) : base(output)
        {
            StandartMessage = new GameMessage(null, ConsoleColor.White, ConsoleColor.Black, 0);
        }
        private void WriteMessageToOutput()
        {
            var length = (Message.text != null) ? Message.text.Length : 0;
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, x < length ? Message.text[x] : Space);
        }
        private void PrepareMessageToOutput()
        {
            if (MessageShowedTicks > 0)
                MessageShowedTicks--;
            else if (Message != StandartMessage)
                Message = StandartMessage;
            Clear();
            WriteMessageToOutput();
        }
        override public void Write()
        {
            PrepareMessageToOutput();
            var colors = ConsoleFunctions.ChangeColors(Message.bcolor, Message.fcolor);
            Console.SetCursorPosition(X, Y);
            Console.Write(output.GetLine(Y, X, X + Width));
            Console.SetCursorPosition(0, 0);
            ConsoleFunctions.ChangeColors(colors.Item1, colors.Item2);
        }
        public override void Clear()
        {
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, Space);
        }
    }
}
