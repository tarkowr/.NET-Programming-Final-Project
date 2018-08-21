using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public class Door
    {
        public bool Entered { get; set; }
        public bool EnemyBehindDoor { get; set; }   
        public Location DoorLocation { get; set; }
    }
}
