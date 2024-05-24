using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Controller
    {
        private IView view;
        private Player player;

        public Controller(IView view, Player player)
        {
            
        }
        public void Start()
        {
            // print hello message + instructions

            string input;
            string[] inputArray;
            bool hasActed = false;

            while (true)
            {
                view.RoomDescription(player.InRoom);

                while ( !hasActed )
                {
                    // print instructions - commands and usage examples
                    input = view.AwaitDecision();
                    inputArray = input.Trim().ToLower().Split(' ');

                    if (inputArray[0] == "go")
                    {
                        hasActed = player.Move(inputArray[1]);
                        if (hasActed) view.CanMove();
                        else view.CantMove();
                    }
                    else if (inputArray[0] == "pick" && inputArray[1] == "up")
                    {
                        hasActed = HandleItemAction(inputArray[2], true, player.PickUpItem);
                    }
                    else if (inputArray[0] == "equip")
                    {
                        hasActed = HandleItemAction(inputArray[1], false, player.Equip);
                    }
                    else if (inputArray[0] == "attack")
                    {
                        player.Attack(player.InRoom.REnemy);
                    }
                    else if (inputArray[0] == "view" && inputArray[1] == "status")
                    {
                        view.PlayerStatus(player);
                    }
                    else if (inputArray[0] == "heal")
                    {
                        try
                        {
                            Item newItem = player.SearchInInventory(inputArray[1]);

                            if (newItem == null)
                            {
                                // print warning - item is not in inventory
                                continue;
                            }
                            view.HealResult(newItem);
                            player.Heal(newItem);
                            hasActed = true;
                        }
                        catch (Exception e)
                        {
                            //  print warning - need to insert a name
                        }
                    }
                    else if (inputArray[0] == "exit")
                    {
                        view.ByeBye();
                        break;
                    }
                    else
                    {
                        // print warning - command was wrong
                    }
                }

                hasActed = false;
            }
        }
        public bool HandleItemAction(string input, bool isPotion, Action<Item> playerAction)
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