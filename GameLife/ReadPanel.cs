using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class ReadPanel : Panel
    {
        public abstract string Read();
        public ReadPanel(OutputMatrix output) : base(output)
        {
            this.output = output;
            X = Y = 0;
            Width = Height = 0;
        }
        public override void Write()
        {
            // Вычитаю единицу для борьбы с "лагом", резким смещением экрана вниз
            var len = X + Width - (output.GetChar(Width - 1, Y) != Space ? 0 : 1);
            Console.SetCursorPosition(X, Y);
            Console.Write(output.GetLine(Y, X, X + Width));
            Console.SetCursorPosition(0, 0);
        }
    }
}
