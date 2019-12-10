using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameIO
    {
        static public int Width { get => matrix.Width; }
        static public int Height { get => matrix.Height; }
        static public int MinWidth { get; private set; }
        static public int MinHeight { get; private set; }
        static private readonly OutputMatrix matrix;
        static private readonly GamePanelField panelField;
        static private readonly GamePanelMessage panelMessage;
        static private readonly GameReadPanelCommand panelCommand;
        static GameIO()
        {
            MinWidth = 20;
            MinHeight = 20;
            matrix = new OutputMatrix(1, 1);
            panelField = new GamePanelField(matrix);
            panelMessage = new GamePanelMessage(matrix);
            var message = new GameMessage("Conway's Game of Life", ConsoleColor.DarkBlue, ConsoleColor.White, 0);
            panelMessage.StandartMessage = message;
            panelCommand = new GameReadPanelCommand(matrix);
            Resize(Console.WindowWidth, Console.WindowHeight);
            ResizeAllPanels();
        }
        static private bool IsNeedResize() => Width != Console.WindowWidth || Height != Console.WindowHeight;
        static private bool WindowHasMinimumRequiredSize() => Width > MinWidth && Height > MinHeight;
        static private void Resize(int width, int height)
        {
            if (Width < 0 || Height < 0)
                throw new ArgumentOutOfRangeException(string.Format($"(width = {width}, height = {height})"));
            Console.CursorVisible = false;
            matrix.Resize(width, height);
        }
        static private void ResizePanel(Panel panel, int x, int y, int width, int height)
        {
            panel.X = x;
            panel.Y = y;
            panel.Width = width;
            panel.Height = height;
        }
        static private void SetPositionPanel(Panel panel, int x0, int y0, int x, int y) => ResizePanel(panel, x0, y0, x - x0 + 1, y - y0 + 1);
        static private void ResizeAllPanels()
        {
            ResizePanel(panelField, 0, 0, Width, Height - 2);
            ResizePanel(panelMessage, 0, Height - 2, Width, 1);
            ResizePanel(panelCommand, 0, Height - 1, Width, 1);
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
        static public void SetMessage(GameMessage message) => panelMessage.Message = message;
        static public string ReadCommand() => panelCommand.Read();
        static public void AddCellPoint(int x, int y, char c) => panelField.AddCellPoint(x, y, c);
        static public (int, int) GetFieldSize() => (panelField.Width, panelField.Height);
    }
}
