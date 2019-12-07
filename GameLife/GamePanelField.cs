using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GamePanelField : GamePanel
    {
        override public char Space { get; set; }
        override public int X { get; set; }
        override public int Y { get; set; }
        override public int Width { get; set; }
        override public int Height { get; set; }
        public GamePanelField(OutputMatrix output) : base(output)
        {

        }
        // TODO: Add check
        public void AddCellPoint(int x, int y, char c)
        {
            output.SetChar(X + x, Y + y, c);
        }
        override public void Write()
        {
            for (int y = Y; y < Y + Height; y++)
            {
                Console.SetCursorPosition(X, y);
                Console.Write(output.GetLine(y, X, X + Width));
            }
            Console.SetCursorPosition(0, 0);
        }
        override public void Clear()
        {
            for (int x = X; x < X + Width; x++)
                for (int y = Y; y < Height; y++)
                    output.SetChar(x, y, Space);
        }
    }
}
