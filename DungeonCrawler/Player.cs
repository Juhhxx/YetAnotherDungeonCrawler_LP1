using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    /// <summary>
    /// Class for the player, that controls the player's traits, how the player works and interacts with the room they are in.
    /// </summary>
    public class Player : Character
    {
        // Initialize Inventory List of potions.
        public List<Item> Inventory { get; private set;} = new List<Item>();
        // Initialize Weapon slot.
        public Item Weapon { get; private set; }
        // Initialize Shield slot.
        public Item Shield { get; private set; }
        // Initialize current Room.
        public Room InRoom{ get; private set; }
        // Initialize base attack value.
        public int BaseAttack{ get; private set;}
        // Initialize base defense value.
        public int BaseDefense{ get; private set;}

        /// <summary>
        /// Constructor for Player class.
        /// </summary>
        /// <param name="name">The name to be used by Player which is returned to Character base.</param>
        /// <param name="hp">The HP to be used by Player which is returned to Character base.</param>
        /// <param name="atk">The attack power to be used by Player which is returned to Character base.</param>
        /// <param name="def">The defense to be used by Player which is returned to Character base.</param>
        public Player(string name, int hp, int atk, int def) : base(name,hp,atk,def)
        {
            BaseAttack = atk;
            BaseDefense = def;
        }

        /// <summary>
        /// Method to set the room that the player is initially in.
        /// </summary>
        /// <param name="initialRoom">The initial room to set InRoom to.</param>
        public void SetInitialRoom(Room initialRoom)
        {
            InRoom = initialRoom;
        }

        /// <summary>
        /// Method that moves the player between rooms, by checking if the given direction,
        /// in the current room's accessible rooms library, is not blocked off.
        /// </summary>
        /// <param name="direction">String to check the direction of.</param>
        /// <returns>moves the player and returns true if the player can walk. Else returns false.</returns>
        public bool Move(string direction)
        {
            if ( InRoom.accessRooms[direction] != null )
            {
                InRoom = InRoom.accessRooms[direction];
                return true;
            }
            return false;
        }

        /// <summary>
        /// Receives an Item and Adds it to the Inventory if it is not full.
        /// </summary>
        /// <param name="newItem">Item to add to Inventory.</param>
        /// <returns>Returns true if the inventory is not full, returns false if it is full.</returns>
        public bool PickUpItem(Item newItem)
        {
            if ( Inventory.Count < 5 )
            {
                Inventory.Add(newItem);
                InRoom.RemoveItem();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the buff a potion gives to the health of the Player, and then removes the potion
        /// from the Player's Inventory.
        /// </summary>
        /// <param name="potion">Item potion to use the buff of.</param>
        public void Heal(Item potion)
        {
            HP += potion.BuffValue;
            Inventory.Remove(potion);
        }

        /// <summary>
        /// Adds the buff from a weapon or shield gives to the attack power or defense of the
        /// Player sets it to weapon or shield property, 
        /// and then removes the item from the Player's current room.
        /// </summary>
        /// <param name="newItem">The weapon or shield for the player to use.</param>
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
        /// <summary>
        /// Method to confirm if the item in the Player's current room is or not a potion.
        /// </summary>
        /// <param name="isPotion">Boolean to check for.</param>
        /// <returns>If the method confirms the item to be or not a potion, it returns the item, else it returns null.</returns>
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

        /// <summary>
        /// Method that returns the Player's current room's enemy.
        /// </summary>
        /// <returns>It will return null if there is no enemy, and return the enemy if it is not null.</returns>
        public Enemy ConfirmEnemy()
        {
            return InRoom.REnemy;
        }

        /// <summary>
        /// Method that returns if the Player's current room has an item.
        /// </summary>
        /// <returns>It will return false if the item is null, and return true if the item is not null.</returns>
        public bool CheckForItem()
        {
            return InRoom.RItem != null;
        }

        /// <summary>
        /// Method that returns if the Player's current room has an enemy.
        /// </summary>
        /// <returns>It will return false if the enemy is null, and return true if the enemy is not null.</returns>
        public bool CheckForEnemy()
        {
            return InRoom.REnemy != null;
        }

        /// <summary>
        /// Method that looks through the Inventory for the first item with a given name.
        /// </summary>
        /// <param name="name">The string name to look for in the Inventory.</param>
        /// <returns>It will return the item if it is not null, and return null if there is no item.</returns>
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

        /// <summary>
        /// Method that checks if the Player has found a final room.
        /// </summary>
        /// <returns>It returns the current room's IsFinal boolean.</returns>
        public bool FoundFinalRoom()
        {
            return InRoom.IsFinal;
        }
    }
}