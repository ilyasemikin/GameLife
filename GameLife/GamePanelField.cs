using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    sealed class GamePanelField : MainPanel
    {
        public override int Width { get; set; }
        public override int Height { get; set; }
        public GamePanelField(OutputMatrix output) : base(output)
        {

        }
        public override void SetChar(int x, int y, char c)
        {
            output.SetChar(X + x, Y + y, c);
        }
        public override void Write()
        {
            for (int y = Y; y < Y + Height; y++)
            {
                Console.SetCursorPosition(X, y);
                Console.Write(output.GetLine(y, X, X + Width));
            }
            Console.SetCursorPosition(0, 0);
        }
        public override void Clear()
        {
            for (int x = X; x < X + Width; x++)
                for (int y = Y; y < Height; y++)
                    output.SetChar(x, y, Space);
        }
    }
}
