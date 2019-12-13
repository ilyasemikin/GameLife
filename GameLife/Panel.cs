using System;

namespace GameLife
{
    abstract class Panel
    {
        protected OutputMatrix output;
        public char Space { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public abstract int Width { get; set; }
        public abstract int Height { get; set; }
        public virtual void Write()
        {
            for (int y = Y; y < Y + Height; y++)
            {
                Console.SetCursorPosition(X, y);
                Console.Write(output.GetLine(y, X, X + Width));
                Console.SetCursorPosition(0, 0);
            }
        }
        public virtual void Clear()
        {
            for (int x = X; x < X + Width; x++)
                for (int y = Y; y < Y + Height; y++)
                    output.SetChar(x, y, Space);
        }
        public Panel(OutputMatrix output)
        {
            this.output = output;
            X = Y = 0;
            Width = Height = 0;
            Space = ' ';
        }
    }
}
