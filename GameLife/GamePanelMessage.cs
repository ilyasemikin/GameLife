using System;

namespace GameLife
{
    sealed class GamePanelMessage : MessagePanel
    {
        private GameMessage _message;
        private int MessageShowedTicks { get; set; }
        public override int Width { get; set; }
        public override int Height
        {
            get => 1;
            set
            {
                return;
            }
        }
        public GameMessage StandartMessage { get; set; }
        public override GameMessage Message
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
        public override void Write()
        {
            PrepareMessageToOutput();
            base.Write();
        }
        public override void Clear()
        {
            for (int x = X; x < X + Width; x++)
                output.SetChar(x, Y, Space);
        }
    }
}
