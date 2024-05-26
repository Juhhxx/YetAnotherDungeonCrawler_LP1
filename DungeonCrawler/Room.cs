using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Room
    {
        public Dictionary<string, Room> accessRooms = new Dictionary<string, Room>
        {
            {"north", null},
            {"west", null},
            {"south", null},
            {"east", null}
        };
        public Enemy REnemy { get; private set; }
        public Item RItem { get; private set; }
        public string Description { get; }
        public bool IsFinal { get; }

        public Room(string description, Enemy enemy, Item item)
        {
            Description = description;
            REnemy = enemy;
            RItem = item;
        }

        public void AddRoom(string direction, Room room)
        {
            accessRooms[direction] = room;
        }

        public void RemoveItem()
        {
            RItem = null;
        }
        public void KillEnemy()
        {
            REnemy = null;
        }
    }
}