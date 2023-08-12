using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class PitchImage
    {
        public Guid ImageId { get; set; }
        public string Name { get; set; } = null!;
        public Guid LandId { get; set; }

        public virtual Land Land { get; set; } = null!;
    }
}
