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

        string s;

        string eName;
        int eHP;
        int eAtck;
        int eDef;

        string iName;
        BuffType iType;
        int iValue;

        string rName;
        string rDesc;
        Enemy rEnemy;
        Item rItem;
        string rNorth;
        string rSouth;
        string rEast;
        string rWest;

        bool instanceReady = false;
        
        public void InitializeEnemies()
        {
            using StreamReader sr = new StreamReader("Enemies.txt");

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
                            Console.WriteLine("Enemy Created");
                            enemyDict.Add(eName,new Enemy(eName,eHP,eAtck,eDef));
                            instanceReady = false;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string,Enemy> kvp in enemyDict)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value.HP}, {kvp.Value.AttackPower}, {kvp.Value.Defense}");
            } 
        }
        public void InitializeItems()
        {
            using StreamReader sr = new StreamReader("Items.txt");

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
                            Console.WriteLine("Item created");
                            itemDict.Add(iName,new Item(iName,iType,iValue));
                            instanceReady = false;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string,Item> kvp in itemDict)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value.Type.ToString()}, {kvp.Value.BuffValue}");
            } 
        }
        public void InitializeRooms()
        {
            using StreamReader sr = new StreamReader("Rooms.txt");

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
                            // case "North":
                            //     if (parameters[1] != "!")
                            //         rNorth = parameters[1];
                            //     break;
                            // case "South":
                            //     if (parameters[1] != "!")
                            //         rSouth = parameters[1];
                            //     break;
                            // case "East":
                            //     if (parameters[1] != "!")
                            //         rEast = parameters[1];
                            //     break;
                            // case "West":
                            //     if (parameters[1] != "!")
                            //         rWest = parameters[1];
                            //     break;
                            case "Enemy":
                                if (parameters[1] != "-")
                                    rEnemy = enemyDict[parameters[1]];
                                break;
                            case "Item":
                                if (parameters[1] != "-")
                                    rItem = itemDict[parameters[1]];
                                instanceReady = true;
                                break;
                        }
                        if (instanceReady)
                        {
                            Console.WriteLine("Room created");
                            roomDict.Add(rName,new Room(rDesc,rEnemy,rItem));
                            instanceReady = false;
                        }
                    }
                }
            }
            foreach (KeyValuePair<string,Room> kvp in roomDict)
            {
                Console.WriteLine($"{kvp.Key}\n{kvp.Value.Description}\nEnemy: {kvp.Value.REnemy.Name}  {kvp.Value.REnemy.HP} {kvp.Value.REnemy.AttackPower}\nItem: {kvp.Value.RItem.Name} {kvp.Value.RItem.Type.ToString()} {kvp.Value.RItem.BuffValue}");
            } 
        }
    }
}