using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    /// <summary>
    /// Class that contains room traits, and controls them.
    /// </summary>
    public class Room
    {
        // Initialize access rooms dictionary.
        public Dictionary<string, Room> accessRooms = new Dictionary<string, Room>
        {
            {"north", null},
            {"west", null},
            {"south", null},
            {"east", null}
        };
        //Initialize room Enemy.
        public Enemy REnemy { get; private set; }
        // Initialize room Item.
        public Item RItem { get; private set; }
        // Initialize room description.
        public string Description { get; }
        // Initialize if room is final.
        public bool IsFinal { get; }

        /// <summary>
        /// Room class constructor.
        /// </summary>
        /// <param name="description">The description of the room to be used on the view class.</param>
        /// <param name="enemy">The unique enemy for the room.</param>
        /// <param name="item">The unique item for the room.</param>
        public Room(string description, Enemy enemy, Item item)
        {
            Description = description;
            REnemy = enemy;
            RItem = item;
        }

        /// <summary>
        /// Method that adds a room to access rooms in a given direction.
        /// </summary>
        /// <param name="direction">The direction to set the room of.</param>
        /// <param name="room">The room to add to direction.</param>
        public void AddRoom(string direction, Room room)
        {
            accessRooms[direction] = room;
        }

        /// <summary>
        /// Method that sets the room's item to null.
        /// </summary>
        public void RemoveItem()
        {
            RItem = null;
        }

        /// <summary>
        /// Method that sets the room's enemy to null.
        /// </summary>
        public void KillEnemy()
        {
            REnemy = null;
        }
    }
}