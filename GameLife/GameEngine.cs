using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    static class GameEngine
    {
        private static OutputMatrix output;
        private static GameScene currentGameScene;
        private static GameScene mainScene;
        private static GameScene textScene;
        static GameEngine()
        {
            output = new OutputMatrix(1, 1);
            InitScenes();
            currentGameScene = mainScene;
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
            var logic = new GameLogic(main);
            mainScene = new GameScene(main, msg, read, logic, output);
            mainScene.Latency = 20;
        }
        private static void InitInfoScene()
        {
            var main = new GamePanelTextOut(output);
            var msg = new GamePanelMessage(output);
            var read = new GameReadPanelTextOut(output);
            var logic = new TextOutLogic(main);
            textScene = new GameScene(main, msg, read, logic, output);
            textScene.Latency = 0;
        }
        public static void Run() => currentGameScene.Run();
    }
}
