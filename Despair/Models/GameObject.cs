using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Despair
{
    public abstract class GameObject
    {
        #region Properties
            public abstract int Id { get; set; }
            public abstract string Name { get; set; }
            public abstract string Description { get; set; }
            public abstract int LocationId { get; set; }
            public abstract int ExperiencePoints { get; set; }
        #endregion
    }
}
