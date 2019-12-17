using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLife
{
    static class GameEngine
    {
        private static OutputMatrix output;
        private static GameScene currentGameScene;
        private static GameScene mainScene;
        private static GameScene textScene;
        private static Dictionary<string, CommandEventDescription> commands;
        private static bool Exit { get; set; }
        static GameEngine()
        {
            output = new OutputMatrix(1, 1);
            InitCommands();
            InitScenes();
            currentGameScene = mainScene;
            Exit = false;
        }
        private static void InitCommands()
        {
            var emptyStrings = new string[0];
            commands = new Dictionary<string, CommandEventDescription>()
            {
                { "help", new CommandEventDescription(emptyStrings, CommandEvent_Help) },
                { "about", new CommandEventDescription(emptyStrings, CommandEvent_About) },
                { "figures", new CommandEventDescription(emptyStrings, CommandEvent_Figures) },
                { "exit", new CommandEventDescription(emptyStrings, CommandEvent_Exit) },
                { "quit", new CommandEventDescription(emptyStrings, CommandEvent_Exit) },
                { "q", new CommandEventDescription(emptyStrings, CommandEvent_Exit) },
            };
        }
        private static void InitScenes()
        {
            InitMainScene();
            InitInfoScene();
        }
        private static void InitMainScene()
        {
            var main = new GamePanelField(output);
            var msg = new GamePanelMessage(output);
            msg.StandartMessage = new GameMessage("Conway's game of life", ConsoleColor.DarkBlue, ConsoleColor.White, 0);
            var read = new GameReadPanelCommand(output);
            var logic = new GameLogic(main, msg);
            mainScene = new GameScene(main, msg, read, logic, output)
            {
                Latency = 20,
                ExitCommands = commands.Select(x => x.Key).ToList(),
            };
        }
        private static void InitInfoScene()
        {
            var main = new GamePanelTextOut(output);
            var msg = new GamePanelPermanentMessage(output);
            var read = new GameReadPanelTextOut(output);
            var logic = new TextOutLogic(main, msg);
            textScene = new GameScene(main, msg, read, logic, output)
            {
                Latency = 0,
            };
        }
        private static List<string> GetListCommandsDescriptions(Dictionary<string, CommandEventDescription> dict)
        {
            return dict.Select(x => x.Key + " - " + x.Value.description).ToList();
        }
        private static void CommandEvent_Help(string[] argv)
        {
            var text = GetListCommandsDescriptions(mainScene.GetCommandEvents());
            text.AddRange(GetListCommandsDescriptions(commands));
            ((TextOutLogic)textScene.Logic).Text = text.ToArray();
            currentGameScene = textScene;
        }
        private static void CommandEvent_About(string[] argv)
        {
            var text = new string[]
            {
                ""
            };
            ((TextOutLogic)textScene.Logic).Text = text;
            currentGameScene = textScene;
        }
        private static void CommandEvent_Exit(string[] argv) => Exit = true;
        private static void CommandEvent_Figures(string[] argv)
        {
            var text = GameFigures.GetListFigures().ToArray();
            ((TextOutLogic)textScene.Logic).Text = text;
            currentGameScene = textScene;
        }
        public static void Run()
        {
            do
            {
                var sceneResult = currentGameScene.Run();
                if (currentGameScene == mainScene)
                    commands[sceneResult].func(new string[] { sceneResult });
                else
                    currentGameScene = mainScene;
            } while (!Exit);
        }
    }
}
