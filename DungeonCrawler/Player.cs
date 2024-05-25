using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Player : Character
    {
        public List<Item> Inventory { get; private set;} = new List<Item>();
        public Item Weapon { get; private set; }
        public Item Shield { get; private set; }
        public Room InRoom{ get; private set; }
        public int BaseAttack{ get; private set;}
        public int BaseDefense{ get; private set;}
        public Player(string name, int hp, int atk, int def, Room room) : base(name,hp,atk,def)
        {
            BaseAttack = AttackPower;
            BaseDefense = Defense;
            InRoom = room;
        }
        public bool Move(string direction)
        {
            if ( InRoom.accessRooms[direction] != null )
            {
                InRoom = InRoom.accessRooms[direction];
                return true;
            }
            return false;
        }
        public void PickUpItem(Item newItem)
        {
            Inventory.Add(newItem);
            InRoom.RemoveItem();
        }
        public void Heal(Item potion)
        {
            HP += potion.BuffValue;
            Inventory.Remove(potion);
        }
        public void Equip(Item newItem)
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
            InRoom.RemoveItem();
        }
        public Item ConfirmItem(bool isPotion)
        {
            Item item = null;

            if ( isPotion && InRoom.RItem.Type == BuffType.HP)
            {
                item = InRoom.RItem;
            }
            else if ( !isPotion && !(InRoom.RItem.Type == BuffType.HP))
            {
                item = InRoom.RItem;
            }

            return item;
        }
        public Enemy ConfirmEnemy()
        {
            return InRoom.REnemy;
        }
        public bool CheckForItem()
        {
            return InRoom.RItem != null;
        }
        public bool CheckForEnemy()
        {
            return InRoom.REnemy != null;
        }
        public Item SearchInInventory(string name)
        {
            Item item = null;

            foreach (Item i in Inventory)
            {
                if ( i.Name == name) 
                {
                    item = i;
                    break;
                }
            }

            return item;
        }
    }
}