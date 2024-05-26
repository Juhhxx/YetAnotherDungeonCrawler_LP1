using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DungeonCrawler
{
    /// <summary>
    /// Class that analyzes game files and instantiates all game objects.
    /// </summary>
    public class GameInitializer
    {
        // Dictionaries to store instances of objects
        Dictionary<string,Enemy> enemyDict = new Dictionary<string, Enemy>();
        Dictionary<string,Item> itemDict = new Dictionary<string, Item>();
        Dictionary<string,Room> roomDict = new Dictionary<string, Room>();
        // String used to analyze the files
        string s;
        // Enemy variables
        private string eName; // Enemy Name
        private string eDesc; //Enemy Description
        private int eHP; // Enemy HP
        private int eAtck; // Enemt Attack Power
        private int eDef; // Enemy Defence
        // Item variables
        private string iName; // Item Name
        private string iDesc; // Item Description
        private BuffType iType; // Item Buff Type
        private int iValue; // Item Buff Value
        // Room variables
        private string rName; // Room Name
        private string rDesc; // Room Description
        private Enemy rEnemy; // Room Enemy
        private Item rItem; // Room Item
        private List<string[]> roomDirectionsList = new List<string[]>(); // List to store iformation about the directions of all Rooms
        private string[] roomDirections; // Array to store what's on each direction of a Room
        private string rNorth; // North direction of a Room
        private string rSouth; // South direction of a Room
        private string rEast; // East direction of a Room
        private string rWest; // West direction of a Room
        // Bool value to know when all parameters are ready to instantiate an object
        private bool instanceReady = false;
        // Initial path for GameFiles folder
        private string pathFolder = Path.Combine("DungeonCrawler","GameFiles");
        // Variable to store the Player
        private Player player;
        /// <summary>
        /// Constructor for GameInitializer class.
        /// </summary>
        /// <param name="player">Player Character</param>
        public GameInitializer(Player player)
        {
            // Set the instance variable player as the given player
            this.player = player;
        }
        /// <summary>
        /// Call all of the initializer methods to start the game.
        /// </summary>
        public void InitializeGame()
        {
            // Initialize Enemies
            InitializeEnemies();
            // Initialize Items
            InitializeItems();
            // Initialize Rooms
            InitializeRooms();
            // Set the player in Initial Room
            player.SetInitialRoom(roomDict["Initial Room"]);
            // Set Final Room
            roomDict["Final Room"].SetAsFinalRoom();
        }
        /// <summary>
        /// Initialize all Enemy instances given in the Enemies.txt file.
        /// </summary>
        private void InitializeEnemies()
        {
            // Complete path to file
            string pathFile = Path.Combine(pathFolder,"Enemies.txt");
            // Initialize StreamReader with specific file
            using StreamReader sr = new StreamReader(pathFile);
            // While the line that is being read is not END do
            while ((s = sr.ReadLine()) != "END")
            {
                // If the line being read isn't empty
                if (s != "")
                {
                    // If the line being read starts with # continue on
                    if (s[0] == '#') continue;
                    // If not analyze it
                    else 
                    {
                        // Split the line by the ":" char
                        string[] parameters = s.Split(":");
                        // Analyze the first part
                        switch(parameters[0])
                        {
                            case "Name": // If equals Name
                                // Set eName as second part
                                eName = parameters[1];
                                break;
                            case "Description": //If equals Description
                                // Set eDesc as second part
                                eDesc = parameters[1];
                                break;
                            case "HP": // If equals HP
                                // Set eHP as second part
                                eHP = int.Parse(parameters[1]);
                                break;
                            case "AttackPower": // If equals AttackPower
                                // Set eAtck as second part
                                eAtck = int.Parse(parameters[1]);
                                break;
                            case "Defense": // If equals Defense
                                // Set eDef as second part
                                eDef = int.Parse(parameters[1]);
                                // Set instanceReady as true
                                instanceReady = true;
                                break;
                        }
                        // If instanceReady is true
                        if (instanceReady)
                        {
                            // Add new entry to enemyDict (eName,new Enemy instance)
                            enemyDict.Add(eName,new Enemy(eName,eDesc,eHP,eAtck,eDef));
                            // Set isntanceReady as false
                            instanceReady = false;
                        }
                    }
                }
            } 
        }
        /// <summary>
        /// Initialize all Item instances given in the Items.txt file.
        /// </summary>
        private void InitializeItems()
        {
            // Complete path to file
            string pathFile = Path.Combine(pathFolder,"Items.txt");
            // Initialize StreamReader with specific file
            using StreamReader sr = new StreamReader(pathFile);
            // While the line that is being read is not END do
            while ((s = sr.ReadLine()) != "END")
            {
                // If the line being read isn't empty
                if (s != "")
                {
                    // If the line being read starts with # continue on
                    if (s[0] == '#') continue;
                    // If not analyze it
                    else 
                    {
                        // Split the line by the ":" char
                        string[] parameters = s.Split(":");
                        // Analyze the first part
                        switch(parameters[0])
                        {
                            case "Name": // If equals Name
                                // Set iName as second part
                                iName = parameters[1];
                                break;
                            case "Description": //If equals Description
                                // Set iDesc as second part
                                iDesc = parameters[1];
                                break;
                            case "BuffType": // If equals BuffType
                                // Set iType as second part
                                iType = Enum.Parse<BuffType>(parameters[1]);
                                break;
                            case "BuffValue": // If equals BuffValue
                                // Set iValue as second part
                                iValue = int.Parse(parameters[1]);
                                // Set instanceReady as true
                                instanceReady = true;
                                break;
                        }
                        // Is instanceReady is true
                        if (instanceReady)
                        {
                            // Add new entry to itemDict (iName, new Item instance)
                            itemDict.Add(iName,new Item(iName,iDesc,iType,iValue));
                            // Set isntanceReady as false
                            instanceReady = false;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Initialize all the Room instances give in the Rooms.txt file.
        /// </summary>
        private void InitializeRooms()
        {
            // Complete path to file
            string pathFile = Path.Combine(pathFolder,"Rooms.txt");
            // Initialize StreamReader with specific file
            using StreamReader sr = new StreamReader(pathFile);
            // While the line that is being read is not END do
            while ((s = sr.ReadLine()) != "END")
            {
                // If the line being read isn't empty
                if (s != "")
                {
                    // If the line being read starts with # continue on
                    if (s[0] == '#') continue;
                    // If not analyze it
                    else 
                    {
                        // Split the line by the ":" char
                        string[] parameters = s.Split(":");
                        // Analyze the first part
                        switch(parameters[0])
                        {
                            case "Name": // If equals Name
                                // Set rName as second part
                                rName = parameters[1];
                                break;
                            case "Description": // If equals Description
                                // Set rDescription as second part
                                rDesc = parameters[1];
                                break;
                            case "North": // If equals North
                                // Set rNorth as second part
                                rNorth = NullOrValue(parameters[1],rNorth);
                                break;
                            case "South": // If equals South
                                // Set rSouth as second part
                                rSouth = NullOrValue(parameters[1],rSouth);
                                break;
                            case "East": // If equals East
                                // Set rEast as second part
                                rEast = NullOrValue(parameters[1],rEast);
                                break;
                            case "West": // If equals West
                                // Set rWest as second part
                                rWest = NullOrValue(parameters[1],rWest);
                                break;
                            case "Enemy": // If equals Enemy
                                // Set rEnemy as second part
                                rEnemy = NullOrValue(parameters[1],rEnemy);
                                break;
                            case "Item": // If equals Item
                                // Set rItem has second part
                                rItem = NullOrValue(parameters[1],rItem);
                                // Set instanceReady as true
                                instanceReady = true;
                                break;
                        }
                        // If instaceReady is true
                        if (instanceReady)
                        {
                            // Create new string array wit all four direcion variables
                            roomDirections = new string[4] { rNorth, rSouth, rEast, rWest };
                            // Add the roomDirections array to a list
                            roomDirectionsList.Add(roomDirections);
                            // Add new entry to roomDict (rNmae, new Room instance)
                            roomDict.Add(rName,new Room(rDesc,rEnemy,rItem));
                            // Set instanceReady to false
                            instanceReady = false;
                        }
                    }
                }
            }
            SetUpRoomDirections();
        }
        /// <summary>
        /// Find if a specified varaible should be null or have a value.
        /// If param is "-" the variable is null, if else the variable is equals to param.
        /// </summary>
        /// <param name="param">Parameter to be evaluated.</param>
        /// <param name="variable">Varable to be set.</param>
        /// <returns>Value to be set.</returns>
        private string NullOrValue(string param,string variable)
        {
            // Check if param not equals to "-"
            if (param != "-")
                // If true set variable as param
                variable = param;
            else
                // If false set variable as null
                variable = null;
            // Return variable
            return variable;
        }
        /// <summary>
        /// Find if a specified varaible should be null or have a value.
        /// If param is "-" the variable is null, if else the variable is equals to param.
        /// </summary>
        /// <param name="param">Parameter to be evaluated.</param>
        /// <param name="variable">Varable to be set.</param>
        /// <returns>Value to be set.</returns>
        private Enemy NullOrValue(string param,Enemy variable)
        {
            // Check if param not equals to "-"
            if (param != "-")
                // If true set variable as param
                variable = enemyDict[param];
            else
                // If false set variable as null
                variable = null;
            // Return variable
            return variable;
        }
        /// <summary>
        /// Find if a specified varaible should be null or have a value.
        /// If param is "-" the variable is null, if else the variable is equals to param.
        /// </summary>
        /// <param name="param">Parameter to be evaluated.</param>
        /// <param name="variable">Varable to be set.</param>
        /// <returns>Value to be set.</returns>
        private Item NullOrValue(string param,Item variable)
        {
            // Check if param not equals to "-"
            if (param != "-")
                // If true set variable as param
                variable = itemDict[param];
            else
                // If false set variable as null
                variable = null;
            // Return variable
            return variable;
        }
        /// <summary>
        /// Set up the Rooms isntances directions.
        /// </summary>
        private void SetUpRoomDirections()
        {
            // Start idx at 0
            int idx = 0;
            // Go throught every entry in roomDict
            foreach (KeyValuePair<string,Room> kvp in roomDict)
            {
                // Get the correct roomDirection array
                string[] roomDir = roomDirectionsList[idx];
                // Set all cardinal directions
                if (roomDir[0] != null)
                    kvp.Value.AddRoom("north",roomDict[roomDir[0]]);
                if (roomDir[1] != null)
                    kvp.Value.AddRoom("south",roomDict[roomDir[1]]);
                if (roomDir[2] != null)
                    kvp.Value.AddRoom("east",roomDict[roomDir[2]]);
                if (roomDir[3] != null)
                    kvp.Value.AddRoom("west",roomDict[roomDir[3]]);
                // Increment idx by 1
                idx++;
            } 
        }
    }
}