using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    class GameIOException : Exception
    {
        public GameIOException(string message) : base(message)
        {

        }
    }
    static class GameIO
    {
        static public int Width { get => matrix.Width; }
        static public int Height { get => matrix.Height; }
        static public int MinWidth { get; private set; }
        static public int MinHeight { get; private set; }
        static private OutputMatrix matrix;
        static private GamePanelField panelField;
        static private GamePanelMessage panelMessage;
        static private GameReadPanelCommand panelCommand;
        static GameIO()
        {
            MinWidth = 20;
            MinHeight = 20;
            matrix = new OutputMatrix(1, 1);
            panelField = new GamePanelField(matrix);
            panelMessage = new GamePanelMessage(matrix);
            panelCommand = new GameReadPanelCommand(matrix);
            Resize(Console.WindowWidth, Console.WindowHeight);
            ResizeAllPanels();
        }
        static private bool IsNeedResize() => Width != Console.WindowWidth || Height != Console.WindowHeight;
        static private bool WindowHasMinimumRequiredSize() => Width > MinWidth && Height > MinHeight;
        static private void Resize(int width, int height)
        {
            if (Width < 0 || Height < 0)
                throw new GameIOException("Negative resize");
            Console.CursorVisible = false;
            matrix.Resize(width, height);
        }
        static private void ResizePanel(GamePanel panel, int x, int y, int width, int height)
        {
            panel.X = x;
            panel.Y = y;
            panel.Width = width;
            panel.Height = height;
        }
        static private void ResizeReadPanel(GameReadPanel panel, int x, int y, int width)
        {
            panel.X = x;
            panel.Y = y;
            panel.Width = width;
        }
        static private void ResizeAllPanels()
        {
            ResizePanel(panelField, 0, 0, Width, Height - 2);
            ResizePanel(panelMessage, 0, Height - 2, Width, 0);
            ResizeReadPanel(panelCommand, 0, Height - 1, Width);
        }
        static public void Write()
        {
            if (IsNeedResize())
            {
                Resize(Console.WindowWidth, Console.WindowHeight);
                ResizeAllPanels();
            }
            if (WindowHasMinimumRequiredSize())
            {
                panelField.Write();
                panelField.Clear();
                panelMessage.Write();
            }
            else
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Too small window");
                Console.SetCursorPosition(0, 0);
            }
        }
        static public void SetMessage(GameMessage message, int ticks = 15) => panelMessage.SetMessage(message, ticks);
        static public string ReadCommand() => panelCommand.Read();
        static public void AddCellPoint(int x, int y, char c) => panelField.AddCellPoint(x, y, c);
        static public (int, int) GetFieldSize() => (panelField.Width, panelField.Height);
    }
}
