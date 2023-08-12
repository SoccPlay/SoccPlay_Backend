using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Price
    {
        public Price()
        {
            Pitches = new HashSet<Pitch>();
        }

        public Guid PriceId { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Price1 { get; set; }
        public int Size { get; set; }
        public Guid LandLandId { get; set; }

        public virtual Land LandLand { get; set; } = null!;
        public virtual ICollection<Pitch> Pitches { get; set; }
    }
}
