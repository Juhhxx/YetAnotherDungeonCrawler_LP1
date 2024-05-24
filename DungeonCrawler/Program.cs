using System;

namespace DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            GameInitializer init = new GameInitializer();
            init.InitializeEnemies();
            Console.WriteLine("\n");
            init.InitializeItems();
            Console.WriteLine("\n");
            init.InitializeRooms();
        }
    }
}
