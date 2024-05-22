using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler
{
    public abstract class Character
    {
        public string Name { get; }
        public int HP { get; private set; }
        public int AttackPower { get; private set; }
        public int Defense { get; private set; }

        public Character(string name, int hp, int def, int atk)
        {
            Name = name;
            HP = hp;
            Defense = def;
            AttackPower = atk;
        }
        public void Attack (Character target)
        {
            int hitPower = AttackPower - target.Defense;
            if (hitPower < 0) hitPower = 0;
            target.HP -= hitPower;
        }
    }
}