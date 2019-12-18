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
        private static GameScene figureScene;
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
            var helpDesc = new string[]
            {
                "show command descriptions",
            };
            var aboutDesc = new string[]
            {
                "show information about program",
            };
            var figuresDesc = new string[]
            {
                "show available figures",
            };
            var exitDesc = new string[]
            {
                "exit from program",
            };
            commands = new Dictionary<string, CommandEventDescription>()
            {
                { "help", new CommandEventDescription(helpDesc, CommandEvent_Help) },
                { "about", new CommandEventDescription(aboutDesc, CommandEvent_About) },
                { "figures", new CommandEventDescription(figuresDesc, CommandEvent_Figures) },
                { "exit", new CommandEventDescription(exitDesc, CommandEvent_Exit) },
                { "quit", new CommandEventDescription(exitDesc, CommandEvent_Exit) },
                { "q", new CommandEventDescription(exitDesc, CommandEvent_Exit) },
            };
        }
        private static void InitScenes()
        {
            InitMainScene();
            InitInfoScene();
            InitFigureScene();
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
        private static void InitFigureScene()
        {
            var main = new GamePanelFigures(output);
            var msg = new GamePanelPermanentMessage(output);
            var read = new GameReadPanelFigures(output);
            var logic = new FiguresLogic(main, msg);
            figureScene = new GameScene(main, msg, read, logic, output)
            {
                Latency = 0,
            };
        }
        private static List<string> GetListCommandsDescriptions(Dictionary<string, CommandEventDescription> dict)
        {
            var ret = new List<string>();
            foreach (var item in dict)
            {
                var desc = item.Value.description;
                ret.Add(item.Key + " - " + (desc.Length == 0 ? "" : desc[0]));
                for (int i = 1; i < desc.Length; i++)
                    ret.Add("    " + desc[i]);
            }
            return ret;
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
            foreach (var figure in GameFigures.GetListFigures())
                ((FiguresLogic)figureScene.Logic).AddFigure(figure.Key, figure.Value);
            currentGameScene = figureScene;
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
