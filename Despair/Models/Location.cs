using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// class for the game map locations
    /// </summary>
    public class Location
    {
        #region FIELDS

        private string _name;
        private int _locationID; // must be a unique value for each object
        private string _roomNumber;
        private string _description;
        private string _generalContents;
        private bool _accessable;
        private int _experiencePoints;
        private bool _searched;
        private List<int> _accessibleLocation;
      
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

        public string RoomNumber
        {
            get { return _roomNumber; }
            set { _roomNumber = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string GeneralContents
        {
            get { return _generalContents; }
            set { _generalContents = value; }
        }

        public bool Accessable
        {
            get { return _accessable; }
            set { _accessable = value; }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        public bool Searched
        {
            get { return _searched; }
            set { _searched = value; }
        }

        public List<int> AccessibleLocations
        {
            get { return _accessibleLocation; }
            set { _accessibleLocation = value; }
        }

        #endregion


        #region CONSTRUCTORS



        #endregion


        #region METHODS



        #endregion


    }
}
