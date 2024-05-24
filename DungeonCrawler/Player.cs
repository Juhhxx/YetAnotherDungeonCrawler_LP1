using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Player : Character
    {
        public List<Item> Inventory { get; private set;} = new List<Item>;
        public Item Weapon { get; private set; }
        public Item Shield { get; private set; }
        public Room InRoom{ get; private set; }
        public int BaseAttack{ get; private set;}
        public int BaseDefense{ get; private set;}
        public Player(string name, int hp, int def, int atk, Room room) : base(name,hp,def,atk)
        {
            BaseAttack = AttackPower;
            BaseDefense = DefensePower;
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
            Inventory.Add(newItem);
        }
        public void Heal(Item potion)
        {
            hp += potion.BuffValue;
            int index = Inventory.FindIndex(potion);
            if (index != -1)
            {
                Inventory[index] = null;
            }
        }
        public void Equip()
        {
            if (newItem.Type == BuffType.AttackPower)
            {
                AttackPower = BaseAttack + newItem.BuffValue;
                Weapon = newItem;
            }
            else if (newItem.Type == BuffType.Defense)
            {
                Defense = BaseDefense + newItem.BuffValue;
                Shield = newItem;
            }
        }
    }
}