using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room
    {
        private Dictionary<string, Room> accessRooms = new Dictionary<string, Room>
        {
            {"north", null},
            {"west", null},
            {"south", null},
            {"east", null}
        };
        public Enemy REnemy { get; private set; }
        public Item RItem { get; private set; }

        public Room(Enemy enemy, Item item)
        {
            REnemy = enemy;
            RItem = item;
        }

        public AddRoom(string direction, Room room)
        {
            accessRooms[direction] = room;
        }
    }
}