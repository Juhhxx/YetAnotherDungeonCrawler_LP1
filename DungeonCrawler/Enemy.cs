using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Enemy : Character
    {
        public Enemy(string name, int hp, int def, int atk) : base(name,hp,def,atk)
        {

        }
    }
}