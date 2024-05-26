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
        private GameInitializer init;
        private bool end;
        private bool died;

        public Controller(IView view, Player player)
        {
            this.view = view;
            this.player = player;
            init = new GameInitializer(player);
            end = false;
        }
        public void Start()
        {
            string input;

            // print main menu

            while (true)
            {
                if (end) break;

                input = view.StartMenu().Trim().ToLower();

                switch (input)
                {
                    case "new game":
                        view.ExplainNewGame();
                        init.InitializeGame();

                        // start new game ?

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

            view.RoomDescription(player.InRoom);

            while ( true )
            {
                input = view.AwaitDecision().Trim().ToLower();

                switch ( input )
                {
                    case "move":
                        HandleRoomAction();
                        break;
                    case "pick up":
                        HandleItemAction(true, player.PickUpItem);
                        break;
                    case "equip":
                        HandleItemAction(false, player.Equip);
                        break;
                    case "attack":
                        HandleEnemyAction();
                        break;
                    case "view status":
                        view.PlayerStatus(player);
                        break;
                    case "heal":
                        HandleHealAction();
                        break;
                    case "quit":
                        view.ByeBye();
                        end = true;
                        break;
                    default:
                        // print warning - command was wrong
                        view.WarningWrongCommand();
                        break;
                }

                if (end || died)
                {
                    died = false;
                    break;
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
                        if (player.FoundFinalRoom())
                        {
                            view.GameWin();
                            end = true;
                        }
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
        public void HandleItemAction( bool isPotion, Func<Item, bool> playerAction)
        {
            if ( !player.CheckForItem() )
            {
                // print warning - there is no item to pick up
                view.WarningNoItemToPickUp();
            }
            else
            {
                Item newItem = player.ConfirmItem(isPotion);

                view.ItemInformation(newItem);

                if ( newItem != null && TakeItem(newItem) )
                {
                    if ( playerAction(newItem) )
                    {
                        if (isPotion) view.PickUpItem(newItem);
                        else view.EquipItem(newItem);
                    }
                    else
                    {
                        // print warning - inventory full
                    }
                    
                }
                else
                {
                    // print warning - item is the wrong type
                    view.WarningNotShieldOrSword();
                }
            }
        }
        public void HandleEnemyAction()
        {
            if ( player.CheckForEnemy() )
            {
                Enemy enemy = player.ConfirmEnemy();
                StartCombat(enemy);
            }
            else
            {
                // print warning - there is no enemy to fight
            }
        }
        public bool HandleHealAction()
        {
            bool result = false;
            
            // print - ask which item they want to use
            string input = view.AwaitDecision().Trim().ToLower();

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
                            if (enemy.IsDead())
                            {
                                // print outcome - you won the battle
                                return;
                            }
                            hasActed = true;
                            break;
                        case "heal":
                            hasActed = HandleHealAction();
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

                if (player.IsDead())
                {
                    view.GameOver();
                    end = true;
                    break;
                }
            }
        }
    }
}