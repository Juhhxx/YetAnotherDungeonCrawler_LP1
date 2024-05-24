using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler
{
    public abstract class Character
    {
        public string Name { get; }
        public int HP { get; protected set; }
        public int AttackPower { get; protected set; }
        public int Defense { get; protected set; }

        public Character(string name, int hp, int atk, int def)
        {
            Name = name;
            HP = hp;
            Defense = def;
            AttackPower = atk;
        }
        public int Attack (Character target)
        {
            int hitPower = AttackPower - target.Defense;
            if (hitPower < 0) hitPower = 0;
            target.HP -= hitPower;
            return hitPower;
        }
    }
}