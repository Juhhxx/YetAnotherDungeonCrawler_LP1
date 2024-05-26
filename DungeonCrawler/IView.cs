using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public interface IView
    {
        public string StartMenu(); //Elaborar mais nesta ideia de menu
        public void ExplainNewGame();
        public void ColoredText(string str, ConsoleColor color);
        public void RoomDescription(Room room);
        public string AwaitDecision();
        public string AwaitBattleInput();
        public string AwaitRoomInput();
        public void AttackResult(Character characterActive, Character characterPassive, int hitPower );
        public void BattleWin();
        public void CantMove();
        public void CanMove();
        public void HealResult(Item potion);
        public void PlayerStatus(Player character);
        public string AskPickUpItem(Item item);
        public void PickUpItem(Item item);
        public void EquipItem(Item item);
        public void ItemInformation(Item item);
        public string ItemToUSe();
        public void WarningNoEnemiesToFight();
        public void WarningItemNotInInventory();
        public void WarningFullInventory();
        public void WarningNeedName();
        public void WarningWrongCommand();
        public void WarningNoItemToPickUp();
        public void WarningWrongItem();
        public void ByeBye();
        public void GameOver();
        public void GameWin(); 
    }
}