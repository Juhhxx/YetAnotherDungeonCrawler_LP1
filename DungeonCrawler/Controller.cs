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


        public void Start()
        {
            // hello message + instructions

            while (true)
            {
                view.RoomDescription(player.InRoom);


                string input = view.AwaitDecision();
                string[] inputArray = input.Trim().ToLower().Split(' ');

                switch (input[0])
                {
                    case "go":
                        if ( player.Move(input[1]) ) continue;
                    case "pickup":
                    case "attack":
                    case "exit":
                        view.ByeBye();
                        break;
                    default:
                        view.
                        continue;
                }

            }
        }
    }
}