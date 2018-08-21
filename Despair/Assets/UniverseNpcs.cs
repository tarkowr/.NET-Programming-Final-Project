using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    class UniverseNpcs
    {
        public static List<Npc> Npcs = new List<Npc>()
        {
            new Illusion
            {
                Id = 1,
                Name = "Young boy",
                LocationID = 5,
                Description = "A little boys sits in one of the chairs, coloring a map.",
                Messages = new List<string>
                {
                    "Take this map before he finds you...",
                    "Look at this map I colored. Hurry, take it."
                },
                XP = 0
            },
            new Illusion
            {
                Id = 2,
                Name = "Little Girl",
                LocationID= 6,
                Description = "A girl sits in the corner of the bathroom, curled up.",
                XP = 0
            },
            new Illusion
            {
                Id = 3,
                Name = "Librarian",
                LocationID = 8,
                Description = "The Librarian shuffles acround the room, moving around and picking up books",
                Messages = new List<string>
                {
                    "I can't wait to leave this place!",
                    "I think I have enough evidence to shut the school down...",
                    "Once I've left Eastshore, I'll never return."
                },
                XP = 0
            },
            new Illusion
            {
                Id = 4,
                Name = "Black Cat",
                LocationID = 2,
                Description = "A black cat with green eyes that almost shine in the darkness.",
                Messages = new List<string>
                {
                    "~ Cafeteria ~"
                },
                XP = 0,
                LocationRestrictions = new List<int>
                {
                    9, 10, 11, 12
                }
            },
            new Illusion
            {
                Id = 5,
                Name = "Enemy",
                LocationID = 0,
                Description = "Avoid him at all costs...",
                XP = 0
            },
            new Spirit
            {
                Id = 6,
                Name = "Cloaked Figure",
                LocationID = 9,
                Description = "A tall, cloaked figure holding a book of chants.",
                Messages = new List<string>
                {
                    "Follow me.",
                    "You are not ready...",
                    "You are ready. You now have what you need. Destroy this school."
                },
                Ready = false
            }

        };

    }
}
