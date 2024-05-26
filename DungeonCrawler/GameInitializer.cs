using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DungeonCrawler
{
    public class GameInitializer
    {
        Dictionary<string,Enemy> enemyDict = new Dictionary<string, Enemy>();
        Dictionary<string,Item> itemDict = new Dictionary<string, Item>();
        Dictionary<string,Room> roomDict = new Dictionary<string, Room>();
        // String used to analyze the files
        string s;
        // Enemy variables
        private string eName;
        private int eHP;
        private int eAtck;
        private int eDef;
        // Item variables
        private string iName;
        private BuffType iType;
        private int iValue;
        // Room variables
        private string rName;
        private string rDesc;
        private Enemy rEnemy;
        private Item rItem;
        private List<string[]> roomDirectionsList = new List<string[]>();
        private string[] roomDirections;
        private string rNorth;
        private string rSouth;
        private string rEast;
        private string rWest;
        // Bool value to know when an instance ir ready
        private bool instanceReady = false;
        // Initial path for GameFiles
        private string pathFolder = Path.Combine("DungeonCrawler","GameFiles");
        private Player player;
        public GameInitializer(Player player)
        {
            this.player = player;
        }
        public void InitializeGame()
        {
            InitializeEnemies();
            InitializeItems();
            InitializeRooms();
            player.SetInitialRoom(roomDict["Room1"]);
        }
        private void InitializeEnemies()
        {
            string pathFile = Path.Combine(pathFolder,"Enemies.txt");
            using StreamReader sr = new StreamReader(pathFile);

            while ((s = sr.ReadLine()) != "END")
            {
                if (s != "")
                {
                    if (s[0] == '#') continue;
                    else 
                    {
                        string[] parameters = s.Split(":");

                        switch(parameters[0])
                        {
                            case "Name":
                                eName = parameters[1];
                                break;
                            case "HP":
                                eHP = int.Parse(parameters[1]);
                                break;
                            case "AttackPower":
                                eAtck = int.Parse(parameters[1]);
                                break;
                            case "Defense":
                                eDef = int.Parse(parameters[1]);
                                instanceReady = true;
                                break;
                        }
                        if (instanceReady)
                        {
                            enemyDict.Add(eName,new Enemy(eName,eHP,eAtck,eDef));
                            instanceReady = false;
                        }
                    }
                }
            }
            // foreach (KeyValuePair<string,Enemy> kvp in enemyDict)
            // {
            //     Console.WriteLine($"{kvp.Key} : {kvp.Value.HP}, {kvp.Value.AttackPower}, {kvp.Value.Defense}");
            // } 
        }
        private void InitializeItems()
        {
            string pathFile = Path.Combine(pathFolder,"Items.txt");
            using StreamReader sr = new StreamReader(pathFile);

            while ((s = sr.ReadLine()) != "END")
            {
                if (s != "")
                {
                    if (s[0] == '#') continue;
                    else 
                    {
                        string[] parameters = s.Split(":");

                        switch(parameters[0])
                        {
                            case "Name":
                                iName = parameters[1];
                                break;
                            case "BuffType":
                                iType = Enum.Parse<BuffType>(parameters[1]);
                                break;
                            case "BuffValue":
                                iValue = int.Parse(parameters[1]);
                                instanceReady = true;
                                break;
                        }
                        if (instanceReady)
                        {
                            itemDict.Add(iName,new Item(iName,iType,iValue));
                            instanceReady = false;
                        }
                    }
                }
            }
            // foreach (KeyValuePair<string,Item> kvp in itemDict)
            // {
            //     Console.WriteLine($"{kvp.Key} : {kvp.Value.Type.ToString()}, {kvp.Value.BuffValue}");
            // } 
        }
        private void InitializeRooms()
        {
            string pathFile = Path.Combine(pathFolder,"Rooms.txt");
            using StreamReader sr = new StreamReader(pathFile);

            while ((s = sr.ReadLine()) != "END")
            {
                if (s != "")
                {
                    if (s[0] == '#') continue;
                    else 
                    {
                        string[] parameters = s.Split(":");

                        switch(parameters[0])
                        {
                            case "Name":
                                rName = parameters[1];
                                break;
                            case "Description":
                                rDesc = parameters[1];
                                break;
                            case "North":
                                rNorth = NullOrValue(parameters[1],rNorth);
                                break;
                            case "South":
                                rSouth = NullOrValue(parameters[1],rSouth);
                                break;
                            case "East":
                                rEast = NullOrValue(parameters[1],rEast);
                                break;
                            case "West":
                                rWest = NullOrValue(parameters[1],rWest);
                                break;
                            case "Enemy":
                                rEnemy = NullOrValue(parameters[1],rEnemy);
                                break;
                            case "Item":
                                rItem = NullOrValue(parameters[1],rItem);
                                instanceReady = true;
                                break;
                        }
                        if (instanceReady)
                        {
                            roomDirections = new string[4] { rNorth, rSouth, rEast, rWest };
                            roomDirectionsList.Add(roomDirections);
                            roomDict.Add(rName,new Room(rDesc,rEnemy,rItem));
                            instanceReady = false;
                        }
                    }
                }
            }
            SetUpRoomDirections();
            // foreach (KeyValuePair<string,Room> kvp in roomDict)
            // {
            //     // Console.WriteLine($"{kvp.Key}\n{kvp.Value.Description}\n{kvp.Value.accessRooms["north"].Name}\n{kvp.Value.accessRooms["south"].Name}\n{kvp.Value.accessRooms["east"].Name}\n{kvp.Value.accessRooms["west"].Name}\nEnemy: {kvp.Value.REnemy.Name}  {kvp.Value.REnemy.HP} {kvp.Value.REnemy.AttackPower}\nItem: {kvp.Value.RItem.Name} {kvp.Value.RItem.Type.ToString()} {kvp.Value.RItem.BuffValue}");
            //     Console.WriteLine($"{kvp.Key}\nNorth:{kvp.Value.accessRooms["north"]}\nSouth:{kvp.Value.accessRooms["south"]}\nEast:{kvp.Value.accessRooms["east"]}\nWest:{kvp.Value.accessRooms["west"]}");
            // } 
        }
        private string NullOrValue(string param,string variable)
        {
            if (param != "-")
                variable = param;
            else
                variable = null;

            return variable;
        }
        private Enemy NullOrValue(string param,Enemy variable)
        {
            if (param != "-")
                variable = enemyDict[param];
            else
                variable = null;
                
            return variable;
        }
        private Item NullOrValue(string param,Item variable)
        {
            if (param != "-")
                variable = itemDict[param];
            else
                variable = null;
                
            return variable;
        }
        private void SetUpRoomDirections()
        {
            int idx = 0;

            foreach (KeyValuePair<string,Room> kvp in roomDict)
            {
                string[] roomDir = roomDirectionsList[idx];

                if (roomDir[0] != null)
                    kvp.Value.AddRoom("north",roomDict[roomDir[0]]);
                if (roomDir[1] != null)
                    kvp.Value.AddRoom("south",roomDict[roomDir[1]]);
                if (roomDir[2] != null)
                    kvp.Value.AddRoom("east",roomDict[roomDir[2]]);
                if (roomDir[3] != null)
                    kvp.Value.AddRoom("west",roomDict[roomDir[3]]);

                idx++;
            } 
        }
    }
}