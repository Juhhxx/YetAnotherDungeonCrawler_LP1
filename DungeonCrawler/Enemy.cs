using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    // Enemy class identifies all the non Player class characters
    public class Enemy : Character
    {
        // Initialize Enemy description
        public string Description { get; }
        /// <summary>
        /// Enemy constructor
        /// </summary>
        /// <param name="name">The name to be used by Enemy which is returned to Character base.</param>
        /// <param name="hp">The HP to be used by Enemy which is returned to Character base.</param>
        /// <param name="atk">The attack power to be used by Enemy which is returned to Character base.</param>
        /// <param name="def">The defense to be used by Enemy which is returned to Character base.</param>
        public Enemy(string name, string desc, int hp, int atk, int def) : base(name,hp,atk,def)
        {
            // Set Enemy Description
            Description = desc;
        }
    }
}