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
        public WorkLogic Logic { get; private set; }
        private CommandHandler commandHandler;
        private List<string> _exitCommands;
        public List<string> ExitCommands
        {
            get => _exitCommands;
            set
            {
                if (value == null || value.Count == 0)
                    throw new ArgumentException();
                _exitCommands = value;
            }
        }
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
            MinWidth = MinHeight = 20;
            mainPanel = main;
            messagePanel = message;
            readPanel = read;
            ExitCommands = new List<string>()
            {
                "q"
            };
            Logic = logic;
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
        public Dictionary<string, CommandEventDescription> GetCommandEvents()
        {
            var ret = new Dictionary<string, CommandEventDescription>();
            AddRangeDictionary(ret, Logic.GetCommandEvents());
            return ret;
        }
        public string Run()
        {
            string[] words;
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
                        Logic.Draw();
                        Write();
                        Logic.Action();
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
                    words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ExitCommands.Contains(words[0]))
                        break;
                    try
                    {
                        commandHandler.TryParseCommand(words);
                    }
                    catch (Exception e)
                    {
                        messagePanel.Message = new GameMessage(e.Message, ConsoleColor.Red, ConsoleColor.White, 20);
                    }
                }
            } while (true);
            return words[0];
        }
    }
}
