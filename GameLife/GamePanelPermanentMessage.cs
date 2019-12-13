using System;

namespace GameLife
{
    class GamePanelPermanentMessage : MessagePanel
    {
        public override int Width { get; set; }
        public override int Height
        {
            get => 1;
            set
            {
                return;
            }
        }
        public override GameMessage Message { get; set; }
        public GamePanelPermanentMessage(OutputMatrix output) : base(output)
        {
            Message = new GameMessage("", ConsoleColor.White, ConsoleColor.Black, 0);
        }
        private void WriteMessageToOutput()
        {
            var length = (Message.text != null) ? Message.text.Length : 0;
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, x < length ? Message.text[x] : Space);
        }
        public override void Write()
        {
            Clear();
            WriteMessageToOutput();
            base.Write();
        }
    }
}
