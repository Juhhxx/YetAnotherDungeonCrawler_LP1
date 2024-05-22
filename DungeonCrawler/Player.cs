using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Player : Character
    {
        Item[] inventory = new Item[5];
        public Player(string name, int hp, int def, int atk) : base(name,hp,def,atk)
        {
            
        }
        public void MoveTo(string direction)
        {

        }
        public void PickUpItem()
        {

        }
        public void Heal()
        {

        }
        public void Equip()
        {

        }
        public void CheckStatus()
        {

        }
    }
}