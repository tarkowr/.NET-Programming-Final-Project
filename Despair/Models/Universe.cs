using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// class of the game map 
    /// </summary>
    public class Universe
    {
        #region Fields -- ***** define all lists to be maintained by the Universe object *****

        //
        // list of all locations
        //
        private List<Location> _locations;
        private List<GameObject> _gameObjects;
        private List<Npc> _npcs;

        public List<Location> Locations
        {
            get { return _locations; }
            set { _locations = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return _gameObjects; }
            set { _gameObjects = value; }
        }

        public List<Npc> Npcs
        {
            get { return _npcs; }
            set { _npcs = value; }
        }
        


        #endregion

        #region ***** constructor *****

        //
        // default Universe constructor
        //
        public Universe()
        {
            //
            // add all of the universe objects to the game
            // 
            IntializeUniverse();
        }

        #endregion

        #region ***** define methods to initialize all game elements *****

        /// <summary>
        /// Give an object a random location on the map
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandom(int min, int max)
        {
            int rand;
            Random r = new Random();

            return rand = r.Next(min, max+1);
        }

        #endregion

        #region Working with Game Locations

        /// <summary>
        /// determine if the Space-Time Location Id is valid
        /// </summary>
        /// <param name="LocationId">true if Space-Time Location exists</param>
        /// <returns></returns>
        public bool IsValidLocationId(int LocationId)
        {
            List<int> locationIds = new List<int>();

            // 
            // Create a list of location ids. 
            //

            foreach (Location location in _locations)
            {
                locationIds.Add(location.LocationID);
            }

            //
            // Determine if the locationID is valid
            //
            if (locationIds.Contains(LocationId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// get a SpaceTimeLocation object using an Id
        /// </summary>
        /// <param name="Id">space-time location ID</param>
        /// <returns>requested space-time location</returns>
        public Location GetLocationById(int Id)
        {
            Location location = null;

            //
            // Go through location list and get correct one
            //
            foreach (Location l in _locations)
            {
                if (l.LocationID == Id)
                {
                    location = l;
                }
            }

            //
            // ID not found in universe
            // throw exception
            //
            if (location == null)
            {
                string feedBackMessage = $"The Location ID {Id} is not valid here.";
                throw new ArgumentException(Id.ToString(), feedBackMessage);
            }

            return location;
        }


        /// <summary>
        /// determine if a location is accessible to the player
        /// </summary>
        /// <param name="LocationId"></param>
        /// <returns>accessible</returns>
        public bool IsAccessibleLocation(int LocationId)
        {
            Location location = GetLocationById(LocationId);
            if (location.Accessable == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// return the highest ID for a Location object
        /// </summary>
        /// <returns>next SpaceTimeLocationObjectID </returns>
        public int GetMaxLocationId()
        {
            int MaxId = 0;

            foreach (Location location in Locations)
            {
                if (location.LocationID > MaxId)
                {
                    MaxId = location.LocationID;
                }
            }

            return MaxId;
        }


        /// <summary>
        /// initialize the universe with all of the space-time locations
        /// </summary>
        private void IntializeUniverse()
        {
            _locations = UniverseObjects.Locations;
            _gameObjects = UniverseGameObjects.gameObject;
            _npcs = UniverseNpcs.Npcs;
        }

        /// <summary>
        /// Assign a new Location to an Object -- Overloaded 1
        /// </summary>
        /// <returns></returns>
        public int AssignRandomLocation(PlayerObject playerObject)
        {
            int locationID;

            //LINQ
            IEnumerable<int> availableLocations =
                from l in _locations
                where l.Searched == false && !playerObject.LocationRestrictions.Contains(l.LocationID)
                select l.LocationID;

            //Make sure there are atleast two locations before assigning a spot
            if (availableLocations.Count() >= 2)
            {
                Random r = new Random();
                do
                {
                    locationID = r.Next(0, GetMaxLocationId());

                } while (!availableLocations.Contains(locationID));

            }
            else
            {
                locationID = _locations.Count() + 1;
            }

            return locationID;
        }

        /// <summary>
        /// Assign a new location to a NPC -- Overloaded 2
        /// </summary>
        /// <returns></returns>
        public int AssignRandomLocation(Npc npc)
        {
            Illusion illusion = (Illusion)npc;

            int locationID;

            //LINQ
            IEnumerable<int> availableLocations =
                from l in _locations
                where l.Searched == false && !illusion.LocationRestrictions.Contains(l.LocationID)
                select l.LocationID;

            //Make sure there are atleast two locations before assigning a spot
            if (availableLocations.Count() >= 2)
            {
                Random r = new Random();
                do
                {
                    locationID = r.Next(0, GetMaxLocationId());

                } while (!availableLocations.Contains(locationID));

            }
            else
            {
                locationID = _locations.Count() + 1;
            }

            return locationID;
        }

        /// <summary>
        /// Change Room Acessibility based on User's Current Location
        /// </summary>
        /// <param name="p"></param>
        public void UpdateLocation(Player p)
        {
            Location currentLocation = GetLocationById(p.LocationID);

            //Default Access Settings
            for (int i = 1; i <= Locations.Count(); i++)
            {
                if (currentLocation.AccessibleLocations.Contains(i))
                {
                    Locations[i-1].Accessable = true;
                }
                else
                {
                    Locations[i-1].Accessable = false;
                }
            }

            //Extra Restrictions
            if(p.HasFlashlight == false)
            {
                Location hallway = GetLocationById(4);
                hallway.Accessable = false;
            }

            if(p.LocationID == 4 && p.HasKey == false)
            {
                Location cafeteria = GetLocationById(9);
                cafeteria.Accessable = false;
                if(p.LocationID == 12)
                {
                    cafeteria.Accessable = true;
                }
            }

            if(p.NpcsInteractedWith.Contains(GetNpcById(4)))
            {
                Location vent = GetLocationById(12);
                vent.Accessable = false;
            }

            if(p.LocationID == 9 && !p.NpcsInteractedWith.Contains(GetNpcById(6)))
            {
                Location lounge = GetLocationById(10);
                lounge.Accessable = false;
            }

            Spirit spirit = (Spirit)GetNpcById(6);

            if (!spirit.Ready || !p.NpcsInteractedWith.Contains(spirit))
            {
                Location exit = GetLocationById(11);
                exit.Accessable = false;
            }

        }

        #endregion

        #region Working with Game Objects

        /// <summary>
        /// Validate Player's selection of a Game Object's ID
        /// </summary>
        /// <param name="gameObjectId"></param>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public bool IsValidGameObjectByLocationId(int gameObjectId, int currentLocation)
        {
            List<int> gameObjectIds = new List<int>();

            //
            // Create a list of game object ids in the player's current location
            //
            foreach (GameObject g in _gameObjects)
            {
                if (g.LocationId == currentLocation)
                {
                    gameObjectIds.Add(g.Id);
                }
            }

            //
            // Determine if the game object id is a valid id and then return the result
            //
            if (gameObjectIds.Contains(gameObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checking if the Player Object is valid
        /// </summary>
        /// <param name="playerObjectId"></param>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public bool IsValidPlayerObjectByLocationId(int playerObjectId, int currentLocation)
        {
            List<int> playerObjectIds = new List<int>();

            //
            // List of player object ids that are in the current location
            //
            foreach (GameObject g in _gameObjects)
            {
                if(g.LocationId == currentLocation && g is PlayerObject)
                {
                    playerObjectIds.Add(g.Id);
                }
            }

            //
            // If the game object id is valid
            //

            if (playerObjectIds.Contains(playerObjectId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validate Player's choice of a game object to see if it exists
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public GameObject GetGameObjectById(int Id)
        {
            GameObject gameObjectToReturn = null;

            //
            // Get the correct game object
            //
            foreach (GameObject g in _gameObjects)
            {
                if(g.Id == Id)
                {
                    gameObjectToReturn = g;
                }
            }

            //
            // The Id was not found in the universe
            //
            if(gameObjectToReturn == null)
            {
                string feedbackMessage = $"The Game Object Id {Id} does not exist.";
                throw new ArgumentException(feedbackMessage, Id.ToString());
            }

            return gameObjectToReturn;
        }

        /// <summary>
        /// Get Game Objects Based on Where the Player is located in the map
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public List<GameObject> GetGameObjectsByLocationId(int locationId)
        {
            List<GameObject> gameObjects = new List<GameObject>();

            //
            // Go through game object list and grab all that are in the current location
            //

            foreach (GameObject g in GameObjects)
            {
                if(g.LocationId == locationId)
                {
                    gameObjects.Add(g);
                }
            }

            return gameObjects;
        }


        public bool IsItemInInventory(Player player, string item)
        {
            bool inInventory = false;

            foreach (PlayerObject obj in player.Inventory)
            {
                if (obj.Name == item)
                {
                    inInventory = true;
                }
                else
                {
                    inInventory = false;
                }
            }

            return inInventory;
        }

        #endregion

        #region Working with NPC's

        public bool IsValidNpcByLocationId(int id, int currentLocation)
        {
            List<int> npcIds = new List<int>();

            //List of NPC id's at the current location
            foreach (Npc npc in _npcs)  
            {
                if(npc.LocationID == currentLocation)
                {
                    npcIds.Add(npc.Id);
                }
            }

            //Check if the passed npc Id is in this list
            if (npcIds.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Npc GetNpcById(int id)
        {
            Npc npcToReturn = null;

            //Grab the correct npc
            foreach (Npc npc in _npcs)
            {
                if(npc.Id == id)
                {
                    npcToReturn = npc;
                }
            }

            //The Id was not found in the universe
            if(npcToReturn == null)
            {
                string feedback = $"The NPC Id {id} does not exist.";
                throw new ArgumentException(feedback, id.ToString());
            }

            return npcToReturn;
        }

        public List<Npc> GetNpcByLocation(int locationId)
        {
            List<Npc> npcs = new List<Npc>();

            //Add all npcs at the current location to a list
            foreach (Npc npc in _npcs)
            {
                if(npc.LocationID == locationId)
                {
                    npcs.Add(npc);
                }
            }

            return npcs;
        }


        #endregion

    }
}
