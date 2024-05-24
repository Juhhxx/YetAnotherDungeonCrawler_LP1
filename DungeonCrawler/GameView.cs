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
            Console.WriteLine
(@$"{character.Name}, your poor state is the following:
-------------------------------------------------------
Health: {character.HP}
Attack Power: {character.AttackPower} ({character.BaseAttack} + {character.Weapon.BuffValue})
Defense: {character.Defense} ({character.BaseDefense} + {character.Shield.BuffValue})
                                    ");
        }
        public void PickupItem(Item item)
        {

        }
        public void ItemInformation(Item item)
        {

        }
        public void ByeBye()
        {

        }
        public void GameOver()
        {

        }
        public void GameWin()
        {

        }
    }
}