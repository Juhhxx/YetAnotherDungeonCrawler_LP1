using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Item
    {
        public string Name { get; }
        public BuffType Type { get; }
        public int BuffValue { get; }

        public Item(string name, BuffType type, int buff)
        {
            Name = name;
            Type = type;
            BuffValue = buff;
        }
    }
}