using System;

namespace GameLife
{
    abstract class MessagePanel : Panel
    {
        public abstract GameMessage Message { get; set; }
        public MessagePanel(OutputMatrix output) : base(output)
        {

        }
        public override void Write()
        {
            var colors = ConsoleFunctions.ChangeColors(Message.bcolor, Message.fcolor);
            Console.SetCursorPosition(X, Y);
            Console.Write(output.GetLine(Y, X, X + Width));
            Console.SetCursorPosition(0, 0);
            ConsoleFunctions.ChangeColors(colors.Item1, colors.Item2);
        }
    }
}
