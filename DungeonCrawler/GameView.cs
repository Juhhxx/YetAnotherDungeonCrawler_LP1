using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class GameView : IView
    {
        public void RoomDescription(Room room)
        {
            Console.WriteLine(room.Description);
        }
        public void AttackResult(Character characterActive, Character characterPassive )
        {
            Console.WriteLine($"{characterActive.Name} attacked {characterPassive} for {characterActive.Attack(characterPassive)} damage!");
        }
        public void MoveResult()
        {

        }
        public void HealResult(Item potion)
        {

        }
        public void PrintStatus(Character character)
        {

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