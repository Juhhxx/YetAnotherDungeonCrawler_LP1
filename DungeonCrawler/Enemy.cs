using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Enemy : Character
    {
        public Enemy(string name, int hp, int atk, int def) : base(name,hp,atk,def)
        {

        }
    }
}