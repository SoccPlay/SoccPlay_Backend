using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Land
    {
        public Land()
        {
            Feedbacks = new HashSet<Feedback>();
            Pitches = new HashSet<Pitch>();
            Prices = new HashSet<Price>();
        }

        public Guid LandId { get; set; }
        public string NameLand { get; set; } = null!;
        public string Title { get; set; } = null!;
        public int Policy { get; set; }
        public string Location { get; set; } = null!;
        public int TotalPitch { get; set; }
        public string Description { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Pitch> Pitches { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
    }
}
