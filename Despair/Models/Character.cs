using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// base class for the player and all game characters
    /// </summary>
    public class Character
    {
        #region ENUMERABLES

        public enum Gender
        {
            None,
            Male,
            Female,
            Other
        }

        #endregion

        #region FIELDS

        private string _name;
        private int _locationID;
        private int _age;
        private Gender _gender;
        private bool _alive = true;
            
        #endregion

        #region PROPERTIES

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int LocationID
        {
            get { return _locationID; }
            set { _locationID = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public Gender PlayerGender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public bool Alive
        {
            get { return _alive; }
            set { _alive = value; }
        }



        #endregion

        #region CONSTRUCTORS

        public Character()
        {

        }

        public Character(string name, Gender gender, int age, int spaceTimeLocationID, bool alive)
        {
            _name = name;
            _gender = gender;
            _age = age;
            _locationID = spaceTimeLocationID;
            _alive = alive;
        }

        #endregion

        #region METHODS


        #endregion
    }
}