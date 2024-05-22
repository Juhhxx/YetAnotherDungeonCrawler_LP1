using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public abstract class Item
    {
        public string Name { get; }
        public int Buff { get; }

        public Item(string name, int buff)
        {
            Name = name;
            Buff = buff;
        }
    }
}