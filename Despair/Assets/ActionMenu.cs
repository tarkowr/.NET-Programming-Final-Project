using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// static class to hold key/value pairs for menu options
    /// </summary>
    public static class ActionMenu
    {
        public enum CurrentMenu
        {
            Introduction,
            InitializePlayer,
            MainMenu,
            Door,
            PlayerMenu,
            NpcMenu,
            GameInfo
        }

        public static CurrentMenu currentMenu = CurrentMenu.MainMenu;

        public static Menu Introduction = new Menu()
        {
            MenuName = "Introduction",
            MenuTitle = "",
            MenuChoices = new Dictionary<char, PlayerAction>()
                    {
                        { ' ', PlayerAction.None }
                    }
        };

        public static Menu InitializePlayer = new Menu()
        {
            MenuName = "InitializePlayer",
            MenuTitle = "Initialize Player",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { '1', PlayerAction.Exit }
                }
        };

        public static Menu EditPlayer = new Menu()
        {
            MenuName = "EditPlayer",
            MenuTitle = "Edit Player",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { ' ', PlayerAction.None }
                }
        };

        public static Menu StoryLine = new Menu()
        {
            MenuName = "StoryLine",
            MenuTitle = "Story Line",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { ' ', PlayerAction.None }
                }
        };

        public static Menu Blank = new Menu()
        {
            MenuName = "",
            MenuTitle = "",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { ' ', PlayerAction.None }
                }
        };

        public static Menu Door = new Menu()
        {
            MenuName = "DoorOptions",
            MenuTitle = "Door Options",
            MenuChoices = new Dictionary<char, PlayerAction>()
            {
                { 'c', PlayerAction.CloseDoor },
                { 'e', PlayerAction.EnterRoom }
            }
        };

        public static Menu MainMenu = new Menu()
        {
            MenuName = "MainMenu",
            MenuTitle = "Main Menu",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { '1', PlayerAction.PlayerMenu },
                    { '2', PlayerAction.Travel },
                    { '3', PlayerAction.SearchArea },
                    { '4', PlayerAction.ViewObject },
                    { '5', PlayerAction.Inventory },
                    { '6', PlayerAction.NpcMenu },
                    { '7', PlayerAction.GameInformation },
                    { '0', PlayerAction.Exit }
                }
        };

        public static Menu PlayerMenu = new Menu()
        {
            MenuName = "PlayerMenu",
            MenuTitle = "Player Menu",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { '1', PlayerAction.PlayerInfo },
                    { '2', PlayerAction.PlayerEdit },
                    { '3', PlayerAction.LocationsVisited },
                    { '0', PlayerAction.ReturnToMainMenu },
                }
        };

        public static Menu NpcMenu = new Menu()
        {
            MenuName = "NpcMenu",
            MenuTitle = "Npc Menu",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { '1', PlayerAction.TalkTo },
                    { '0', PlayerAction.ReturnToMainMenu },
                }
        };

        public static Menu GameInfo = new Menu()
        {
            MenuName = "GameInformation",
            MenuTitle = "Game Information Menu",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { '1', PlayerAction.ListLocations },
                    { '2', PlayerAction.ListKnownObjects },
                    { '3', PlayerAction.ListKnownNpcs },
                    { '4', PlayerAction.GameRules },
                    { '0', PlayerAction.ReturnToMainMenu },
                }
        };

        public static Menu Exit = new Menu()
        {
            MenuName = "Exit",
            MenuTitle = "",
            MenuChoices = new Dictionary<char, PlayerAction>()
                {
                    { ' ', PlayerAction.None}
                }
        };
    }
}
