using System;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("Adventurer",100,10,5);
            GameView view = new GameView();

            Controller controller = new Controller(view,player);

            controller.Start();
        }
    }
}
