using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
    /// <summary>
    /// Class that houses all the 'View' part of the MVC model, be it for menus, other UI or simple prints
    /// </summary>
{
    public class GameView : IView
    {
        /// <summary>
        /// Method to make it easier to print colored text
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        public void ColoredText(string str, ConsoleColor color) 
        {
            //Change console foreground color
            Console.ForegroundColor = color;
            //Print given string to the console
            Console.Write(str);
            //Reset console color
            Console.ResetColor();
        }
        /// <summary>
        /// A method to print out the Start Menu dialogue that comes up every single startup and the options
        /// to Start te game or Quit
        /// </summary>
        /// <returns>Returns the string corresponding to the choice the player makes about wether to start or quit</returns>
        public string StartMenu()
        {
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
            Console.WriteLine("Dungeon Master:");
            Console.WriteLine("Greetings... have I ever laid mine eyes on thee before?");
            Console.WriteLine("Dost thou not feele like talking?");
            Console.WriteLine("Write it down... if thou knowest how to do such thing that is");
            Console.WriteLine("What hath brought thee here then?\n");

            Console.WriteLine("(Write down what you want to do)");
            Console.WriteLine("New Game");
            Console.WriteLine("Quit");

            Console.Write(">");
            string s = Console.ReadLine();
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");

            return s;
        }
        /// <summary>
        /// A group of prints that aim to give a little bit of a flavorful introduction to the game setting and
        /// at the same time, inform the player about the objectives in the game and how to interact with the game
        /// text-based action system, while informing them how to navigate the dungeon
        /// </summary>
        public void ExplainNewGame()
        {
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
            Console.WriteLine("Dungeon Master:");
            Console.WriteLine(@"
This is but a simple prison. A simple yet perilous one at that!
Thou should not take these challenges lightly as the place thou art delving into is ruthless and unforgiving...
But don't thou take me as a villain just because of this lair of mine!
I am here not only to challenge thee but to guide thee through it as well:

1. This prison is composed of many chambers and one of them leads thee to the exit. Find it.
2. Write down simple command spell to explore this dungeon and interact with its elements!
    Here's a comprehensive list of such spells:
        .'Move [direction]' - You investigate a possible way in the direction you choose ('North', 'West', 'South', 'East')  
        .'Attack' - Use it in battles to inflict damage on the enemy 
        .'Heal' - Use it in or out of battles to select a potion if you have any in order to heal up
        .'Pick up item' - If there is an item in the room you're in, use this to pick it up
        .'Equip' - Use it to equip //ver como vai se ro processo de selção de items
        .'Inventory' - Use it to open your invotry and chekc your items 
3. Each chamber is different and may or may not lead to another chamber depending on which direction thou choose to follow.
4. Each chamber may contain an item or/and an opponent, in which case victory in battle is obligatory to proceed!

Stay sharp and use thy items wisely to navigate this prison and maybe overcome it...");
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
        }
        /// <summary>
        /// prints out the description of a specific room
        /// </summary>
        /// <param name="room"></param>
        public void RoomDescription(Room room)
        {
            Console.WriteLine(room.Description);
        }
        /// <summary>
        /// This method is aiming to be a general non-specific input request
        /// </summary>
        /// <returns>String with player's input</returns>
        public string AwaitDecision()
        {
            Console.WriteLine("What will you do, adventurer?");
            Console.Write(">");
            string s = Console.ReadLine();
            return s;
        }
        /// <summary>
        /// This method is for requesting the player's input in a battle situation
        /// </summary>
        /// <returns>String with player's input</returns>
        public string AwaitBattleInput()
        {
            Console.WriteLine("The battlefield is ready for your decision. Quick!");
            Console.WriteLine("1 - Attack    2 - Heal");
            Console.Write(">");
            string s = Console.ReadLine();
            return s;
        }
        /// <summary>
        /// This method is for requesting the player's input when th player is exploring a room
        /// </summary>
        /// <returns>String with player's input</returns>
        public string AwaitRoomInput()
        {
            Console.WriteLine("The dungeon awaiteth thy decision...");
            Console.Write(">");
            string s = Console.ReadLine();
            return s;
        }
        /// <summary>
        /// Print the result of an Attack executed by one Character on another
        /// </summary>
        /// <param name="characterActive"></param>
        /// <param name="characterPassive"></param>
        /// <param name="hitPower"></param>
        public void AttackResult(Character characterActive, Character characterPassive, int hitPower)
        {
            Console.WriteLine($"{characterActive.Name} attacked {characterPassive} for {characterActive.Attack(characterPassive)} damage!");
        }
        /// <summary>
        /// Print that accompanies a communicates to the player that he has moved to the next room
        /// </summary>
        public void CanMove()
        {
            Console.WriteLine("Your journey advances into the next chamber");
        }
        /// <summary>
        /// Print affirming the player can't move that way when exploring rooms
        /// </summary>
        public void CantMove()
        {
            Console.WriteLine("Thy can not pass through there");
        }
        public void HealResult(Item potion)
        {
            Console.WriteLine($"You heal for {potion.BuffValue}");
        }
        public void PlayerStatus(Player character)
        {     
            Console.WriteLine(@$"
{character.Name}, your poor state is the following:
|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
            Console.WriteLine($"Health: {character.HP}");
            //Prints for the Attack Power stat vision
            Console.Write($"Attack Power: {character.AttackPower} ");
            ColoredText("( ", ConsoleColor.Gray); 
            ColoredText($"{character.BaseAttack}",ConsoleColor.DarkBlue);
            ColoredText(" + ", ConsoleColor.Gray); 
            ColoredText($"{character.Weapon.BuffValue}",ConsoleColor.DarkGreen);
            ColoredText(" )\n", ConsoleColor.Gray);

            //Prints for the Defense stat vision
            Console.Write($"Attack Power: {character.Defense} ");
            ColoredText("( ", ConsoleColor.Gray); 
            ColoredText($"{character.BaseDefense}",ConsoleColor.DarkBlue);
            ColoredText(" + ", ConsoleColor.Gray); 
            ColoredText($"{character.Shield.BuffValue}",ConsoleColor.DarkGreen);
            ColoredText(" )\n", ConsoleColor.Gray);

            Console.WriteLine(@$"
|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
        }
        public void PickupItem(Item item)
        {
            Console.WriteLine($"You added {item.Name} to your inventory.");
        }
        public void ItemInformation(Item item)
        {
            Console.WriteLine($"{item.Name} - {item.Type} - {item.BuffValue}");
        }
        public void WarningItemNotInInventory()
        {
            Console.WriteLine("What thou seekest is not in thy possession.");
        }
        public void WarningNeedName()
        {
            Console.WriteLine("Each adventurer hath a name, thou shouldst be no exception.\nName, please?");
        }
        public void WarningWrongCommand()
        {
            Console.WriteLine("Something is amiss with that command spell...");
        }
        public void WarningNoItemToPickUp()
        {
            Console.WriteLine("There is naught to pick up...");
        }
        public void WarningNotShieldOrSword()
        {
            Console.WriteLine("That is not a piece of equipment.");
        }
        public void ByeBye()
        {
            Console.WriteLine("Thou wishest to rest?");
            Console.WriteLine("Very well... the dungeon shall await thy return.");
        }
        public void GameOver()
        {
            Console.WriteLine("Death. A sweet relief...");
        }
        public void GameWin()
        {
            Console.WriteLine("Triumphant victory!");
        }
    }
}