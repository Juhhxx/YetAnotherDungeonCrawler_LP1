using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    /// <summary>
    /// The controller class is responsible for the loop of the game, for the logic behind every game/player interaction
    /// And to use the view and model together to make the interfaces of the game work
    /// </summary>
    public class Controller
    {
        // Initializes the view
        private IView view;
        // Initializes the player
        private Player player;
        // Initializes the gameInitializer
        private GameInitializer init;
        // Initializes the bool end
        private bool end;
        // Initializes the bool died
        private bool died;

        /// <summary>
        /// Controller class constructor
        /// </summary>
        /// <param name="view">The view reference for the view to be utilized by the controller.</param>
        /// <param name="player">The player reference of the player to be utilized by the controller.</param>
        public Controller(IView view, Player player)
        {
            this.view = view;
            this.player = player;
            init = new GameInitializer(player);
            end = false;
        }

        /// <summary>
        /// Main menu method that asks the player if they want to start a new game or quit, and 
        /// loops back if they wrote the wrong command. The player will be back to the main menu
        /// when they die or win the game. And checks if the player has chosen to end the game,
        /// if true, the main menu loop ends, and consequently the game too.
        /// </summary>
        public void Start()
        {
            string input;

            while (true)
            {
                if (end) break;

                // print main menu
                input = view.StartMenu().Trim().ToLower();

                switch (input)
                {
                    case "new game":
                        view.ExplainNewGame();
                        init.InitializeGame();

                        // start new game

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

        /// <summary>
        /// The game loop method where the player will stay in as long as they don't win, die or quit.
        /// The player has 7 options in this menu: move, pick up item, equip item, attack, view status,
        /// heal, or quit. If their command is incorrect an error appears and the menu loops back.
        /// </summary>
        public void StartGameLoop()
        {
            string input;

            CheckForDescriptions(player.InRoom);

            while ( true )
            {
                input = view.AwaitDecision().Trim().ToLower();

                switch ( input )
                {
                    case "move":
                        HandleRoomAction();
                        break;
                    case "pick up item":
                        HandleItemAction(true, player.PickUpItem);
                        break;
                    case "equip item":
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
        /// <summary>
        /// HandleRoomAction is a method that while the player does not input either north, south,
        /// east, west or quit, will ask him for a direction to go in. If the wrong command is inserted,
        /// The menu loops back. If he can move in the desired direction and the room is the Final
        /// Room the Player wins the game. Else he just moves to a new room, or does not move at
        /// all.
        /// </summary>
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
                        CheckForDescriptions(player.InRoom);
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

        /// <summary>
        /// HandleItemAction is a method capable of adding items to the Player's inventory or
        /// equipping them. First it checks if there even is an item in the room, giving a warning to
        /// the player if not. Else it will confirm that the item is either pick up-able or equip-able
        /// And then it will give the item information to the Player and ask them if they want to
        /// take it. If the player takes it, and the Inventory is not full, it will tell the player the
        /// item was kept.
        /// </summary>
        /// <param name="isPotion">The bool that says if the item is supposed to be a potion or a shield/sword.</param>
        /// <param name="playerAction">The action that the Player is supposed to take depending if they want to equip or pick up the item.</param>
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
                        view.WarningFullInventory();
                    }
                    
                }
                else
                {
                    // print warning - item is the wrong type
                    view.WarningWrongItem();
                }
            }
        }

        /// <summary>
        /// HandleItemAction checks if there is an enemy in the room, and if so it will confirm the enemy
        /// and start a battle between it and the player. If there are no enemies in the room a warning
        /// will appear.
        /// </summary>
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
                view.WarningNoEnemiesToFight();
            }
        }

        /// <summary>
        /// HandleHealAction will first ask the player which item they want to use, and if the input
        /// corresponds to an item in the Inventory, it will heal the Player and show the action's
        /// result, else it will warn the Player that no such item is in their inventory.
        /// </summary>
        /// <returns>The method returns true if the action was successful.</returns>
        public bool HandleHealAction()
        {
            bool result = false;
            
            // print - ask which item they want to use
            string input = view.ItemToUse().Trim().ToLower();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool TakeItem(Item item)
        {
            bool result;
            string input;

            while (true)
            {
                // print - ask if they wanna take the item or not

                input = view.AskPickUpItem(item).Trim().ToLower();

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
                                view.BattleWin();
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
        /// <summary>
        /// This method describes a given room and the current room's Enemy and
        /// Item only if they exit.
        /// </summary>
        /// <param name="room">The room to write the description of.</param>
        public void CheckForDescriptions(Room room)
        {
            view.RoomDescription(room);
            if (player.CheckForEnemy())
            {
                view.EnemyDescription(room.REnemy);
            }
            if (player.CheckForItem())
            {
                view.ItemDescription(room.RItem);
            }
        }
    }
}