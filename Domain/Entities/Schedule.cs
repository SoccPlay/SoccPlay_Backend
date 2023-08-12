using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Schedule
    {
        public Guid ScheduleId { get; set; }
        public DateTime StarTime { get; set; }
        public DateTime EndTime { get; set; }
        public float Price { get; set; }
        public string Status { get; set; } = null!;
        public Guid PitchPitchId { get; set; }
        public int BookingBookingId { get; set; }

        public virtual Booking BookingBooking { get; set; } = null!;
        public virtual Pitch PitchPitch { get; set; } = null!;
    }
}
