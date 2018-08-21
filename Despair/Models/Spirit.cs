using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public class Spirit : Npc, ISpeak
    {
        public override int Id { get; set; }
        public override string Description { get; set; }
        public List<string> Messages { get; set; }
        public bool Ready { get; set; }

        public string Speak()
        {
            return GetMessage();
        }

        private string GetMessage()
        {
            int messageIndex;

            if (base.LocationID == 9) messageIndex = 0;
            else if (this.Ready == false) messageIndex = 1;
            else messageIndex = 2;

            return Messages[messageIndex];
        }
    }
}
