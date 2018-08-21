using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Despair
{
    /// <summary>
    /// controller for the MVC pattern in the application
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private ConsoleView _gameConsoleView;
        private Player _gamePlayer;
        private Universe _gameUniverse;
        private Location _currentLocation;
        private bool _playingGame;
        private GameDifficulty.Difficulty _gameDifficulty; 

        //
        //Events and Delegates
        //
        private event EndGame PlayerCaught;
        private delegate void EndGame();

        private event HandleKey KeyPressed;
        private delegate int HandleKey(ConsoleKeyInfo i, int index);

        private event HandleNpc NpcInteraction;
        private delegate void HandleNpc(int id);

        //
        // Sound variables
        //
        private Sound _menuMusic = new Sound("Media//MenuMusic.wav");
        private Sound _thunder = new Sound("Media//Thunder.wav");
        private Sound _lock = new Sound("Media//DoorLock.wav");
        private Sound _openDoor = new Sound("Media//DOOROPEN.wav");
        private Sound _closeDoor = new Sound("Media//DoorClose.wav");
        private Sound _jumpScare = new Sound("Media//JumpScare.wav");
        private Sound _flashlight = new Sound("Media//Flashlight.wav");
        private Sound _static = new Sound("Media//GameOverStatic.wav");
        private Sound _running = new Sound("Media//running.wav");
        private Sound _laugh = new Sound("Media//Laugh.wav");
        private Sound _illusion = new Sound("Media//IllusionInteraction.wav");
        private Sound _lowStatic = new Sound("Media//LowStatic.wav");

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            //
            // setup all of the objects in the game
            //
            InitializeGame();

            //
            // begins running the application UI
            //
            ManageGameLoop();
        }

        #endregion

        #region METHODS

        #region Main Methods

        /// <summary>
        /// initialize the major game objects
        /// </summary>
        private void InitializeGame()
        {
            _gamePlayer = new Player();
            _gameUniverse = new Universe();
            PlayerObject playerObject;
            _gameConsoleView = new ConsoleView(_gamePlayer, _gameUniverse);
            _playingGame = true;

            //
            // Add event handler to each object
            //

            foreach (GameObject g in _gameUniverse.GameObjects)
            {
                if (g is PlayerObject)
                {
                    playerObject = g as PlayerObject;
                    playerObject.ObjectAddedToInventory += HandleObjectAddedToInventory;
                }
            }

            //
            // Wire PlayerCaught event to EndGame event handler
            //
            PlayerCaught += new EndGame(JumpScare);
            PlayerCaught += new EndGame(GameOver);

            //Key pressed event for switching menu option
            KeyPressed += new HandleKey(Movement);

            //Handle Player talking to NPC
            NpcInteraction += new HandleNpc(HandleNpcInteraction);

            Console.CursorVisible = false;
        }

        private void GameIntroduction()
        {
            _menuMusic.playSound(true);

            // display splash screen
            _playingGame = _gameConsoleView.DisplaySpashScreen();

            if (!_playingGame)
            {
                Environment.Exit(1);
            }

            //Get game difficulty
            int x = SetUpGameDifficulty();
            _gameDifficulty = (GameDifficulty.Difficulty)x;

            // display introductory message
            _gameConsoleView.DisplayGamePlayScreen("Introduction", Text.Introduction(), ActionMenu.Introduction, "");
            _gameConsoleView.GetContinueKey();

            //Get Player info
            InitializeMission();

            _menuMusic.stopSound(true);
            _thunder.playSound(true);

            Rules();

            AssignXP();

            //Assign Random Key Location
            GameObject gameObject = _gameUniverse.GetGameObjectById(5);
            PlayerObject Key = (PlayerObject)gameObject;

            Key.LocationId = _gameUniverse.AssignRandomLocation(Key);

            //Set Initial location
            _currentLocation = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

            //Display Current Location
            _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.IntroLocationInfo(), ActionMenu.MainMenu, "");

        }

        /// <summary>
        /// Assign XP to every location, object, npc in the game
        /// </summary>
        private void AssignXP()
        {
            foreach (Location l in _gameUniverse.Locations)
            {
                l.ExperiencePoints = _gameUniverse.GetRandom(12, 30);
            }
            foreach (GameObject g in _gameUniverse.GameObjects)
            {
                if(g is PlayerObject)
                {
                    PlayerObject p = (PlayerObject)g;
                    g.ExperiencePoints = _gameUniverse.GetRandom(6, 12);
                }
            }
            foreach (Npc n in _gameUniverse.Npcs)
            {
                if(n is IExperiencePoints)
                {
                    IExperiencePoints i = (IExperiencePoints)n;
                    i.XP = _gameUniverse.GetRandom(6, 12);
                }
            }
        }

        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;

            //Game Beginning
            GameIntroduction();

            //game loop
            while (_playingGame)
            {
                //process all events
                UpdateGameStatus();

                //update room accessibility
                UpdateAccessibility();

                // get next action from player
                travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);

                // choose an action based on the player's menu choice
                switch (travelerActionChoice)
                {
                    case PlayerAction.None:
                        break;
                    case PlayerAction.PlayerMenu:
                        PlayerMenu();
                        break;
                    case PlayerAction.Travel:

                        //Make sure there are accessible locations before travel menu
                        if (CanTravelAtLocation() == true)
                        {
                            //Get new location
                            _gamePlayer.LocationID = _gameConsoleView.DisplayGetNextLocation();
                            _currentLocation = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

                            //update room accessibility
                            UpdateAccessibility();

                            //Display new location
                            AdvanceInGame();
                        }
                        break;
                    case PlayerAction.SearchArea:
                        SearchLocation();
                        break;
                    case PlayerAction.ViewObject:
                        LookAtAction();
                        break;
                    case PlayerAction.Inventory:
                        _gameConsoleView.DisplayInventory();
                        break;
                    case PlayerAction.NpcMenu:
                        NpcMenu();
                        break;
                    case PlayerAction.GameInformation:
                        GameInfoMenu();
                        break;
                    case PlayerAction.Exit:
                        _lowStatic.playSound(false);
                        _gameConsoleView.DisplayExitScreen();
                        _playingGame = false;
                        break;
                    default:
                        break;
                }
            }

            // close the application
            Environment.Exit(1);
        }

        /// <summary>
        /// initialize the player info
        /// </summary>
        private void InitializeMission()
        {
            Player player = _gameConsoleView.GetInitialPlayerInfo();

            _gamePlayer.Name = player.Name;
            _gamePlayer.Age = player.Age;
            _gamePlayer.PlayerGender = player.PlayerGender;
            _gamePlayer.LocationID = 1;
            _gamePlayer.PlayerGrade = player.PlayerGrade;
            _gamePlayer.Alive = true;

            _gamePlayer.ExperiencePoints = 0;
            _gamePlayer.Lives = 1;
            _gamePlayer.HasKey = false;
            _gamePlayer.HasFlashlight = false; //-------------------------------------- CHANGE TO FALSE
            _gamePlayer.Batteries = 0;
            _gamePlayer.FlashlightBattery = 50;
        }
        #endregion

        #region Game Menus

        /// <summary>
        /// Menu for Player Info
        /// </summary>
        private void PlayerMenu()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;
            ActionMenu.currentMenu = ActionMenu.CurrentMenu.PlayerMenu;

            _gameConsoleView.DisplayGamePlayScreen("Player Menu", "--This Menu contains information about your Player--", ActionMenu.PlayerMenu, "");

            while (ActionMenu.currentMenu == ActionMenu.CurrentMenu.PlayerMenu)
            {
                travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.PlayerMenu);

                switch (travelerActionChoice)
                {
                    case PlayerAction.PlayerInfo:
                        _gameConsoleView.DisplayPlayerInfo();
                        break;
                    case PlayerAction.PlayerEdit:
                        _gameConsoleView.EditPlayerInfo(_gamePlayer);
                        break;
                    case PlayerAction.LocationsVisited:
                        _gameConsoleView.DisplayLocationsVisited();
                        break;
                    case PlayerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Menu for NPC Interaction
        /// </summary>
        private void NpcMenu()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;
            ActionMenu.currentMenu = ActionMenu.CurrentMenu.NpcMenu;

            _gameConsoleView.DisplayGamePlayScreen("Npc Menu", "--This Menu allows you to interact with NPCs--", ActionMenu.NpcMenu, "");

            while (ActionMenu.currentMenu == ActionMenu.CurrentMenu.NpcMenu)
            {
                travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.NpcMenu);

                switch (travelerActionChoice)
                {
                    case PlayerAction.TalkTo:
                        TalkToAction();
                        break;
                    case PlayerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Menu for Game Info
        /// </summary>
        private void GameInfoMenu()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;
            ActionMenu.currentMenu = ActionMenu.CurrentMenu.GameInfo;

            _gameConsoleView.DisplayGamePlayScreen("Game Information Menu", "--This Menu contains information about Despair--", ActionMenu.GameInfo, "");

            while (ActionMenu.currentMenu == ActionMenu.CurrentMenu.GameInfo)
            {
                travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.GameInfo);

                switch (travelerActionChoice)
                {
                    case PlayerAction.ListLocations:
                        _gameConsoleView.DisplayListOfLocations();
                        break;
                    case PlayerAction.ListKnownObjects:
                        _gameConsoleView.DisplayListOfKnownGameObjects();
                        break;
                    case PlayerAction.ListKnownNpcs:
                        _gameConsoleView.DisplayListAllNpcObjects();
                        break;
                    case PlayerAction.GameRules:
                        _gameConsoleView.DisplayRulesInGame();
                        break;
                    case PlayerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Object Methods

        /// <summary>
        /// Player Views an Object (will automatically add to inventory if it can be)
        /// </summary>
        private void LookAtAction()
        {
            //
            // Display the List of traveler objects in the current location and get the players choice
            //
            int gameObjectId = _gameConsoleView.DisplayGetGameObjectToLookAt(_currentLocation.Searched);

            //
            // Display game object info
            //
            if (gameObjectId != 0)
            {
                //
                // Get the game object from the universe
                //
                GameObject gameObject = _gameUniverse.GetGameObjectById(gameObjectId);

                //
                // Display info for the chosen object
                //
                _gameConsoleView.DisplayGameObjectInfo(gameObject);

                //
                // Add object to inventory if: it is a Player Object,
                // It can be put in the inventory,
                // And it isn't already in the inventory
                //
                if (gameObject is PlayerObject)
                {
                    PlayerObject p = gameObject as PlayerObject;

                    if (p.CanInventory == true && !_gamePlayer.Inventory.Contains(p))
                    {
                        _gamePlayer.Inventory.Add(p);

                        //Handle Object that is added to inventory
                        //Trigger PlayerObject event handler
                        p.OnObjectAddedToInventory();

                        //Add experience points to Player
                        _gamePlayer.ExperiencePoints += p.ExperiencePoints;
                    }
                    else if (p.Stackable && _gamePlayer.Inventory.Contains(p))
                    {
                        //Add experience points to Player
                        _gamePlayer.ExperiencePoints += p.ExperiencePoints;

                        switch (p.Name)
                        {
                            case "Batteries":
                                _gamePlayer.Batteries += 2;

                                BatteryHandler();

                                //Set the batteries to an unaccessible location
                                p.LocationId = _gameUniverse.Locations.Count() + 1;

                                break;
                            default:
                                break;
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Event Handler to be triggered when event is raised (object is added to inventory)
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="e"></param>
        private void HandleObjectAddedToInventory(object gameObject, EventArgs e)
        {
            PlayerObject p = (PlayerObject)gameObject;

            switch (p.Type)
            {
                case PlayerObjectType.GameItem:
                    HandleGameItem(p);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handles if player finds a Game Item
        /// </summary>
        /// <param name="p"></param>
        private void HandleGameItem(PlayerObject p)
        {
            switch (p.Name)
            {
                case "Flashlight":
                    _gamePlayer.HasFlashlight = true;
                    break;
                case "Batteries":
                    _gamePlayer.Batteries += 2;
                    p.LocationId = _gameUniverse.AssignRandomLocation(p);
                    break;
                case "Key":
                    _gamePlayer.HasKey = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Handle Battery Storage and Flashlight in Game
        /// </summary>
        private void BatteryHandler()
        {
            if (_gamePlayer.FlashlightBattery == 0)
            {
                if (_gamePlayer.Batteries >= 2)
                {
                    _gamePlayer.FlashlightBattery += 100;
                    _gamePlayer.Batteries -= 2;
                }
                if (_gamePlayer.Batteries == 0)
                {
                    //Get the battery object
                    GameObject battery = _gameUniverse.GetGameObjectById(7);

                    //Cast the battery as a player object to remove from player inventory
                    PlayerObject playerBattery = battery as PlayerObject;

                    _gamePlayer.Inventory.Remove(playerBattery);
                }
            }
        }

        #endregion

        #region NPC Methods

        /// <summary>
        /// NPC Talk To Main Method
        /// </summary>
        private void TalkToAction()
        {
            Location location = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

            int npcToTalkToId;

            //Get a NPC
            if (location.Searched == true) npcToTalkToId = _gameConsoleView.DisplayGetNpcToTalkTo(true);
            else npcToTalkToId = _gameConsoleView.DisplayGetNpcToTalkTo(false);

            if (npcToTalkToId != 0)
            {
                //Get the npc
                Npc npc = _gameUniverse.GetNpcById(npcToTalkToId);

                //If player is not ready, we display a custom message
                bool ready = IsPlayerReady();

                if (ready == true)
                {
                    Npc spiritNpc = _gameUniverse.GetNpcById(6);
                    Spirit spirit = (Spirit)spiritNpc;
                    spirit.Ready = true;
                }

                //Display the Message
                _gameConsoleView.DisplayTalkTo(npc, _illusion);

                //Raise the NPC Interaction Event
                NpcInteraction(npc.Id);
            }
        }

        /// <summary>
        /// Event Handler for when Player Interacts with NPC
        /// </summary>
        /// <param name="id"></param>
        private void HandleNpcInteraction(int id)
        {
            Npc npc = _gameUniverse.GetNpcById(id);

            if (!(_gamePlayer.NpcsInteractedWith.Contains(npc)))
            {
                //Add npc to npc interacted with list
                _gamePlayer.NpcsInteractedWith.Add(npc);

                if (npc is IExperiencePoints)
                {
                    IExperiencePoints npcXp = (IExperiencePoints)npc;

                    _gamePlayer.ExperiencePoints += npcXp.XP;
                }
            }

            //If player finds a black cat and interacts with it, assign the cat a new location
            if (npc.Name == "Black Cat")
            {
                npc.LocationID = _gameUniverse.AssignRandomLocation(npc);
            }
            else if (npc.Name == "Cloaked Figure")
            {
                if (_currentLocation.LocationID == 9)
                {
                    GrantRoomAccess(_currentLocation.LocationID + 1);
                    npc.LocationID = _currentLocation.LocationID + 1;
                }
                else
                {
                    bool ready = IsPlayerReady();

                    if (ready == false)
                    {
                        SetPlayerBackToStart();
                    }
                    else
                    {
                        Npc spiritNpc = _gameUniverse.GetNpcById(6);
                        Spirit spirit = (Spirit)spiritNpc;
                        spirit.Ready = true;

                        GrantRoomAccess(_currentLocation.LocationID + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Set the Player back to the School Lobby
        /// </summary>
        private void SetPlayerBackToStart()
        {
            _gamePlayer.LocationID = 2;
            _currentLocation = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

            //Remove the spirit NPC so the player has to interact with him again
            Npc spirit = _gameUniverse.GetNpcById(6);
            _gamePlayer.NpcsInteractedWith.Remove(spirit);
            spirit.LocationID = 9;

            Spirit s = (Spirit)spirit;
            s.Ready = false;

            _gameUniverse.UpdateLocation(_gamePlayer);
        }

        /// <summary>
        /// Check if Player has all Evidence before they can leave the school
        /// </summary>
        /// <returns></returns>
        private bool IsPlayerReady()
        {
            int count = 0;
            int evidenceInInventory = 0;
            bool ready;

            //Total all pieces of evidence
            foreach (GameObject g in _gameUniverse.GameObjects)
            {
                if (g is PlayerObject)
                {
                    PlayerObject p = (PlayerObject)g;
                    if (p.Type is PlayerObjectType.Evidence) count++;
                }
            }

            //Total all pieces of evidence in the inventory
            foreach (PlayerObject p in _gamePlayer.Inventory)
            {
                if (p.Type == PlayerObjectType.Evidence) evidenceInInventory++;
            }

            //Ternary Operator Used to compare counts
            ready = (count > evidenceInInventory) ? false : true;

            return ready;
        }
        #endregion

        #region Other Game Functionality

        /// <summary>
        /// Pre-Game
        /// Display Introduction Letter
        /// </summary>
        private void Rules()
        {
            _gameConsoleView.DisplayRules();
        }

        /// <summary>
        /// Update Player Info Based on their Actions
        /// </summary>
        private void UpdateGameStatus()
        {
            if (!_gamePlayer.HasVisted(_currentLocation.LocationID))
            {
                //add new location to the list of visited locations if this is a first visit
                _gamePlayer.LocationsVisited.Add(_currentLocation.LocationID);

                //update xp for visiting locations
                _gamePlayer.ExperiencePoints += _currentLocation.ExperiencePoints;
            }
            BatteryHandler();
        }

        /// <summary>
        /// Update Room Accessibility based on user's location
        /// </summary>
        private void UpdateAccessibility()
        {
            _gameUniverse.UpdateLocation(_gamePlayer);
        }

        /// <summary>
        /// Player is caught by enemy -- Game Over Pt2
        /// </summary>
        private void GameOver()
        {
            _static.playSound(false);
            _gameConsoleView.GameOver();
            Environment.Exit(0);
        }

        /// <summary>
        /// Search Room Functionality
        /// </summary>
        private void SearchLocation()
        {
            bool flashlightStatus = false;
            bool searched = false;

            Location currentLocation = _gameUniverse.GetLocationById(_gamePlayer.LocationID);

            if (currentLocation.Searched == true)
            {
                searched = true;
            }
            if (_gamePlayer.HasFlashlight && _gamePlayer.FlashlightBattery > 0)
            {
                flashlightStatus = true;
                _gamePlayer.FlashlightBattery -= 25;

                BatteryHandler();
            }

            _gameConsoleView.SearchLocation(flashlightStatus, searched, _flashlight);
     
        }

        /// <summary>
        /// Display a screen based on where the user traveled
        /// </summary>
        private void AdvanceInGame()
        {
            //Check location to play or stop wind sound in hallway
            if (_gamePlayer.LocationID == 4)
            {
                _thunder.playSound(true);
            }
            else if (_gamePlayer.LocationID != 4 && _thunder.Playing == true)
            {
                _thunder.stopSound(false);
            }

            //Display screens based on player's location and game status
            switch (_gamePlayer.LocationID)
            {
                case 2:
                    if (_gamePlayer.LocationsVisited.Contains(2))
                    {
                        //Current Location
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    }
                    else
                    {
                        //Stop Playing Thunder
                        _thunder.stopSound(false);

                        //Display School Lobby Story
                        _gameConsoleView.SchoolLobby(_lock);

                        //Current Location
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    }
                    break;
                case 3:
                    //Door Screen
                    DoorLoop();

                    //Current Location
                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                    break;
                case 4:
                    if (_gamePlayer.LocationsVisited.Contains(4))
                    {
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                    }
                    else
                    {
                        _gameConsoleView.Hallway();
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                    }
                    break;
                case 5:
                    //Door Screen
                    DoorLoop();

                    if (_gamePlayer.LocationsVisited.Contains(5))
                    {
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                    }
                    else
                    {
                        _gameConsoleView.Pause(250);
                        _laugh.playSound(false);
                        _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

                    }
                    break;
                case 6:
                    //Door Screen
                    DoorLoop();

                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
                case 7:
                    //Door Screen
                    DoorLoop();

                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
                case 8:
                    //Door Screen
                    DoorLoop();

                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
                case 9:

                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
                case 10:
                    //Remove the spirit NPC so the player has to interact with him again
                    Npc npc = _gameUniverse.GetNpcById(6);
                    _gamePlayer.NpcsInteractedWith.Remove(npc);

                    //Restrict Access to the previous room
                    RestrictRoomAccess(_currentLocation.LocationID - 1);

                    //Restrict Acces to exit
                    RestrictRoomAccess(_currentLocation.LocationID + 1);

                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
                case 11:
                    _lowStatic.playSound(false);
                    _gameConsoleView.GameWon();
                    Environment.Exit(0);
                    break;
                default:
                    _gameConsoleView.DisplayGamePlayScreen(_currentLocation.Name, Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                    break;
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Method to check if the player can travel at this location right now
        /// </summary>
        /// <returns></returns>
        private bool CanTravelAtLocation()
        {
            int numOfLocations = _gameUniverse.Locations.Count();
            int numOfAccessibleLocations = 0;
            bool canTravel;

            foreach (Location l in _gameUniverse.Locations)
            {
                if (l.Accessable == false) numOfAccessibleLocations++;
            }

            canTravel = (numOfLocations == numOfAccessibleLocations) ? false : true;

            return canTravel;
        }

        /// <summary>
        /// Allows access to the room of the passed room ID
        /// </summary>
        /// <param name="locationId"></param>
        private void GrantRoomAccess(int locationId)
        {
            Location l = _gameUniverse.GetLocationById(locationId);
            l.Accessable = true;
        }

        /// <summary>
        /// Restricts access to the room of the passed room ID
        /// </summary>
        /// <param name="locationId"></param>
        private void RestrictRoomAccess(int locationId)
        {
            Location l = _gameUniverse.GetLocationById(locationId);
            l.Accessable = false;
        }

        /// <summary>
        /// Get a key from the user
        /// </summary>
        /// <returns></returns>
        public ConsoleKeyInfo GetKey()
        {
            ConsoleKeyInfo i = new ConsoleKeyInfo();
            i = Console.ReadKey(true);
            return i;
        }
        #endregion

        #region Setup Game Difficulty Methods

        /// <summary>
        /// Game Difficulty Main Method
        /// </summary>
        /// <returns></returns>
        private int SetUpGameDifficulty()
        {
            ConsoleKeyInfo selection = new ConsoleKeyInfo();
            int index = 0;

            _gameConsoleView.DisplayGamePlayScreen("Game Setup", Text.Setup(), ActionMenu.Blank, "");

            //Display the Menu with default at Easy
            Movement(selection, index);

            //Move selector until user presses ENTER on one option
            while (selection.Key != ConsoleKey.Enter)
            {
                //Get a key and raise event
                selection = GetKey();
                index = KeyPressed(selection, index);
            }

            //User's final choice
            return index;
        }

        /// <summary>
        /// Event Handler for when Player Uses Up and Down Keys
        /// </summary>
        /// <param name="i"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int Movement(ConsoleKeyInfo i, int index)
        {
            //Array of Enum options for game difficulty
            GameDifficulty.Difficulty[] difficulty = new GameDifficulty.Difficulty[] { GameDifficulty.Difficulty.Easy, GameDifficulty.Difficulty.Normal, GameDifficulty.Difficulty.Hard };

            if (i.Key == ConsoleKey.DownArrow)
            {
                index++;
                if (index > 2) index = 0;
            }
            else if (i.Key == ConsoleKey.UpArrow)
            {
                index--;
                if (index < 0) index = 2;
            }

            //Update Screen
            _gameConsoleView.SetUpGame(index, difficulty);
            return index;
        }
        #endregion

        #region Door Methods

        /// <summary>
        /// Main Door Loop
        /// Player stays at door until they can safely enter or get caught by the enemy
        /// </summary>
        /// <param name="enter"></param>
        /// <returns></returns>
        public void DoorLoop()
        {
            Door currentDoor = new Door();
            currentDoor.DoorLocation = _currentLocation;

            AssignEnemyBehindDoor(currentDoor);

            while (currentDoor.Entered == false)
            {
                _gameConsoleView.Door(_openDoor, _closeDoor, _running, currentDoor);
                HandleDoor(currentDoor);
            }
        }

        /// <summary>
        /// Get User's Door Choice and Perform Actions based on their choice
        /// </summary>
        /// <param name="currentDoor"></param>
        public void HandleDoor(Door currentDoor)
        {
            //Get player's door choice
            PlayerAction travelerActionChoice = PlayerAction.None;
            travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.Door);

            switch (travelerActionChoice)
            {
                case PlayerAction.EnterRoom:
                    if (currentDoor.EnemyBehindDoor == false)
                    {
                        currentDoor.Entered = true;
                    }
                    else
                    {
                        _playingGame = false;

                        //Raise the event
                        PlayerCaught();
                    }
                    break;
                case PlayerAction.CloseDoor:
                    _closeDoor.playSound(false);

                    //Re-assign enemy
                    AssignEnemyBehindDoor(currentDoor);
                    break;
                case PlayerAction.Caught:
                    _playingGame = false;

                    //Raise the event
                    PlayerCaught();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Based on Game Difficulty, Assign an Enemy Behind the Door
        /// </summary>
        /// <param name="currentDoor"></param>
        private void AssignEnemyBehindDoor(Door currentDoor)
        {
            //Get rate at which enemy will spawn (2/rate), based on game difficulty
            int spawnRate = 4 - (int)_gameDifficulty;

            //Assign enemy behind door
            int rand = _gameUniverse.GetRandom(0, spawnRate);
            if (rand == 0 || rand == 1)
            {
                currentDoor.EnemyBehindDoor = true;
            }
            else
            {
                currentDoor.EnemyBehindDoor = false;
            }
            //currentDoor.EnemyBehindDoor = false; //------------------------- DELETE THIS LINE FOR ENEMY
        }

        /// <summary>
        /// Jumpscare Event -- Game Over Pt1
        /// </summary>
        private void JumpScare()
        {
            Console.CursorVisible = false;

            //Jumpscare the Player
            _jumpScare.playSound(false);

            //Pause the Game before displaying the game over screen
            _gameConsoleView.Pause(1500);
        }

        #endregion

        #endregion
    }
}
