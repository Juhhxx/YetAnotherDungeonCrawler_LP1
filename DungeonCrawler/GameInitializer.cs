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
        // List<Room> roomList;
        string s;

        string eName;
        int eHP;
        int eAtck;
        int eDef;

        string iName;
        BuffType iType;
        int iValue;

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
                            Console.WriteLine("item created");
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

    }
}