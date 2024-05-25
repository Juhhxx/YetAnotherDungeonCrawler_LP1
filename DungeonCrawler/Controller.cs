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
        private bool end;
        private bool died;

        public Controller(IView view, Player player)
        {
            this.view = view;
            this.player = player;
            end = false;
        }
        public void Start()
        {
            string input;

            // print hello message
            while (true)
            {
                if (end) break;

                input = view.StartMenu().Trim().ToLower();

                switch (input)
                {
                    case "new game":
                        view.ExplainNewGame();

                        // start new game ?

                        StartGameLoop();
                        break;
                    case "continue":

                        // open save file ?

                        StartGameLoop();
                        break;
                    case "quit":
                        view.ByeBye();
                        return;
                    default:
                        // print warning - command was wrong
                        view.WarningWrongCommand();
                        break;
                }
            }
        }
        public void StartGameLoop()
        {
            string input;
            string[] inputArray;

            view.RoomDescription(player.InRoom);

            while (true)
            {
                input = view.AwaitDecision();
                inputArray = input.Trim().ToLower().Split(' ');

                if (inputArray[0] == "move")
                {
                    HandleRoomAction();
                    if (end) break;
                }
                else if (inputArray[0] == "pick" && inputArray[1] == "up")
                {
                    HandleItemAction(inputArray[2], true, player.PickUpItem);
                }
                else if (inputArray[0] == "equip")
                {
                    HandleItemAction(inputArray[1], false, player.Equip);
                }
                else if (inputArray[0] == "attack")
                {
                    HandleEnemyAction(inputArray[1]);
                    if (end || died)
                    {
                        died = false;
                        break;
                    }
                }
                else if (inputArray[0] == "view" && inputArray[1] == "status")
                {
                    view.PlayerStatus(player);
                }
                else if (inputArray[0] == "heal")
                {
                    HandleHealAction(inputArray[1]);
                }
                else if (inputArray[0] == "quit")
                {
                    view.ByeBye();
                    end = true;
                    break;
                }
                else
                {
                    // print warning - command was wrong
                    view.WarningWrongCommand();
                }
            }
        }
        public void HandleRoomAction()
        {
            string input;

            while (true)
            {
                input = view.AwaitRoomInput().Trim().ToLower();

                if ((input == "north") || (input == "south") || (input == "east") || (input == "west"))
                {
                    bool result = player.Move(input);

                    if (result)
                    {
                        view.CanMove();
                        view.RoomDescription(player.InRoom);
                    } 
                    else view.CantMove();
                    break;
                }
                else if (input == "quit")
                {
                    view.ByeBye();
                    end = true;
                    break;
                }
                else
                {
                    // print warning - command was wrong
                    view.WarningWrongCommand();
                }
            }
        }
        public void HandleItemAction(string input, bool isPotion, Action<Item> playerAction)
        {
            if ( !player.CheckForItem() )
            {
                // print warning - there is no item to pick up
                view.WarningNoItemToPickUp();
            }
            else
            {
                try 
                {
                    Item newItem = player.ConfirmItem(input, isPotion);

                    if ( newItem != null && TakeItem(newItem) )
                    {
                        playerAction(newItem);
                        view.PickupItem(newItem);
                    }
                    else
                    {
                        // print warning - item is not shield or sword
                        view.WarningNotShieldOrSword();
                    }
                }
                catch (Exception e)
                {
                    //  print warning - need to insert a name
                    view.WarningNeedName();
                }
            }
        }
        public void HandleEnemyAction(string input)
        {
            if ( !player.CheckForEnemy() )
            {
                // print warning - there is no enemy to fight
            }
            else
            {
                try
                {
                    Enemy enemy = player.ConfirmEnemy(input);

                    if (enemy != null)
                    {
                        StartCombat(enemy);
                    }
                    else
                    {
                        // print warning - there is no enemy with that name

                    }
                }
                catch (Exception e)
                {
                    //  print warning - need to insert a name
                    view.WarningNeedName();
                }
            }
        }
        public bool HandleHealAction(string input)
        {
            bool result = false;

            try
            {
                Item newItem = player.SearchInInventory(input);

                if ( newItem != null )
                {
                    player.Heal(newItem);
                    view.HealResult(newItem);
                    result = true;
                }
                else
                {
                    // print warning - item is not in inventory
                    view.WarningItemNotInInventory();
                }
            }
            catch (Exception e)
            {
                //  print warning - need to insert a name
                view.WarningNeedName();
            }

            return result;
        }
        public bool TakeItem(Item item)
        {
            bool result;
            string input;

            while (true)
            {
                // print - ask if they wanna take the item or not

                input = view.AwaitDecision().Trim().ToLower();

                if ( input == "take" )
                {
                    result = true;
                    break;
                } 
                else if ( input == "leave" )
                {
                    result = false;
                    break;
                }
                else 
                {
                    // print warning - command was wrong
                    view.WarningWrongCommand();
                }
            }

            return result;
        }
        public void StartCombat(Enemy enemy)
        {
            int hitPower;
            string input;
            string[] inputArray;
            bool hasActed;

            while (true)
            {
                hasActed = false;

                while ( !hasActed )
                {
                    input = view.AwaitBattleInput();
                    inputArray = input.Trim().ToLower().Split(' ');

                    switch (inputArray[0])
                    {
                        case "attack":
                            hitPower = player.Attack(enemy);
                            view.AttackResult(player, enemy, hitPower);
                            if (enemy.HP <= 0)
                            {
                                // print outcome - you won the battle
                                return;
                            }
                            hasActed = true;
                            break;
                        case "heal":
                            hasActed = HandleHealAction(inputArray[1]);
                            break;
                        case "view":
                            if (inputArray[1] == "status")
                            {
                                view.PlayerStatus(player);
                                hasActed = true;
                                break;
                            }
                            // print warning - command was wrong
                            view.WarningWrongCommand();
                            break;
                        case "quit":
                            view.ByeBye();
                            return;
                        default:
                            // print warning - command was wrong
                            view.WarningWrongCommand();
                            break;
                    }
                }

                hitPower = enemy.Attack(player);
                view.AttackResult(enemy, player, hitPower);

                if (player.HP <= 0)
                {
                    view.GameOver();
                    end = true;
                    break;
                }
            }
        }
    }
}