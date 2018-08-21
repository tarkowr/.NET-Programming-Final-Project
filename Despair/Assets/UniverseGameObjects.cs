using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public static partial class UniverseGameObjects
    {
        public static List<GameObject> gameObject = new List<GameObject>()
        {
            new PlayerObject
            {
                Id = 1,
                Name = "Map",
                LocationId = 5,
                Description = "A deteriorated map of the city from over 300 years ago.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Evidence,
                CanInventory = true,
                HasContent = true,
                Content = "The map has a large red circle around where Eastshore was built... however, it was a burial ground back then.",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 2,
                Name = "Diary",
                LocationId = 6,
                Description = "A dusty, black diary belonging to a girl who atteneded the school.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Evidence,
                CanInventory = true,
                HasContent = true,
                Content = "Dear Diary...",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 3,
                Name = "Letter",
                LocationId = 3,
                Description = "A letter addressed to the City Council that was never actually sent, still laying on the principal's desk.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Evidence,
                CanInventory = true,
                HasContent = true,
                Content = "In his letter, the principal describes how 'financial struggles' were not the real reason why the school closed. He reveals his suspiction that the school is cursed.",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 4,
                Name = "Dissertation",
                LocationId = 8,
                Description = "A dissertation, written by Eastshore's Librarian, lay perfectly in tact on her desk.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Evidence,
                CanInventory = true,
                HasContent = true,
                Content = "It argues that the tragic events at Eastshore were derived from a powerful force who wanted to establish its presence in the school. Almost as if it was disturbed that a school was built on those grounds.",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 5,
                Name = "Key",
                LocationId = 9,
                Description = "A small, golden key",
                ExperiencePoints = 0,
                Type = PlayerObjectType.GameItem,
                CanInventory = true,
                HasContent = false,
                Content = "",
                Value = 1,
                Stackable = false,
                LocationRestrictions = new List<int>
                {
                    1, 2, 3, 4, 9, 10, 11, 12
                }
            },
            new PlayerObject
            {
                Id = 6,
                Name = "Flashlight",
                LocationId = 3,
                Description = "A blue and silver flashlight, with batteries already in it.\n" +
                " \n" +
                "Benefits of having the flashlight: \n" +
                "1) You can now navigate through Eastshore's Hallway \n" +
                "2) Decreased Search Time when looking through a room. Listen for the clicks of the flashlight!",
                ExperiencePoints = 0,
                Type = PlayerObjectType.GameItem,
                CanInventory = true,
                HasContent = false,
                Content = "",
                Value = 1,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 7,
                Name = "Batteries",
                LocationId = 3,
                Description = "Two AA batteries",
                ExperiencePoints = 0,
                Type = PlayerObjectType.GameItem,
                CanInventory = true,
                HasContent = true,
                Content = "WARNING: Choking Hazard!",
                Value = 100,
                Stackable = true,
                LocationRestrictions = new List<int>
                {
                    9, 10, 11, 12
                }
            },
            new PlayerObject
            {
                Id = 8,
                Name = "[???]",
                LocationId = 2,
                Description = "A piece of paper, with words and symbols written largely in red ink.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Unknown,
                CanInventory = false,
                HasContent = true,
                Content = "I've been waiting for you",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 9,
                Name = "Menu",
                LocationId = 7,
                Description = "A list containing all of the foods that were prepared in Eastshore's kitchen.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Unknown,
                CanInventory = false,
                HasContent = false,
                Content = "",
                Value = 0,
                Stackable = false
            },
            new PlayerObject
            {
                Id = 10,
                Name = "[???]",
                LocationId = 7,
                Description = "A hardcover book with all but one page ripped out of it.",
                ExperiencePoints = 0,
                Type = PlayerObjectType.Unknown,
                CanInventory = false,
                HasContent = true,
                Content = "There is more than one way to the Cafeteria...",
                Value = 0,
                Stackable = false
            }
        };
    }
}
