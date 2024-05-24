using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public interface IView
    {
        public void RoomDescription(Room room);
        public void ActionResult();
        public void PrintStatus();
        public void PickupItem();
        public void ItemInformation();
        public void TurnNarrator();
        public void ByeBye();
        public void GameOver();
        public void GameWin(); 
    }
}