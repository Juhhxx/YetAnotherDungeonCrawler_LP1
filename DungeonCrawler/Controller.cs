using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DungeonCrawler
{
    public class Controller : Controller
    {
        private IView view;
        private Player player;

        public Controller(IView view, Player player)
        {
            
        }
        public void Start()
        {
            // hello message + instructions

            string input;
            string[] inputArray;
            bool hasActed;

            while (true)
            {
                view.RoomDescription(player.InRoom);

                while ( !hasActed )
                {
                    input = view.AwaitDecision();
                    inputArray = input.Trim().ToLower().Split(' ');

                    if (inputArray[0] == "go")
                    {
                        hasActed = player.Move(inputArray[1]);
                        hasActed ? view.CanMove() : view.CantMove();
                    }
                    else if (inputArray[0] == "pick" && inputArray[1] == "up")
                    {
                        hasActed = HandleItemAction(inputArray[2], true, player.PickupItem);
                    }
                    else if (inputArray[0] == "equip")
                    {
                        hasActed = HandleItemAction(inputArray[1], false, player.Equip);
                    }
                    else if (inputArray[0] == "attack")
                    {
                        player.Attack(player.InRoom.REnemy)
                    }
                    else if (inputArray[0] == "view" && inputArray[1] == "status")
                    {
                        view.PlayerStatus(player);
                    }
                    else if (inputArray[0] == "exit")
                    {
                        view.ByeBye();
                        break;
                    }
                    else
                    {
                        view.WrongCommand();
                    }
                }

                hasActed = false;
            }
        }
        public HandleItemAction(string input, Action<Item> playerAction, bool isPotion)
        {
            bool result = false;

            if ( !player.CheckForItem() )
            {
                // print warning - there is no item to pick up
            }
            else
            {
                try 
                {
                    Item newItem = player.ConfirmItem(input, isPotion);

                    if ( newItem != null)
                    {
                        playerAction(newItem);
                        view.PickupItem(newItem);
                        result = true;
                    }
                    else
                    {
                        // print warning - item is not shield or sword
                    }
                }
                catch (Exception e)
                {
                    //  print warning - need to insert a name
                }
            }
            return result;
        }
    }
}