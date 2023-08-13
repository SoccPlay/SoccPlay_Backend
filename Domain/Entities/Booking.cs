using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            Schedules = new HashSet<Schedule>();
        }

        public Guid BookingId { get; set; }
        public float TotalPrice { get; set; }
        public DateTime DateBooking { get; set; }
        public string Note { get; set; } = null!;
        public Guid CustomerId { get; set; }
        
        public string Status { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Schedule> Schedules { get; set; } = null!;
    }
}
