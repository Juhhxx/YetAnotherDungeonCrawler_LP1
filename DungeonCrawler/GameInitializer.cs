using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DungeonCrawler
{
    public class GameInitializer
    {
        List<Enemy> enemyList;
        List<Item> itemList;
        List<Room> roomList;

        public void InitializeEnemies()
        {
            string s;
            using StreamReader sr = new StreamReader("Enemies.txt");
            
            while ((s = sr.ReadLine()) != "END")
            {
                Console.WriteLine(s);
            }
        }

    }
}