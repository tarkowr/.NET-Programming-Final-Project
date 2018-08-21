using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// view class
    /// </summary>
    public class ConsoleView
    {
        #region ENUMS 

        private enum ViewStatus
        {
            PlayerInitialization,
            PlayingGame
        }

        #endregion

        #region FIELDS  

        //
        // declare game objects for the ConsoleView object to use
        //
        Player _gamePlayer;
        Universe _gameUniverse;

        ViewStatus _viewStatus;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Player gamePlayer, Universe gameUniverse)
        {
            _gamePlayer = gamePlayer;
            _gameUniverse = gameUniverse;

            _viewStatus = ViewStatus.PlayerInitialization;

            InitializeDisplay();
        }

        #endregion

        #region METHODS

        #region Main Methods / Helper Methods

        /// <summary>
        /// display all of the elements on the game play screen on the console
        /// </summary>
        /// <param name="messageBoxHeaderText">message box header title</param>
        /// <param name="messageBoxText">message box text</param>
        /// <param name="menu">menu to use</param>
        /// <param name="inputBoxPrompt">input box text</param>
        public void DisplayGamePlayScreen(string messageBoxHeaderText, string messageBoxText, Menu menu, string inputBoxPrompt)
        {
            //
            // reset screen to default window colors
            //
            Console.BackgroundColor = ConsoleTheme.WindowBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.WindowForegroundColor;
            Console.Clear();

            ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
            ConsoleWindowHelper.DisplayFooter(Text.FooterText);

            DisplayMessageBox(messageBoxHeaderText, messageBoxText);
            DisplayMenuBox(menu);
            DisplayInputBox();
            DisplayStatusBox();
        }

        /// <summary>
        /// wait for any keystroke to continue
        /// </summary>
        public void GetContinueKey()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// get a action menu choice from the user
        /// </summary>
        /// <returns>action menu choice</returns>
        public PlayerAction GetActionMenuChoice(Menu menu)
        {
            PlayerAction choosenAction = PlayerAction.None;
            Console.CursorVisible = false;

            //array of valid keys from menu dictionary
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            //Create a container to store user's menu choice
            char keyPressedChar;

            if (menu.MenuName != "DoorOptions")
            {
                //validate key pressed
                do
                {
                    ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
                    keyPressedChar = keyPressedInfo.KeyChar;

                } while (!validKeys.Contains(keyPressedChar));

                choosenAction = menu.MenuChoices[keyPressedChar];
                Console.CursorVisible = true;
            }
            //Menu is Door
            else
            {
                //Call method to wait 2 seconds
                ConsoleKeyInfo keyPressedInfo = WaitForKey(2000);

                //No key is pressed after two seconds
                if (keyPressedInfo.KeyChar == (char)0)
                {
                    choosenAction = PlayerAction.Caught;
                }

                //Key is pressed before two seconds is up
                else
                {
                    //ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
                    keyPressedChar = keyPressedInfo.KeyChar;

                    //validate key pressed
                    if (validKeys.Contains(keyPressedChar))
                    {
                        choosenAction = menu.MenuChoices[keyPressedChar];
                        Console.CursorVisible = true;
                    }

                }
            }

            return choosenAction;

        }

        /// <summary>
        /// Waits for a key to be pressed.
        /// If no key is pressed, it returns empty.
        /// If key is pressed, it returns keys.
        /// 
        /// Taken from: https://stackoverflow.com/questions/14385044/console-readkey-canceling 
        /// StackOverflow
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static ConsoleKeyInfo WaitForKey(int ms)
        {
            int delay = 0;
            ConsoleKeyInfo x = new ConsoleKeyInfo();

            while (delay < ms)
            {
                if (Console.KeyAvailable)
                {
                    x = Console.ReadKey();

                    //Validate Key
                    if (x.Key == ConsoleKey.E || x.Key == ConsoleKey.C)
                    {
                        return x;
                    }
                }
                System.Threading.Thread.Sleep(50);
                delay += 50;
            }
            return new ConsoleKeyInfo((char)0, (ConsoleKey)0, false, false, false);
        }

        /// <summary>
        /// Pause the Game for a specified amount of time
        /// </summary>
        /// <param name="delay"></param>
        public void Pause(int delay)
        {
            System.Threading.Thread.Sleep(delay);
        }
        #endregion

        #region Get Data Methods

        /// <summary>
        /// get a string value from the user
        /// </summary>
        /// <returns>string value</returns>
        public string GetString()
        {
            return Console.ReadLine();
        }

        public bool GetBool(string prompt, out bool choice)
        {
            choice = false;
            bool validResponse = false;
            string userResponse;

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                userResponse = Console.ReadLine();
                if (userResponse.ToLower() == "true")
                {
                    choice = true;
                    validResponse = true;
                }
                else if (userResponse.ToLower() == "false")
                {
                    validResponse = true;
                }
                else
                {
                    DisplayInputErrorMessage("You must enter either true or false. Please try again.");
                }
            }

            return choice;
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetInt(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            //
            // Validate range if either minValue or maximumValue are not 0
            //
            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (validateRange)
                    {
                        if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage($"Invalid Integer value. Please try again.");
                            DisplayInputBoxPrompt(prompt);
                        }
                    }
                    else
                    {
                        validResponse = true;
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"Invalid Integer Value.");
                    DisplayInputBoxPrompt(prompt);
                }
            }
            Console.CursorVisible = false;

            return true;
        }
        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetAge(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else if (integerChoice <= minimumValue)
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You are too young to play Despair.. You must be 13. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            return true;
        }


        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Character.Gender GetGender()
        {
            Character.Gender genderType;
            Enum.TryParse<Character.Gender>(Console.ReadLine(), out genderType);

            return genderType;
        }

        /// <summary>
        /// get a character gender value from user
        /// </summary>
        /// <returns></returns>
        public Player.Grade GetGrade()
        {
            Player.Grade grade;
            Enum.TryParse<Player.Grade>(Console.ReadLine(), out grade);

            return grade;
        }
        #endregion

        #region Initial Screen
        /// <summary>
        /// display splash screen
        /// </summary>
        /// <returns>player chooses to play</returns>
        public bool DisplaySpashScreen()
        {
            bool playing = true;
            ConsoleKeyInfo keyPressed;

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 15);
            string tabSpace = new String(' ', 37);
            Console.WriteLine(tabSpace + @"                   _____                         __                         ");
            Console.WriteLine(tabSpace + @"                  |  __  \                      |__|                        ");
            Console.WriteLine(tabSpace + @"  _____________   | |  | | ___  ____ ____  ____  __  ____    ______________ ");
            Console.WriteLine(tabSpace + @" /_____________/  | |  | |/ _ \/  __| _  \/ _  \|  |/  __|  /_____________/ ");
            Console.WriteLine(tabSpace + @"                  | |__| |  __|\___ ||_| | |_|  |  |  |                     ");
            Console.WriteLine(tabSpace + @"                  |_____/ \___//____| ___|___/\_|__|__|                     ");
            Console.WriteLine(tabSpace + @"                                    | |                                     ");
            Console.WriteLine(tabSpace + @"                                    |_|                                     ");

            Console.SetCursorPosition(55, 28);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Press any key to continue or Esc to exit.");
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                playing = false;
            }

            return playing;
        }

        #endregion

        #region Game Screens

        /// <summary>
        /// initialize the console window settings
        /// </summary>
        private static void InitializeDisplay()
        {
            //
            // control the console window properties
            //
            ConsoleWindowControl.DisableResize();
            ConsoleWindowControl.DisableMaximize();
            ConsoleWindowControl.DisableMinimize();
            Console.Title = "Despair";

            //
            // set the default console window values
            //
            ConsoleWindowHelper.InitializeConsoleWindow();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// display the correct menu in the menu box of the game screen
        /// </summary>
        /// <param name="menu">menu for current game state</param>
        private void DisplayMenuBox(Menu menu)
        {
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuBorderColor;

            //
            // display menu box border
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MenuBoxPositionTop,
                ConsoleLayout.MenuBoxPositionLeft,
                ConsoleLayout.MenuBoxWidth,
                ConsoleLayout.MenuBoxHeight);

            //
            // display menu box header
            //
            Console.BackgroundColor = ConsoleTheme.MenuHeaderBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuHeaderForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(menu.MenuTitle, ConsoleLayout.MenuBoxWidth - 4));

            //
            // display menu choices
            //
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            int topRow = ConsoleLayout.MenuBoxPositionTop + 3;

            foreach (KeyValuePair<char, PlayerAction> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != PlayerAction.None)
                {
                    string formatedMenuChoice = ConsoleWindowHelper.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
        }

        /// <summary>
        /// display the text in the message box of the game screen
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="messageText"></param>
        private void DisplayMessageBox(string headerText, string messageText)
        {
            //
            // display the outline for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxBorderColor;
            /*
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MessageBoxPositionTop,
                ConsoleLayout.MessageBoxPositionLeft,
                ConsoleLayout.MessageBoxWidth,
                ConsoleLayout.MessageBoxHeight);
            */
            //
            // display message box header
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBorderColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxHeaderForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(headerText, ConsoleLayout.MessageBoxWidth - 4));

            //
            // display the text for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            List<string> messageTextLines = new List<string>();
            messageTextLines = ConsoleWindowHelper.MessageBoxWordWrap(messageText, ConsoleLayout.MessageBoxWidth - 4);

            int startingRow = ConsoleLayout.MessageBoxPositionTop + 3;
            int endingRow = startingRow + messageTextLines.Count();
            int row = startingRow;
            foreach (string messageTextLine in messageTextLines)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, row);
                Console.Write(messageTextLine);
                row++;
            }

        }

        /// <summary>
        /// draw the status box on the game screen
        /// </summary>
        public void DisplayStatusBox()
        {
            Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

            //
            // display the outline for the status box
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.StatusBoxPositionTop,
                ConsoleLayout.StatusBoxPositionLeft,
                ConsoleLayout.StatusBoxWidth,
                ConsoleLayout.StatusBoxHeight);
            //
            // display status box header with title
            //
            Console.BackgroundColor = ConsoleTheme.StatusBoxHeaderBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.StatusBoxHeaderForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center("Game Information", ConsoleLayout.StatusBoxWidth - 4));

            //
            // display the text for the status box if playing game
            //
            if (_viewStatus == ViewStatus.PlayingGame)
            {

                //
                // display stats
                //
                int startingRow = ConsoleLayout.StatusBoxPositionTop + 3;
                int row = startingRow;

                //
                //Player level
                //

                foreach (string statusTextLine in Text.StatusBox(_gamePlayer, _gameUniverse))
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                    Console.Write(statusTextLine);
                    //row++;
                }

                //
                //Player XP Bar
                //

                //Change color
                Console.ForegroundColor = ConsoleColor.Red;

                // XP Bar
                foreach (string statusTextLine in Text.StatusBoxBar(_gamePlayer, _gameUniverse))
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 13, row - 1);
                    Console.Write(statusTextLine);
                    row++;
                }

                //Reset Color
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

                foreach (var l in _gameUniverse.Locations)
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 6, row);

                    if (l.LocationID == _gamePlayer.LocationID)
                    {
                        Console.Write(l.Name);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" <<");
                        Console.ForegroundColor = ConsoleColor.White;
                        row++;
                    }
                    else if (l.Accessable == true)
                    {
                        /*|| _gamePlayer.LocationsVisited.Contains(l.LocationID)*/
                        Console.Write(l.Name);
                        row++;
                    }
                }

                row++;
                //
                // Flashlight Battery
                //
                if (_gamePlayer.HasFlashlight)
                {
                    foreach (string statusTextLine in Text.FlashlightStatus(_gamePlayer))
                    {
                        Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                        Console.Write(statusTextLine);
                        row++;
                    }
                }

            }
            else
            {
                //
                // display status box header without header
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxHeaderBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxHeaderForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
            }
        }

        /// <summary>
        /// draw the input box on the game screen
        /// </summary>
        public void DisplayInputBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.InputBoxPositionTop,
                ConsoleLayout.InputBoxPositionLeft,
                ConsoleLayout.InputBoxWidth,
                ConsoleLayout.InputBoxHeight);
        }

        /// <summary>
        /// display the prompt in the input box of the game screen
        /// </summary>
        /// <param name="prompt"></param>
        public void DisplayInputBoxPrompt(string prompt)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 1);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.Write(prompt);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void DisplayInputErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(errorMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// clear the input box
        /// </summary>
        private void ClearInputBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.InputBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.InputBoxBackgroundColor;
            for (int row = 1; row < ConsoleLayout.InputBoxHeight; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + row);
                DisplayInputBoxPrompt(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
        }
        #endregion

        #region Get/Set/Edit Player Info

        /// <summary>
        /// get the player's initial information at the beginning of the game
        /// </summary>
        /// <returns>traveler object with all properties updated</returns>
        public Player GetInitialPlayerInfo()
        {
            Player player = new Player();

            //
            // intro
            //
            DisplayGamePlayScreen("Introduction", Text.BeginningInformation(), ActionMenu.Introduction, "");
            GetContinueKey();

            //
            // get traveler's name
            //
            DisplayGamePlayScreen("Player Initialization - Name", Text.InitializePlayerGetName(), ActionMenu.Introduction, "");
            DisplayInputBoxPrompt("Enter your name: ");
            player.Name = GetString();

            //
            // get traveler's age
            //
            DisplayGamePlayScreen("Player Initialization - Age", Text.InitializePlayerGetAge(player.Name), ActionMenu.Introduction, "");
            int gameTravelerAge;

            GetAge($"Enter your age, {player.Name}: ", 13, 100, out gameTravelerAge);
            player.Age = gameTravelerAge;

            //
            // get traveler's gender
            //
            DisplayGamePlayScreen("Player Initialization - Gender", Text.InitializePlayerGetGender(player), ActionMenu.Introduction, "");
            DisplayInputBoxPrompt($"Enter your gender, {player.Name}: ");
            player.PlayerGender = GetGender();

            //
            //get traveler's grade
            //
            DisplayGamePlayScreen("Player Initialization - Grade", Text.InitializePlayerGetPlayerGrade(player), ActionMenu.Introduction, "");
            DisplayInputBoxPrompt("Enter your grade: ");
            player.PlayerGrade = GetGrade();

            //
            // echo the traveler's info
            //
            DisplayGamePlayScreen("Player Initialization - Complete", Text.InitializePlayerEchoInfo(player), ActionMenu.Introduction, "");
            GetContinueKey();

            // 
            // change view status to playing game
            //
            //_viewStatus = ViewStatus.PlayingGame;

            return player;
        }

        /// <summary>
        /// Edit player information
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Player EditPlayerInfo(Player player)
        {
            string userResponse;

            DisplayGamePlayScreen("Edit Player Information", Text.PlayerEdit(), ActionMenu.EditPlayer, "");
            Console.CursorVisible = false;
            Console.ReadKey();

            //
            // edit player's name
            //
            DisplayGamePlayScreen("Player Edit - Name", Text.EditPlayerName(), ActionMenu.EditPlayer, "");
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft, ConsoleLayout.InputBoxPositionTop - 3);

            Console.WriteLine("\tCurrent Value: " + player.Name + " ".PadRight(3));
            DisplayInputBoxPrompt("");
            userResponse = Console.ReadLine();
            if (userResponse.ToUpper() == "EDIT")
            {
                DisplayInputBoxPrompt("Enter your name: ");
                player.Name = GetString();
            }

            //
            // edit player's age
            //
            DisplayGamePlayScreen("Player Edit - Age", Text.EditPlayerAge(), ActionMenu.EditPlayer, "");
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft, ConsoleLayout.InputBoxPositionTop - 3);

            Console.WriteLine("\tCurrent Value: " + player.Age + " ");
            DisplayInputBoxPrompt("");
            userResponse = Console.ReadLine();
            if (userResponse.ToUpper() == "EDIT")
            {
                int gameTravelerAge;

                GetAge("Enter your age: ", 13, 100, out gameTravelerAge);
                player.Age = gameTravelerAge;
            }

            //
            // edit player's gender
            //
            DisplayGamePlayScreen("Player Edit - Gender", Text.EditPlayerGender(), ActionMenu.EditPlayer, "");
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft, ConsoleLayout.InputBoxPositionTop - 3);

            Console.WriteLine("\tCurrent Value: " + player.PlayerGender + " ");
            DisplayInputBoxPrompt("");
            userResponse = Console.ReadLine();
            if (userResponse.ToUpper() == "EDIT")
            {
                DisplayInputBoxPrompt($"Enter your gender, {player.Name}: ");
                player.PlayerGender = GetGender();
            }

            //
            //edit player's grade
            //
            DisplayGamePlayScreen("Player Edit - Grade", Text.EditPlayerGrade(), ActionMenu.EditPlayer, "");
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft, ConsoleLayout.InputBoxPositionTop - 3);

            Console.WriteLine("\tCurrent Value: " + player.PlayerGrade + " ");
            DisplayInputBoxPrompt("");
            userResponse = Console.ReadLine();
            if (userResponse.ToUpper() == "EDIT")
            {
                DisplayInputBoxPrompt("Enter your grade: ");
                player.PlayerGrade = GetGrade();
            }

            //
            // Display the player's info
            //
            DisplayGamePlayScreen("Player Edit - Complete", Text.EditPlayerEchoInfo(player), ActionMenu.PlayerMenu, "");

            return player;
        }

        /// <summary>
        /// Display Current Player Info
        /// </summary>
        public void DisplayPlayerInfo()
        {
            DisplayGamePlayScreen("Player Information", Text.PlayerInfo(_gamePlayer), ActionMenu.PlayerMenu, "");
        }
        #endregion

        #region Search Room Screen

        /// <summary>
        /// Overloaded Method -- Player does not have flashlight
        /// </summary>
        public void SearchLocation(bool flashlightStatus, bool searched, Sound flashlight)
        {
            int searchTime = 0;
            string messageBoxText = "";

            Location currentLocation = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

            if(searched == false)
            {
                if (flashlightStatus == false)
                {
                    searchTime = 3600;

                    HandleSearch(searchTime);
                }
                else
                {
                    searchTime = 1800;

                    flashlight.playSound(false);

                    HandleSearch(searchTime);

                    flashlight.playSound(false);

                    Pause(500);
                }

                currentLocation.Searched = true;
            }
            else
            {
                messageBoxText += "--This Location has already been searched--" + Environment.NewLine + Environment.NewLine;
            }

            // List of a game objects in the location
            List<GameObject> gameObjectsInCurrentLocation = _gameUniverse.GetGameObjectsByLocationId(_gamePlayer.LocationID);

            //List of NPCs at the location
            List<Npc> npcsInCurrentLocation = _gameUniverse.GetNpcByLocation(_gamePlayer.LocationID);

            //Get general contents in room and list of objects in location
            messageBoxText += Text.SearchRoom(currentLocation) + Environment.NewLine + Environment.NewLine;
            messageBoxText += Text.GameObjectsViewList(gameObjectsInCurrentLocation);
            messageBoxText += Text.NpcChooseList(npcsInCurrentLocation);

            //Display the location info
            Location current = _gameUniverse.GetLocationById(_gamePlayer.LocationID);
            DisplayGamePlayScreen(current.Name, messageBoxText, ActionMenu.MainMenu, "");
        }

        /// <summary>
        /// Search Screen
        /// </summary>
        /// <param name="searchTime"></param>
        public void HandleSearch(int searchTime)
        {
            Pause(200);

            SearchRoomTime(searchTime);
        }

        /// <summary>
        /// Display Search Text and Delay 
        /// </summary>
        /// <param name="ms"></param>
        public void SearchRoomTime(int ms)
        {
            int delay = 0;
            int increment = 300;
            int counter = 0; //Keep track of dots
            string message = "Searching ";

            DisplayGamePlayScreen("", "", ActionMenu.Blank, "");
            Console.CursorVisible = false;

            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 3);
            Console.Write(message);
            while (delay < ms)
            {
                if (counter == 3)
                {
                    System.Threading.Thread.Sleep(increment);
                    counter = 0;
                    Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 3);
                    Console.Write(message + "    ");
                }
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2 + message.Length + counter, ConsoleLayout.MessageBoxPositionTop + 3);
                System.Threading.Thread.Sleep(increment);
                Console.Write(".");
                delay += increment;
                counter++;
            }
            System.Threading.Thread.Sleep(increment);
        }

        #endregion

        #region Location Screens

        /// <summary>
        /// Get Next Location ID
        /// </summary>
        public int DisplayGetNextLocation()
        {
            int locationID = 0;
            bool validLocation = false;

            DisplayGamePlayScreen("Travel to a New Location", Text.Travel(_gamePlayer, _gameUniverse.Locations), ActionMenu.MainMenu, "");

            while (!validLocation)
            {
                //get integer from user
                GetInt("New Location: ", 1, _gameUniverse.GetMaxLocationId(), out locationID);

                //validate integer and determine acessibility
                if (_gameUniverse.IsValidLocationId(locationID))
                {
                    if (_gameUniverse.IsAccessibleLocation(locationID))
                    {
                        validLocation = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you are attempting to go to an inaccessible location. Please Try Again.");
                    }

                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage("It appears you entered an invalid location ID. Please Try Again.");

                }
            }
            return locationID;
        }

        /// <summary>
        /// Display the Locations the Player has visited so far
        /// </summary>
        public void DisplayLocationsVisited()
        {
            //Create a list of locations that have been visited.
            List<Location> visitedLocations = new List<Location>();

            //Player keeps track of each location visited
            foreach (int locationID in _gamePlayer.LocationsVisited)
            {
                //Get visited location by its ID and add to the list<location>
                visitedLocations.Add(_gameUniverse.GetLocationById(locationID));
            }

            //Display all visited locations
            DisplayGamePlayScreen("Locations Visited", Text.VisitedLocations(visitedLocations), ActionMenu.PlayerMenu, "");
        }

        /// <summary>
        /// List of all Locations in Map
        /// </summary>
        public void DisplayListOfLocations()
        {
            DisplayGamePlayScreen("List: Locations", Text.ListLocations(_gameUniverse.Locations), ActionMenu.GameInfo, "");
        }
        #endregion

        #region Object Screens

        public void DisplayListOfKnownGameObjects()
        {
            DisplayGamePlayScreen("Game Objects", Text.ListKnownGameObjects(_gameUniverse.GameObjects, _gamePlayer), ActionMenu.GameInfo, "");
        }

        public int DisplayGetGameObjectToLookAt(bool searched)
        {
            int gameObjectId = 0;
            bool validGameObjectId = false;

            if (searched == true)
            {
                //
                // Get a list of game objects in the current location
                //
                List<GameObject> gameObjectsInCurrentLocation = _gameUniverse.GetGameObjectsByLocationId(_gamePlayer.LocationID);

                if (gameObjectsInCurrentLocation.Count > 0)
                {
                    DisplayGamePlayScreen("Game Object", Text.GameObjectsChoiceList(gameObjectsInCurrentLocation), ActionMenu.MainMenu, "");

                    while (!validGameObjectId)
                    {
                        //
                        // Get an integer from player
                        //
                        GetInt("Enter the ID of the desired object: ", 0, 0, out gameObjectId);

                        //
                        // Validate integer as a valid game object ID and in current location
                        //
                        if (_gameUniverse.IsValidGameObjectByLocationId(gameObjectId, _gamePlayer.LocationID))
                        {
                            validGameObjectId = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("Invalid Game Object Id.");
                        }
                    }
                }
                else
                {
                    DisplayGamePlayScreen("Game Object", "--There are no objects here--", ActionMenu.MainMenu, "");
                }
            }
            else
            {
                DisplayGamePlayScreen("Game Object", "--You must search the room before interacting with the objects--", ActionMenu.MainMenu, "");
            }


            return gameObjectId;
        }

        /// <summary>
        /// Display Information about an object
        /// </summary>
        /// <param name="gameObject"></param>
        public void DisplayGameObjectInfo(GameObject gameObject)
        {
            Location current = _gameUniverse.GetLocationById(_gamePlayer.LocationID);
            DisplayGamePlayScreen(current.Name, Text.LookAt(gameObject, _gamePlayer.Inventory), ActionMenu.MainMenu, "");
        }

        /// <summary>
        /// Display Player's Inventory
        /// </summary>
        public void DisplayInventory()
        {
            DisplayGamePlayScreen("Inventory", Text.CurrentInventory(_gamePlayer.Inventory), ActionMenu.MainMenu, "");
        }

        #endregion

        #region NPC Screens

        /// <summary>
        /// Display all NPC the Player knows of
        /// </summary>
        public void DisplayListAllNpcObjects()
        {
            DisplayGamePlayScreen("All Known NPCs", Text.ListKnownNpcObjects(_gameUniverse.Npcs, _gamePlayer.NpcsInteractedWith), ActionMenu.GameInfo, "");
        }

        /// <summary>
        /// Return a NPC id based on user's selection
        /// </summary>
        /// <param name="searched"></param>
        /// <returns></returns>
        public int DisplayGetNpcToTalkTo(bool searched)
        {
            int npcId = 0;
            bool validNpcId = false;

            if (searched == true)
            {
                //Get a list of npcs in the current location
                List<Npc> npcsInCurrentLocation = _gameUniverse.GetNpcByLocation(_gamePlayer.LocationID);

                if (npcsInCurrentLocation.Count() > 0)
                {
                    DisplayGamePlayScreen("Select a NPC", Text.NpcInteractWithList(npcsInCurrentLocation), ActionMenu.NpcMenu, "");

                    while (!validNpcId)
                    {
                        //Get int from user
                        GetInt("Enter the ID of the NPC: ", 0, 0, out npcId);

                        //validate int as a valid NPC id and in the current location
                        if (_gameUniverse.IsValidNpcByLocationId(npcId, _gamePlayer.LocationID))
                        {
                            Npc npc = _gameUniverse.GetNpcById(npcId);

                            if (npc is ISpeak)
                            {
                                validNpcId = true;
                            }
                            else
                            {
                                ClearInputBox();
                                DisplayInputErrorMessage("This character does not speak.");
                            }
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("Invalid NPC id. Please try again.");
                        }
                    }
                }
                else
                {
                    DisplayGamePlayScreen("Select a NPC: ", "There are no NPCs at this location.", ActionMenu.NpcMenu, "");
                }
            }
            else
            {
                DisplayGamePlayScreen("Game Object", "--You must search the room before interacting with NPCs--", ActionMenu.NpcMenu, "");
            }

            return npcId;
        }

        /// <summary>
        /// Talk to Screen for NPCs
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="_illusion"></param>
        public void DisplayTalkTo(Npc npc, Sound _illusion)
        {
            if (npc.Name == "Black Cat") _illusion.playSound(false);

            //Allow the NPC to access speak functionality
            ISpeak speakingNpc = npc as ISpeak;

            string message = speakingNpc.Speak();

            if (message == "")
            {
                message = "This character has nothing to say.";
            }

            DisplayGamePlayScreen("Speak to NPC", message, ActionMenu.NpcMenu, "");
        }
        #endregion

        #region Introduction / Game Setup

        /// <summary>
        /// Pre-Game
        /// Display the Introduction Letter
        /// </summary>
        public void DisplayRules()
        {
            DisplayGamePlayScreen("IMPORTANT | Rules of the Game", Text.Rules() + " \n" + " \n" + "\tPress any key to begin the game.", ActionMenu.Introduction, "");
            GetContinueKey();

            _viewStatus = ViewStatus.PlayingGame;
        }

        /// <summary>
        /// During Game
        /// Display the Introduction Letter
        /// </summary>
        public void DisplayRulesInGame()
        {
            DisplayGamePlayScreen("IMPORTANT | Rules of the Game", Text.Rules(), ActionMenu.GameInfo, "");
        }

        /// <summary>
        /// Display user's game difficulty selection
        /// </summary>
        /// <param name="index"></param>
        /// <param name="difficulty"></param>
        public void SetUpGame(int index, GameDifficulty.Difficulty[] difficulty)
        {
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft - 1, ConsoleLayout.MessageBoxPositionTop + 8);
            Console.CursorVisible = false;

            for (int i = 0; i < difficulty.Length; i++)
            {
                if (index == i)
                {
                    Console.Write(">> ".PadLeft(10));
                    Console.WriteLine(difficulty[i] + " <<");
                }
                else
                {
                    Console.Write("   ".PadLeft(10));
                    Console.WriteLine(difficulty[i] + "   ");
                }
            }
        }
        #endregion

        #region Room / Door Screens

        /// <summary>
        /// Screen when the player enters school lobby for the first time
        /// </summary>
        /// <param name="lockDoor"></param>
        public void SchoolLobby(Sound lockDoor)
        {
            //Screen 1
            DisplayGamePlayScreen("School Lobby", Text.SchoolLobby(), ActionMenu.StoryLine, "");
            GetContinueKey();

            //Pause
            Pause(200);

            //Play locked door sound
            lockDoor.playSound(false);

            //Pause
            Pause(500);

            //Screen 2
            DisplayGamePlayScreen("School Lobby", Text.SchoolLobbyPT2(), ActionMenu.StoryLine, "");
            GetContinueKey();

        }

        /// <summary>
        /// Screen when player enters the hallway for the first time
        /// </summary>
        public void Hallway()
        {
            DisplayGamePlayScreen("Hallway", Text.Hallway(), ActionMenu.Blank, "");
            GetContinueKey();
        }

        /// <summary>
        /// Screen when Player is at a Door
        /// </summary>
        /// <param name="openDoor"></param>
        /// <param name="closeDoor"></param>
        /// <param name="running"></param>
        /// <param name="door"></param>
        public void Door(Sound openDoor, Sound closeDoor, Sound running, Door door)
        {
            Console.CursorVisible = false;

            //Get user to 'open' the door
            DisplayGamePlayScreen("Player At Door", Text.Door(), ActionMenu.StoryLine, "");
            GetContinueKey();

            //Play door opening sound
            openDoor.playSound(false);

            DisplayGamePlayScreen(" ", Text.Empty(), ActionMenu.Blank, "");
            //Pause to let door sound play through
            Pause(1400);

            //Pause for a random amount of time
            Pause(_gameUniverse.GetRandom(500, 1200));

            if (door.EnemyBehindDoor == true)
            {
                running.playSound(false);
            }

            //Get user to 'close' the door or 'enter' room
            DisplayGamePlayScreen("Player At Door", Text.DoorPT2(), ActionMenu.Door, "");
            ClearInputBox();
        }
        #endregion

        #region Game Over Screens

        /// <summary>
        /// Exit Screen
        /// </summary>
        public void DisplayExitScreen()
        {
            DisplayGamePlayScreen("Exit Screen", Text.ExitScreen(), ActionMenu.Exit, "");
            Console.ReadLine();
        }

        /// <summary>
        /// Player dies
        /// </summary>
        public void GameOver()
        {
            DisplayGamePlayScreen("Game Over", Text.GameOver(), ActionMenu.Exit, "");
            Console.ReadLine();
        }

        /// <summary>
        /// Player wins the game
        /// </summary>
        public void GameWon()
        {
            DisplayGamePlayScreen("You Win!", Text.GameWon(_gamePlayer), ActionMenu.Exit, "");
            Console.ReadLine();
        }
        #endregion

        #endregion
    }
}
