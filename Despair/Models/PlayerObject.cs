using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public class PlayerObject : GameObject
    {
        public override int Id { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public override int LocationId { get; set; }
        public override int ExperiencePoints { get; set; }

        public PlayerObjectType Type { get; set; }
        public bool CanInventory { get; set; }
        public bool HasContent { get; set; }
        public string Content { get; set; }
        public int Value { get; set; }
        public bool Stackable { get; set; }
        public List<int> LocationRestrictions { get; set; } 

        public event EventHandler ObjectAddedToInventory;

        public void OnObjectAddedToInventory()
        {
            if(ObjectAddedToInventory != null)
            {
                ObjectAddedToInventory(this, EventArgs.Empty);
            }
        }
    }
}   
