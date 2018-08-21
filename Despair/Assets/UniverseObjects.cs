using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    /// <summary>
    /// static class to hold all objects in the game universe; locations, game objects, npc's
    /// </summary>
    public static partial class UniverseObjects
    {

        public static List<Location> Locations = new List<Location>()
        {

        new Location
            {
                Name = "Eastshore Entrance",
                LocationID = 1,
                RoomNumber = "N/A",
                Description = "You are outside of Eastshore elementary, alone. Seeing your old school, abandoned... " +
                    "empty, feels strange. It is raining outside. You approach the front entrance you used to walk through every day " +
                    "as a child. This time it feels different.\n",
                GeneralContents = "There are two unlocked wooden doors that lead into the school." +
                    "Trees, bushes, vines, and plants block your view to the outside world.\n",
                Accessable = true,
                ExperiencePoints = 0,
                Searched = true,
                AccessibleLocations = new List<int>
                {
                    2
                }
            },

            new Location
            {
                Name = "School Lobby",
                LocationID = 2,
                RoomNumber = "N/A",
                Description = "The Lobby was the nicest part of Eastshore. " +
                    "They probably wanted guests to have a good first impression of the school. " +
                    "Even now, the lobby doesn't appear to be completely desolate, "+
                    "it still has the same old chairs, tables, and paintings.",
                GeneralContents = "There are two doors in this room, leading to the Principal's Office and the Hallway. " +
                    "The tables in the lobby are all pushed to the sides of the room, the chairs forming a large circle, all facing inward. " +
                    "In the middle of the circle, lays a small piece of paper.",
                Accessable = true,
                ExperiencePoints = 10,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    3, 4
                }
            },

            new Location
            {
                Name = "Principal's Office",
                LocationID = 3,
                RoomNumber = "11",
                Description = "This room was once occupied by each Eastshore prinicpal. " +
                    "It is rather small, and has no windows whatsoever.",
                GeneralContents = "Papers are scattered, desk drawers are opened, and everything is knocked over." +
                    "Oddly enough, the prinicpal's desk and chair were perfectly in tact... " +
                    "Could it be possible that something, or someone, is still using it?",
                Accessable = false,
                ExperiencePoints = 20,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    2
                }
            },

            new Location
            {
                Name = "Hallway",
                LocationID = 4,
                RoomNumber = "N/A",
                Description = "Eastshore's main hallway serves as the connection between the lobby/entrance and the rest of the school. It stretches all the way to the Cafeteria, " +
                              "which is blocked off by large wooden doors. Lockers are lined along the walls, most of them still intact. ",
                GeneralContents =  "This strange, black symbol is carved all over the walls and ceiling; it appears that there are hundreds of them.\n" +
                    " \n" +
                    "\t o \n" +
                    "\t<|>\n" +
                    "\t - ",
                Accessable = false,
                ExperiencePoints = 13,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    2, 5, 6, 7, 8, 9
                }
            },

            new Location
            {
                Name = "Classroom",
                LocationID = 5,
                RoomNumber = "21",
                Description = "Classroom 21 was the first classroom build in Eastshore. The room has tile floors and various educational posters hanging from its white walls. " +
                              "There are four long rows of desks and chairs, and a projector in the middle of the room... still shining brightly onto the whiteboard.",
                GeneralContents = "A little boy sits at one of the desks, coloring a map.",
                Accessable = false,
                ExperiencePoints = 17,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    4
                }
            },

            new Location
            {
                Name = "Bathroom",
                LocationID = 6,
                RoomNumber = "31",
                Description = "The Bathroom has blue, chipped tile floors, and the once white stalls are no longer clean. " +
                              "Most of the stall doors are unhinged, and the sinks are all dried up.",
                GeneralContents = "A girl sits in the corner of the bathroom, next to the sink, cautiously scanning the room, while not making a sound. " +
                    "A look of fear is on her face as she grips a diary in her hands.",
                Accessable = false,
                ExperiencePoints = 16,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    4
                }
            },


            new Location
            {
                Name = "Kitchen",
                LocationID = 7,
                RoomNumber = "41",
                Description = "Kitchen staff prepared food for the students in this room." +
                              "There is a variety of kitchen utensils hanging from the walls, along with several traditional wooden cabinets. " +
                              "In the center of the room, a large table holds all kinds of foods/ingredients.",
                GeneralContents = "A vent near the ceiling is missing its cover. It is large enough to be crawled through.",
                Accessable = false,
                ExperiencePoints = 14,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    4, 12
                }
            },


            new Location
            {
                Name = "Library",
                LocationID = 8,
                RoomNumber = "51",
                Description = "Other than the cafeteria and gym, the Library was the largest room in Eastshore. " +
                              "All sorts of books sat on the never-ending shelves. The carpet floors and tall glass windows once gave the " +
                              "place a welcoming feeling.",
                GeneralContents = "The librarian is in the room with you. She shuffles around, picking up books from the ground and placing them back on the shelves. " +
                    "She is mumbling about almost being finished with a report...",
                Accessable = false,
                ExperiencePoints = 6,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    4
                }
            },

            new Location
            {
                Name = "Cafeteria",
                LocationID = 9,
                RoomNumber = "71",
                Description = "The Cafeteria, a very open area of the school, has the same old long tables that are perfectly aligned. " +
                              "Again, more school posters hung from the walls, covering many of the strange black symbols that were seen in the Hallway.",
                GeneralContents = "In the middle of the room stands a tall, cloaked figure who is whispering chants from a book.",
                Accessable = false,
                ExperiencePoints = 11,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    10
                }
            },

            new Location
            {
                Name = "Teachers' Lounge",
                LocationID = 10,
                RoomNumber = "81",
                Description = "The Teachers' Lounge was a room no student at Eastshore had ever seen, and probabily didn't want to see." +
                              "There were several rumors of parties in here, back in the day.",
                GeneralContents = "This room is very large, much larger than it appears to be. The cloaked figure is standing in the room, awaiting your arrival.",
                Accessable = false,
                ExperiencePoints = 10,
                Searched = false,
                AccessibleLocations = new List<int>
                {
                    11
                }
            },
            new Location
            {
                Name = "The Exit",
                LocationID = 11,
                RoomNumber = "N/A",
                Description = "Free At Last!",
                Accessable = false,
                ExperiencePoints = 100,
                Searched = true,
                AccessibleLocations = new List<int>
                {

                }
            },
            new Location
            {
                Name = "Vent",
                LocationID = 12,
                RoomNumber = "N/A",
                Description = "It no longer remains a secret.",
                Accessable = false,
                ExperiencePoints = 100,
                Searched = true,
                AccessibleLocations = new List<int>
                {
                    9
                }
            }
        };
    }
}
