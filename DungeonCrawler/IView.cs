using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public interface IView
    {
        public void StartMenu(); //Elaborar mais nesta ideia de menu
        public void ExplainGame();
        public void ColoredText(string str, ConsoleColor color);
        public void RoomDescription(Room room);
        public string AwaitDecision();
        public void AttackResult(Character characterActive, Character characterPassive );
        public void CantMove();
        public void CanMove();

        public void HealResult(Item potion);
        public void PlayerStatus(Player character);
        public void PickupItem(Item item);
        public void ItemInformation(Item item);
        public void ByeBye();
        public void GameOver();
        public void GameWin(); 
    }
}