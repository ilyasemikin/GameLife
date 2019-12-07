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
        static public ConsoleColor DefaultBackgroundColor { get; set; }
        static public ConsoleColor DefaultForegroundColor { get; set; }
        static private GameMessage Message { get; set; }
        static private int MessageShowedTicks { get; set; }
        public GamePanelMessage(OutputMatrix output) : base(output)
        {
            DefaultBackgroundColor = ConsoleColor.White;
            DefaultForegroundColor = ConsoleColor.Black;
        }
        public void SetMessage(GameMessage message, int ticks)
        {
            MessageShowedTicks = ticks;
            Message = message;
        }
        private void PrepareMessageToOutput()
        {
            if (MessageShowedTicks > 0)
            {
                for (int x = X; x < X + Width; x++)
                    output.SetChar(x, Y, x < Message.text.Length ? Message.text[x] : Space);
                MessageShowedTicks--;
            }
            else
            {
                Clear();
                Message = new GameMessage(null, DefaultBackgroundColor, DefaultForegroundColor);
            }
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
