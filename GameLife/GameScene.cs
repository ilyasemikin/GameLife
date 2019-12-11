using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GameLife
{
    class GameScene : ICommandEvent
    {
        private int _latency;
        private OutputMatrix output;
        private MainPanel mainPanel;
        private MessagePanel messagePanel;
        private ReadPanel readPanel;
        private WorkLogic logic;
        private CommandHandler commandHandler;
        private bool Exit { get; set; }
        public int MinWidth { get; set; }
        public int MinHeight { get; set; }
        public int Width { get => output.Width; }
        public int Height { get => output.Height; }
        public int Latency
        {
            get => _latency;
            set
            {
                _latency = (value > 0) ? value : 0;
            }
        }
        public GameScene(MainPanel main, MessagePanel message, ReadPanel read, WorkLogic logic, OutputMatrix output)
        {
            Exit = false;
            MinWidth = MinHeight = 20;
            mainPanel = main;
            messagePanel = message;
            readPanel = read;
            this.logic = logic;
            this.output = output;
            var availableCommands = GetCommandEvents();
            commandHandler = new CommandHandler(availableCommands);
            Resize(Console.WindowWidth, Console.WindowHeight);
            ResizeAllPanels();
        }
        private bool HasMinimalSize() => Width > MinWidth && Height > MinHeight;
        private bool IsNeedResize() => Width != Console.WindowWidth || Height != Console.WindowHeight;
        private void Resize(int width, int height)
        {
            if (width < 0 || height < 0)
                throw new ArgumentOutOfRangeException($"(width = {width}, height = {height})");
            Console.CursorVisible = false;
            output.Resize(width, height);
        }
        private void ResizePanel(Panel panel, int x, int y, int width, int height)
        {
            panel.X = x;
            panel.Y = y;
            panel.Width = width;
            panel.Height = height;
        }
        private void Write()
        {
            mainPanel.Write();
            messagePanel.Write();
            readPanel.Write();
        }
        private void Clear()
        {
            mainPanel.Clear();
            messagePanel.Clear();
            readPanel.Clear();
        }
        private void ResizeAllPanels()
        {
            ResizePanel(mainPanel, 0, 0, Console.WindowWidth, Console.WindowHeight - 2);
            ResizePanel(messagePanel, 0, Console.WindowHeight - 2, Console.WindowWidth, 1);
            ResizePanel(readPanel, 0, Console.WindowHeight - 1, Console.WindowWidth, 1);
        }
        public void AddRangeDictionary(Dictionary<string, CommandEventDescription> ret, Dictionary<string, CommandEventDescription> added)
        {
            foreach (var item in added)
                ret.Add(item.Key, item.Value);
        }
        void CommandEvent_Exit(string[] argv)
        {
            if (CommandsFunctions.IsCorrectParams(argv, 0))
                Exit = true;
        }
        public Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var type = GetType();
            var ret = new Dictionary<string, CommandEventDescription>()
            {
                { "exit", new CommandEventDescription("", CommandEvent_Exit) },
                { "quit", new CommandEventDescription("", CommandEvent_Exit) },
                { "q", new CommandEventDescription("", CommandEvent_Exit) },
            };
            AddRangeDictionary(ret, logic.GetCommandEvents());
            return ret;
        }
        public void Run()
        {
            do
            {
                while (!Console.KeyAvailable)
                {
                    if (IsNeedResize())
                    {
                        Resize(Console.WindowWidth, Console.WindowHeight);
                        ResizeAllPanels();
                    }
                    Clear();
                    if (HasMinimalSize())
                    {
                        logic.Draw();
                        Write();
                        logic.Action();
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.Write("Too small window");
                        Console.SetCursorPosition(0, 0);
                    }

                    Thread.Sleep(Latency);
                }
                var input = readPanel.Read();
                if (input != null)
                {
                    try
                    {
                        commandHandler.TryParseCommand(input);
                    }
                    catch (Exception e)
                    {
                        messagePanel.Message = new GameMessage(e.Message, ConsoleColor.Red, ConsoleColor.White, 20);
                    }
                }
            } while (!Exit);
        }
    }
}
