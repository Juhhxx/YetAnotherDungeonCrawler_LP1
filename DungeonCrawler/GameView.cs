using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class GameView : IView
    {
        public void ColoredText(string str, ConsoleColor color) 
        {
            //Change console foreground color
            Console.ForegroundColor = color;
            //Print given string to the console
            Console.Write(str);
            //Reset console color
            Console.ResetColor();
        }
        public string StartMenu()
        {
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");
            Console.WriteLine("Dungeon Master:");
            Console.WriteLine("Greetings... have I ever laid mine eyes on thee before?");
            Console.WriteLine("Dost thou not feele like talking?");
            Console.WriteLine("Write it down... if thou knowest how to do such thing that is");
            Console.WriteLine("What hath brought thee here then?\n");

            Console.WriteLine("New Game");
            Console.WriteLine("Continue");
            Console.WriteLine("Quit");

            Console.Write(">");
            string s = Console.ReadLine();
            Console.WriteLine("|-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-|");

            return s;
        }

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
        
        public void RoomDescription(Room room)
        {
            Console.WriteLine(room.Description);
        }
        public string AwaitDecision()
        {
            Console.Write(">");
            string s = Console.ReadLine();
            return s;
        }

        public void AttackResult(Character characterActive, Character characterPassive )
        {
            Console.WriteLine($"{characterActive.Name} attacked {characterPassive} for {characterActive.Attack(characterPassive)} damage!");
        }
        public void CanMove()
        {
            Console.WriteLine("Your journey advances into the next room");
        }
        public void CantMove()
        {
            Console.WriteLine("Not through there");
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
            Console.WriteLine("What you search for is not in our possession");
        }
        public void WarningNeedName()
        {
            Console.WriteLine("Every adventurer has a name, you should be no exception");
        }
        public void WarningWrongCommand()
        {}
        public void WarningNoItemToPickUp()
        {}
        public void WarningNotShieldOrSword()
        {}
        public void ByeBye()
        {
            Console.WriteLine("You wish to rest?");
            Console.WriteLine("Very well... the dungeon will be waiting.");
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