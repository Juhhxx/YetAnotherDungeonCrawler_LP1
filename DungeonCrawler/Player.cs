using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Player : Character
    {
        public Item[] Inventory { get; private set;} = new Item[5];
        public Item Weapon { get; private set; }
        public Item Shield { get; private set; }
        public Room InRoom{ get; private set; }
        private int _baseAttack;
        private int _baseDefense;
        public Player(string name, int hp, int def, int atk, Room room) : base(name,hp,def,atk)
        {
            _baseAttack = AttackPower;
            _baseDefense = DefensePower;
            InRoom = room;
        }
        public bool Move(string direction)
        {
            if ( room.accessRooms[direction] != null )
            {
                InRoom = room.accessRooms[direction];
                return true;
            }
            return false;
        }
        public void PickUpItem(Item newItem)
        {
            foreach (Item item in Inventory)
            {
                if (item == null) item = newItem;
            }
        }
        public void Heal(Item potion)
        {
            hp += potion.buff;
            foreach (Item item in Inventory)
            {
                if (item == potion)
                {
                    item = null;
                    break;
                }
            }
        }
        public void Equip()
        {
            if (newItem is Weapon)
            {
                AttackPower = _baseAttack + newItem.AttackPower;
                Weapon = newItem;
            }
            else if (newItem is Shield)
            {
                Defense = _baseDefense + newItem.Defense;
                Shield = newItem;
            }
        }
    }
}