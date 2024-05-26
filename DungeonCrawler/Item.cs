using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    /// <summary>
    /// Class that contains the Item traits
    /// </summary>
    public class Item
    {
        // Initialize Item Name.
        public string Name { get; }
        // Initialize Type of Buff.
        public BuffType Type { get; }
        //Initialize value for buff.
        public int BuffValue { get; }

        /// <summary>
        /// Constructor for Item.
        /// </summary>
        /// <param name="name">Name the item will be identified by.</param>
        /// <param name="type">Type of buff the Item will inflict on the Player.</param>
        /// <param name="buff">Value of the buff the Item will inflict on the Player.</param>
        public Item(string name, BuffType type, int buff)
        {
            Name = name;
            Type = type;
            BuffValue = buff;
        }
    }
}