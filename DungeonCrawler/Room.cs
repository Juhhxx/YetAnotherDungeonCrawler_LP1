using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room
    {
        Dictionary<string, Room> accessRooms = new Dictionary<string, Room>
        {
            {"north", null},
            {"west", null},
            {"south", null},
            {"east", null}
        }
        Enemy enemy;
        Item item;

        public Room()
        {

        }

        public AddRoom(string room)
    }
}