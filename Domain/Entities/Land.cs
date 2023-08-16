﻿using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Domain.Entities
{
    public partial class Land
    {
        public Land()
        {
            Feedbacks = new HashSet<Feedback>();
            Pitches = new HashSet<Pitch>();
            Prices = new HashSet<Price>();
            Bookings = new HashSet<Booking>();
            Images = new HashSet<PitchImage>();
        }

        public Guid LandId { get; set; }
        public string NameLand { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Policy { get; set; }
        public string Location { get; set; } = null!;
        public int TotalPitch { get; set; }
        public string Description { get; set; } = null!;
        public Guid OwnerId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Owner Owner { get; set; } = null!;
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Pitch> Pitches { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<PitchImage> Images { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
