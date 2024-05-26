using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler
{
    /// <summary>
    /// Base class for Player and Enemy, that controls the stats and combat of a character
    /// </summary>
    public abstract class Character
    {
        // Initialize Name identifier 
        public string Name { get; }
        // Initialize HP value
        public int HP { get; protected set; }
        // Initialize AttackPower value
        public int AttackPower { get; protected set; }
        // Initialize Defense value
        public int Defense { get; protected set; }

        /// <summary>
        /// Constructor for Character class
        /// </summary>
        /// <param name="name">String name that describes the character</param>
        /// <param name="hp">Int HP as the base value for a character's life</param>
        /// <param name="atk">Int AttackPower as the base value for a character's hit power</param>
        /// <param name="def">Int Defense as the base value for a character's defense</param>
        public Character(string name, int hp, int atk, int def)
        {
            Name = name;
            HP = hp;
            Defense = def;
            AttackPower = atk;
        }

        /// <summary>
        /// Method for combat between characters. It calculates the hit power of an attack by
        /// by first removing the targets defense out of the value and clamping it to 0, 
        /// and then applying the remaining value to the target's HP and clamping it to 0.
        /// </summary>
        /// <param name="target">The character to inflict the attack on.</param>
        /// <returns>Returns the hit power of the attack.</returns>
        public int Attack (Character target)
        {
            int hitPower = AttackPower - target.Defense;
            if (hitPower < 0) hitPower = 0;
            target.HP -= hitPower;
            if (target.HP < 0) target.HP = 0;
            return hitPower;
        }

        /// <summary>
        /// Method that sees if the character is dead.
        /// </summary>
        /// <returns>Returns true if the HP is less or equal to O. True if its above 0.</returns>
        public bool IsDead()
        {
            return HP <= 0;
        }
    }
}