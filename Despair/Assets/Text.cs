using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// class to store static and to generate dynamic text for the message and input boxes
    /// </summary>
    public static class Text
    {
        public static List<string> HeaderText = new List<string>() { "Despair" };
        public static List<string> FooterText = new List<string>() { "RiP Studios | Copyright 2018" };

        #region INTITIAL GAME SETUP

        public static string Setup()
        {
            string messageBoxText =
                "--Please turn up your game volume loud enough to hear the background music--\n" +
                " \n" +
                "Select a game difficulty from the options below.";

            return messageBoxText;
        }

        public static string Introduction()
        {
            string messageBoxText =
            "August 8th, 1978: \n" +
            " \n" +
            "In six days, it will mark the 25th anniversary of the Eastshore elementary school closing. " +
            "Eastshore--located on the outskirts of town--is now isolated since it was closed. All roads nearby it " +
            "have been permanently blocked off. The once well-maintained trees, shrubbery, and plants surrounding the school " +
            "are now overgrown and allow nobody from the outside world to see past them.\n" +
            " \n" +
            "You are a correspondent for a local news company and have a cruel, money-craving boss. " +
            "Several stories around town suggest some super-natural force has a presence inside Eastshore. " +
            "Some believe it was the reason that the school closed. Your boss has tasked you with investigating " +
            "these claims in spite of the upcoming 25th anniversary of the school closing.\n" +
            " \n" +
             "\tPress any key to continue...\n";

            return messageBoxText;
        }

        public static string Rules()
        {
            string messageBoxText =
                "Despair Rules:\n" +
                " \n" +
                "1) When entering certain rooms, you will need to open a door and listen for a specific sound. \n" +
                "\tIf you hear footsteps, close the door right away, and the room will reset. If you don't hear footsteps, \n" +
                "\tyou can safely enter the room. \n" +
                " \n" +
                "2) IMPORTANT: Be careful at each door! He only knows when a door is open, \n" +
                "\tso if you wait too long with the door open, he will find you.\n" +
                " \n" +
                "3) The enemy will never spawn in certain locations, so you will immediately enter \n" +
                "\tthose rooms without worrying about the door. \n" +
                " \n" +
                "4) Collect as many pieces of evidence for your investigation as possible...\n";
            return messageBoxText;
        }

        public static string IntroLocationInfo()
        {
            string messageBoxText =
            "August 11th, 1978. 11:14 PM.\n" +
            " \n" +
            "You are outside of Eastshore elementary, alone. Seeing your old school, abandoned... " +
            "empty, feels strange. It is raining outside. You approach the front entrance you used to walk through every day " +
            "as a child. This time it feels different.\n" +
            " \n" +
            "\tChoose from the menu options to proceed.\n";

            return messageBoxText;
        }

        #region Initialize Mission Text

        public static string BeginningInformation()
        {
            string messageBoxText =
                "Here is what knowledge you already have about Eastshore: \n" +
                " \n" +
                "\t - The school was closed due to 'financial reasons', according to the school's board. \n" +
                "\t - While Eastshore was open, sudden, horrific events plagued the school. \n" +
                "\t - You attended this school many years ago...\n" +
                " \n" +
                "Before you can begin, you must provide some required information.\n" +
                "You will be prompted with several questions pertaining to your character in the game.\n" +
                " \n" +
                "\tPress any key to begin.";

            return messageBoxText;
        }

        public static string InitializePlayerGetName()
        {
            string messageBoxText =
                "Enter your name.\n" +
                " \n" +
                "Please use the name you wish to be referred to as in the game.";

            return messageBoxText;
        }

        public static string InitializePlayerGetAge(string name)
        {
            string messageBoxText =
                "Enter your age below.\n" +
                " \n" +
                "Please provide your age as a whole number.";

            return messageBoxText;
        }

        public static string InitializePlayerGetGender(Player gamePlayer)
        {
            string messageBoxText =
                $"{gamePlayer.Name}, it is important that your gender is known.\n" +
                " \n" +
                "Enter your gender below.\n" +
                " \n";

            string genderList = null;

            foreach (Character.Gender race in Enum.GetValues(typeof(Character.Gender)))
            {
                if (race != Character.Gender.None)
                {
                    genderList += $"\t{race}\n";
                }
            }

            messageBoxText += genderList;

            return messageBoxText;
        }

        public static string InitializePlayerGetPlayerGrade(Player gamePlayer)
        {
            string messageBoxText =
                $"{gamePlayer.Name}, we must know what grade you were in when you attended Eastshore.\n" +
                " \n" +
                "Enter which grade you attened Eastshore\n" +
                " \n";

            string gradeList = null;

            foreach (Player.Grade grade in Enum.GetValues(typeof(Player.Grade)))
            {
                gradeList += $"\t{grade}\n";
            }

            messageBoxText += gradeList;

            return messageBoxText;
        }

        public static string InitializePlayerEchoInfo(Player gamePlayer)
        {
            string messageBoxText =
                $"Here is the information we have collected:\n" +
                " \n" +
                $"\tPlayer Name: {gamePlayer.Name}\n" +
                $"\tPlayer Age: {gamePlayer.Age}\n" +
                $"\tPlayer Gender: {gamePlayer.PlayerGender}\n" +
                $"\tPlayer Grade: {gamePlayer.PlayerGrade}\n" +
                " \n" +
                "Press any key to continue.";

            return messageBoxText;
        }

        public static string EditPlayerName()
        {
            string messageBoxText =
                "Enter 'EDIT' to change the Player's Name.\n" +
                " \n";

            return messageBoxText;
        }

        public static string EditPlayerAge()
        {
            string messageBoxText =
                "Enter 'EDIT' to change the Player's Age.\n" +
                " \n";

            return messageBoxText;
        }

        public static string EditPlayerGender()
        {
            string messageBoxText =
                "Enter 'EDIT' to change the Player's Gender to one of the options below:\n" +
                " \n";

            string genderList = null;

            foreach (Character.Gender race in Enum.GetValues(typeof(Character.Gender)))
            {
                if (race != Character.Gender.None)
                {
                    genderList += $"\t{race}\n";
                }
            }

            messageBoxText += genderList;

            return messageBoxText;
        }

        public static string EditPlayerGrade()
        {
            string messageBoxText =
                "Enter 'EDIT' to change the grade in which the Player attended Eastshore to one of the options below: \n" +
                " \n";

            string gradeList = null;

            foreach (Player.Grade grade in Enum.GetValues(typeof(Player.Grade)))
            {
                gradeList += $"\t{grade}\n";
            }

            messageBoxText += gradeList;

            return messageBoxText;
        }

        public static string EditPlayerEchoInfo(Player gamePlayer)
        {
            string messageBoxText =
                $"Here is the information about the current player:\n" +
                " \n" +
                $"\tPlayer Name: {gamePlayer.Name}\n" +
                $"\tPlayer Age: {gamePlayer.Age}\n" +
                $"\tPlayer Gender: {gamePlayer.PlayerGender}\n" +
                $"\tPlayer Grade: {gamePlayer.PlayerGrade}\n";

            return messageBoxText;
        }

        public static string PlayerEdit()
        {
            string messageBoxText = "";

            messageBoxText =
                "You can edit the player you have created here. " +
                "Each of your player's characteristics will be displayed \n" +
                "one at a time. \n" +
                " \n" +
                "\t-If you wish to change the value, type in EDIT, hit ENTER, and input the desired information. \n" +
                " \n" +
                "\t-If you do NOT want to change the value, press the ENTER key to skip. \n" +
                " \n" +
                "\tPress any key to begin.\n";

            return messageBoxText;
        }

        #endregion

        #endregion

        #region Object Text

        /// <summary>
        /// Display Player's 'Known' Game Objects
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <returns></returns>
        public static string ListKnownGameObjects(IEnumerable<GameObject> gameObjects, Player player)
        {
            //
            // Table name and headers
            //
            string messageBoxText =
                "Game Objects the Player Knows of:\n" +
                " \n" +
                "ID".PadRight(10) +
                "Name".PadRight(30) + "\n" + "\n" +
                "---".PadRight(10) +
                "-------------------".PadRight(30) + "\n";
            //
            // Display all player objects
            //
            string gameObjectsRows = null;
            foreach (GameObject g in gameObjects)
            {
                //Treat GameObject as a PlayerObject to test its Type
                PlayerObject p = (PlayerObject)g;

                if (p.Type == PlayerObjectType.GameItem || player.Inventory.Contains(p))
                {
                    gameObjectsRows +=
                        $"{g.Id}".PadRight(10) +
                        $"{g.Name}".PadRight(30) +
                        Environment.NewLine;
                }
            }

            messageBoxText += gameObjectsRows;

            return messageBoxText;
        }

        /// <summary>
        /// Display the Name and ID of the Game Objects
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <returns></returns>
        public static string GameObjectsChoiceList(IEnumerable<GameObject> gameObjects)
        {
            //
            // Table and Header
            //
            string messageBoxText =
                "Game Objects\n" +
                " \n" +
                "Id".PadRight(10) +
                "Name".PadRight(30) + "\n" +
                "---".PadRight(10) +
                "-------------------".PadRight(30) + " \n";

            //
            // Display available objects
            //
            string gameObjectRows = null;

            foreach (GameObject g in gameObjects)
            {
                gameObjectRows +=
                    $"{g.Id}".PadRight(10) +
                    $"{g.Name}".PadRight(30) +
                    Environment.NewLine;
            }

            messageBoxText += gameObjectRows;
            return messageBoxText;
        }

        /// <summary>
        /// Only Display the Name of the Game Objects
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <returns></returns>
        public static string GameObjectsViewList(IEnumerable<GameObject> gameObjects)
        {
            //
            // Table and Header
            //
            string messageBoxText = "";

            if (gameObjects.Count() > 0)
            {
                messageBoxText =
                    "Game Objects\n" +
                    " \n" +
                    "".PadRight(5) +
                    "Name" + "\n" +
                    "".PadRight(5) +
                    "-----------------" + " \n";

                //
                // Display available objects
                //
                string gameObjectRows = null;

                foreach (GameObject g in gameObjects)
                {
                    gameObjectRows +=
                        "".PadRight(5) +
                        $"{g.Name}" +
                        Environment.NewLine;
                }

                messageBoxText += gameObjectRows;
            }
            else
            {
                messageBoxText = " \nThere are no game objects here.\n";
            }

            return messageBoxText;
        }
        #endregion

        #region Location Text

        public static string CurrentLocationInfo(Location location)
        {
            string messageBoxText =
                $"Current Location: {location.Name}\n" +
                " \n" +
                location.Description;
            return messageBoxText;
        }

        public static string ListLocations(IEnumerable<Location> locations)
        {
            string messageBoxText =
                "Locations:\n" +
                " \n" +

                //
                // Display a line as the header
                //
                "ID".PadRight(10) + "Name".PadRight(30) + "\n" +
                "---".PadRight(10) + "----------------------".PadRight(30) + "\n";

            //
            // Display all locations
            //
            string locationList = null;

            foreach (Location location in locations)
            {
                if(location.LocationID != 12 && location.LocationID != 11)
                {
                    locationList +=
                        $"{location.LocationID}".PadRight(10) +
                        $"{location.Name}".PadRight(30) +
                        Environment.NewLine;
                }
            }

            messageBoxText += locationList;
            return messageBoxText;
        }

        public static string VisitedLocations(IEnumerable<Location> Locations)
        {
            string messageBoxText =
                "Locations Visited\n" +
                " \n" +

                //Table Header
                "ID".PadRight(10) + "Name".PadRight(30) + "\n" +
                "---".PadRight(10) + "----------------------".PadRight(30) + "\n";

            //Display all Visited Locations

            string LocationList = null;
            foreach (Location l in Locations)
            {
                LocationList +=
                    $"{l.LocationID}".PadRight(10) +
                    $"{l.Name}".PadRight(30) +
                    Environment.NewLine;
            }

            messageBoxText += LocationList;
            return messageBoxText;
        }

        #endregion

        #region NPC Text

        /// <summary>
        /// List All Known NPCs
        /// </summary>
        /// <param name="npcs"></param>
        /// <param name="knownNpcs"></param>
        /// <returns></returns>
        public static string ListKnownNpcObjects(IEnumerable<Npc> npcs, List<Npc> knownNpcs)
        {
            //Table
            string messageBoxText =
                "Nonplayer Characters\n" +
                " \n" +

                //Table Header
                "Id".PadRight(10) +
                "Name".PadRight(30) + "\n" +
                "---".PadRight(10) +
                "-----------------------".PadRight(30) + "\n";

            //All known npcs
            string npcObjectRows = null;
            foreach (Npc npc in npcs)
            {
                if (knownNpcs.Contains(npc))
                {
                    npcObjectRows +=
                        $"{npc.Id}".PadRight(10) +
                        $"{npc.Name}".PadRight(10) + Environment.NewLine;
                }
            }

            messageBoxText += npcObjectRows;
            return messageBoxText;
        }

        /// <summary>
        /// Display all NPCs in a location (Search Screen)
        /// </summary>
        /// <param name="npcs"></param>
        /// <returns></returns>
        public static string NpcChooseList(IEnumerable<Npc> npcs)
        {
            //Table
            string messageBoxText = "";

            if (npcs.Count() > 0)
            {
                messageBoxText =
                    " \n" +
                    "Nonplayer Characters\n" +
                    " \n" +
                    "".PadRight(5) +

                    //Table Header
                    "Name".PadRight(30) + "\n" +
                    "".PadRight(5) +
                    "-----------------------".PadRight(30) + "\n";

                //All npcs
                string npcObjectRows = null;
                foreach (Npc npc in npcs)
                {
                    npcObjectRows +=
                        "".PadRight(5) +
                        $"{npc.Name}".PadRight(10) + Environment.NewLine;
                }

                messageBoxText += npcObjectRows;
            }
            else
            {
                messageBoxText = " \nThere are no NPCs here.";
            }
            return messageBoxText;
        }

        /// <summary>
        /// Display all NPCs you can interact with at the location
        /// </summary>
        /// <param name="npcs"></param>
        /// <returns></returns>
        public static string NpcInteractWithList(IEnumerable<Npc> npcs)
        {
            //Table
            string messageBoxText = "";

            if (npcs.Count() > 0)
            {
                messageBoxText =
                    "Nonplayer Characters\n" +
                    " \n" +

                    //Table Header
                    "Id".PadRight(10) +
                    "Name".PadRight(30) + "\n" +
                    "---".PadRight(10) +
                    "-----------------------".PadRight(30) + "\n";

                //All npcs
                string npcObjectRows = null;
                foreach (Npc npc in npcs)
                {
                    npcObjectRows +=
                        $"{npc.Id}".PadRight(10) +
                        $"{npc.Name}".PadRight(10) + Environment.NewLine;
                }

                messageBoxText += npcObjectRows;
            }
            else
            {
                messageBoxText = " \n --There are no NPCs here.";
            }
            return messageBoxText;
        }
        #endregion

        #region MAIN MENU ACTION SCREENS

        public static string PlayerInfo(Player gamePlayer)
        {
            string messageBoxText =
                $"\tPlayer Name: {gamePlayer.Name}\n" +
                $"\tPlayer Age: {gamePlayer.Age}\n" +
                $"\tPlayer Gender: {gamePlayer.PlayerGender}\n" +
                $"\tPlayer Grade: {gamePlayer.PlayerGrade}\n" +
                " \n";

            return messageBoxText;
        }
        public static string Travel(Player gamePlayer, List<Location> Locations)
        {
            string messageBoxText =
                "Enter the ID number of the desired location.\n" +
                " \n" +

                //
                //Table Header
                //
                "ID".PadRight(10) + "Name".PadRight(30) + "\n" +
                "---".PadRight(10) + "----------------------".PadRight(30) + "\n";
            //
            // Display all accessible locations
            //
            string LocationList = null;
            foreach(Location location in Locations)
            {
                if((location.LocationID != gamePlayer.LocationID) && location.Accessable == true)
                {
                    LocationList +=
                        $"{location.LocationID}".PadRight(10) +
                        $"{location.Name}".PadRight(30) +
                        Environment.NewLine;
                }
            }

            messageBoxText += LocationList;
            return messageBoxText;
        }


        public static string SearchRoom(Location location)
        {
            string messageBoxText =
                $"Current Location: {location.Name}\n" +
                " \n" +
                location.GeneralContents;

            return messageBoxText;
        }

        public static string LookAt(GameObject gameObject, IEnumerable<PlayerObject> inventory)
        {
            string messageBoxText = "";

            messageBoxText =
                $"{gameObject.Name}\n" +
                " \n" +
                $"Description: {gameObject.Description}\n" +
                " \n";

            //Check if the game object is a player object
            if (gameObject is PlayerObject)
            {
                PlayerObject playerObject = gameObject as PlayerObject;

                //Check if object has content to display
                if (playerObject.Content != "")
                {
                    //Display the content
                    messageBoxText += "Content: \n";
                    messageBoxText += $"\n{playerObject.Content} \n";
                    messageBoxText += " \n";
                }

                //Check if the player can add the object to the inventory
                if ((playerObject.CanInventory && !inventory.Contains(playerObject)) || playerObject.Name == "Batteries")
                {
                    messageBoxText += "\t--This object has been added to your inventory--";
                }
                else if (playerObject.CanInventory && inventory.Contains(playerObject))
                {
                    messageBoxText += "\t--This object is already in your inventory--";
                }
                else
                {
                    messageBoxText += "\t--This object is view-only // It can not be added to your inventory--";
                }
            }
            else
            {
                messageBoxText += "\t--This object is view-only // It can not be added to your inventory--";
            }

            return messageBoxText;
        }

        public static string CurrentInventory(IEnumerable<PlayerObject> inventory)
        {
            string messageBoxText = "";

            //
            // Table Header
            //
            messageBoxText =
                "Id".PadRight(10) +
                "Name".PadRight(20) +
                "Type".PadRight(10) +
                " \n" +
                "---".PadRight(10) +
                "------------------".PadRight(20) +
                "----------" +
                " \n";

            //
            //Display all player objects
            //
            string inventoryObjectRows = null;
            foreach (PlayerObject inventoryObject in inventory)
            {
                inventoryObjectRows +=
                    $"{inventoryObject.Id}".PadRight(10) +
                    $"{inventoryObject.Name}".PadRight(20) +
                    $"{inventoryObject.Type}".PadRight(10) +
                    Environment.NewLine;
            }

            messageBoxText += inventoryObjectRows;
            return messageBoxText;
        }

        public static string ExitScreen()
        {
            string messageBoxText =
                "Press the ENTER key to exit.";
            return messageBoxText;
        }

        public static string GameOver()
        {
            string messageBoxText =
                "Game Over... He found you.\n" +
                " \n" +
                "\tPress the ENTER key to exit.";
            return messageBoxText;
        }

        public static string GameWon(Player p)
        {
            string messageBoxText =
                "Congratulations! You found all of the evidence and managed to escape Eastshore successfully! \n" +
                "Thanks to your hard work, you were able to convince the city to demolish Eastshore, \n" +
                "releasing the spirits that haunted the building to finally be at rest.\n" +
                " \n" +
                "Game Stats: \n" +
                " \n" +
                $"\t Player Level: {p.ExperiencePoints / 10}\n" +
                $"\t Number of Locations Visited: {p.LocationsVisited.Count()}\n" +
                $"\t Number of Objects in Inventory: {p.Inventory.Count()}\n" +
                $"\t Number of NPCs Interacted With: {p.NpcsInteractedWith.Count()}\n" +
                " \n" +
                "Press ENTER to exit the game...";

            return messageBoxText;
        }

        #endregion

        #region StatusBox Text

        public static List<string> StatusBox(Player player, Universe universe)
        {
            List<string> statusBoxText = new List<string>();

            //Level
            int level = player.ExperiencePoints / 10;

            statusBoxText.Add($"Level {level} \n".PadLeft(5));

            return statusBoxText;
        }

        public static List<string> StatusBoxBar(Player player, Universe universe)
        {
            List<string> statusBoxText = new List<string>();

            //XP -- Between Levels
            int xp = player.ExperiencePoints % 10;

            //Variable to hold XP Bar
            string xpBoxTop;
            string xpBoxBottom;

            switch (xp)
            {
                case 0:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|__________|";
                    break;
                case 1:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|/_________|";
                    break;
                case 2:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|//________|";
                    break;
                case 3:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|///_______|";
                    break;
                case 4:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|////______|";
                    break;
                case 5:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|/////_____|";
                    break;
                case 6:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|//////____|";
                    break;
                case 7:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|///////___|";
                    break;
                case 8:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|////////__|";
                    break;
                case 9:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|/////////_|";
                    break;
                default:
                    xpBoxTop = " __________ ";
                    xpBoxBottom = "|__________|";
                    break;
            }

            statusBoxText.Add($"    {xpBoxTop} \n".PadLeft(5));
            statusBoxText.Add($"XP: {xpBoxBottom} \n".PadLeft(5));

            return statusBoxText;
        }

        public static List<string> FlashlightStatus(Player player)
        {
            List<string> statusBoxText = new List<string>();

            statusBoxText.Add($"Flashlight Battery: {player.FlashlightBattery}% \n".PadLeft(8));
            statusBoxText.Add($"Batteries in Inventory: {player.Batteries} \n".PadLeft(8));

            return statusBoxText;
        }

        #endregion

        #region Room Screens

        //School Lobby

        public static string SchoolLobby()
        {
            string messageBoxText =
                "\"My old elementary school... I never thought I'd be here again\".\n" +
                " \n" +
                "The Lobby is dimly lit, providing enough light to see the objects in the room.\n" +
                " \n" +
                "\tPress any key to continue.";
            return messageBoxText;
        }
        public static string SchoolLobbyPT2()
        {
            string messageBoxText =
                "\"What was that?" +
                " \n" +
                "I think the door behind me just locked. That's not normal...\"\n" +
                " \n" +
                "You are now in Eastshore. Find a source of light to navigate through the school. \n" +
                " \n" +
                "\tPress any key to continue.";
            return messageBoxText;
        }

        //Hallway

        public static string Hallway()
        {
            string messageBoxText =
                "\"It looks like one of the windows is cracked open.\"\n" +
                " \n" +
                "At the end of the hallway is a locked door that leads to the cafeteria. Find the key.\n" +
                " \n" +
                "\tPress any key to continue.";
            return messageBoxText;
        }

        //Door
        public static string Door()
        {
            string messageBoxText =
                "\tPress any key to open the door.";
            return messageBoxText;
        }

        public static string DoorPT2()
        {
            string messageBoxText =
                "\t--The Door is Open--";
            return messageBoxText;
        }

        public static string Empty()
        {
            string messageBoxText =
                " ";
            return messageBoxText;
        }
        #endregion

    }
}
