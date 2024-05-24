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
-----------------------------------------------------------------------------------------------
Health: {character.HP}
Attack Power: {character.AttackPower} ({character.BaseAttack} + {character.Weapon.BuffValue})
Defense: {character.Defense} ({character.BaseDefense} + {character.Shield.BuffValue})
-----------------------------------------------------------------------------------------------
                            ");
        }
        public void PickupItem(Item item)
        {
            Console.WriteLine($"You added {item.Name} to your inventory.");
        }
        public void ItemInformation(Item item)
        {
            Console.WriteLine($"{item.Name} - {item.Type} - {item.BuffValue}");
        }
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