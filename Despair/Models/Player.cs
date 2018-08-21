using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// the character class the player uses in the game
    /// </summary>
    public class Player : Character
    {
        #region ENUMERABLES
        public enum Grade
        {
            Third,
            Fourth,
            Fifth
        }

        #endregion

        #region FIELDS
        private Grade _grade;

        //game status
        private int _experiencePoints;
        private int _lives;
        private bool _hasKey;
        private bool _hasFlashlight;
        private int _flashlightBattery;
        private int _batteries;

        //location visited
        private List<int> _locationsVisited;

        //inventory list
        private List<PlayerObject> _inventory;

        //NPC list
        private List<Npc> _npcsInteractedWith;      

        #endregion


        #region PROPERTIES

        public Grade PlayerGrade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        public bool HasKey
        {
            get { return _hasKey; }
            set { _hasKey = value; }
        }

        public bool HasFlashlight
        {
            get { return _hasFlashlight; }
            set { _hasFlashlight = value; }
        }

        public int FlashlightBattery
        {
            get { return _flashlightBattery; }
            set { _flashlightBattery = value; }
        }

        public int Batteries
        {
            get { return _batteries; }
            set { _batteries = value; }
        }

        public List<int> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        public List<PlayerObject> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }

        public List<Npc> NpcsInteractedWith
        {
            get { return _npcsInteractedWith; }
            set { _npcsInteractedWith = value; }
        }

        #endregion


        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<int>();
            _inventory = new List<PlayerObject>();
            _npcsInteractedWith = new List<Npc>();
        }

        public Player(string name, Character.Gender gender, int age, int LocationID, bool alive) : base(name, gender, age, LocationID, alive)
        {
            _locationsVisited = new List<int>();
            _inventory = new List<PlayerObject>();
            _npcsInteractedWith = new List<Npc>();
        }

        #endregion


        #region METHODS

        /// <summary>
        /// Check if Location has been visited
        /// </summary>
        /// <param name="_locationID"></param>
        /// <returns></returns>
        public bool HasVisted(int _locationID)
        {
            if (LocationsVisited.Contains(_locationID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
