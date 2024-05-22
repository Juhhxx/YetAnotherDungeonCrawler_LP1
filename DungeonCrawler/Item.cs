using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public abstract class Item
    {
        public string Name { get; }
        public BuffType Type { get; }
        public int Buff { get; }

        public Item(string name, BuffType type, int buff)
        {
            Name = name;
            Type = type;
            Buff = buff;
        }
    }
}