using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public class Illusion : Npc, ISpeak, IExperiencePoints
    {
        public override int Id { get; set; }
        public override string Description { get; set; }
        public List<string> Messages { get; set; }
        public int XP { get; set; }
        public List<int> LocationRestrictions { get; set; } 

        public string Speak()
        {
            if (this.Messages != null) return GetMessage();
            else return $"This character has nothing to say.";
        }

        private string GetMessage()
        {
            Random rand = new Random();
            int messageIndex = rand.Next(0, Messages.Count());
            return Messages[messageIndex];
        }
    }
}
