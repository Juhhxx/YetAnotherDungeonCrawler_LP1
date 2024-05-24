using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public interface IView
    {
        public void ColoredText(string str, ConsoleColor color);
        public void RoomDescription(Room room);
        public void AttackResult(Character characterActive, Character characterPassive );
        public void CantMove();
        public void CanMove();

        public void HealResult(Item potion);
        public void PlayerStatus(Character character);
        public void PickupItem(Item item);
        public void ItemInformation(Item item);
        public void ByeBye();
        public void GameOver();
        public void GameWin(); 
    }
}