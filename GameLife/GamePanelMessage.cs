using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GamePanelMessage : GamePanel
    {
        private const int _height = 1;
        override public char Space { get; set; }
        override public int X { get; set; }
        override public int Y { get; set; }
        override public int Width { get; set; }
        override public int Height
        {
            get => _height;
            set
            {
                return;
            }
        }
        public GameMessage StandartMessage { get; set; }
        private GameMessage Message { get; set; }
        private int MessageShowedTicks { get; set; }
        public GamePanelMessage(OutputMatrix output) : base(output)
        {
            StandartMessage = new GameMessage(null, ConsoleColor.White, ConsoleColor.Black);
        }
        public void SetMessage(GameMessage message, int ticks)
        {
            MessageShowedTicks = ticks;
            Message = message;
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
