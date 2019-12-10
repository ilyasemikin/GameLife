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
        private static GameScene mainScene;
        static GameEngine()
        {
            output = new OutputMatrix(1, 1);
            InitMainScene();
        }
        private static void InitMainScene()
        {
            var main = new GamePanelField(output);
            var msg = new GamePanelMessage(output);
            msg.StandartMessage = new GameMessage("Conway's game of life", ConsoleColor.DarkBlue, ConsoleColor.White, 0);
            var read = new GameReadPanelCommand(output);
            var logic = new GameLogic(main);
            mainScene = new GameScene(main, msg, read, logic, output);
        }
        public static void Run()
        {
            mainScene.Run();
        }
    }
}
