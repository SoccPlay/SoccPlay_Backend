﻿namespace Domain.Entities;

public class Schedule
{
    public Guid ScheduleId { get; set; }
    public DateTime StarTime { get; set; }
    public DateTime EndTime { get; set; }
    public float Price { get; set; }
    public string Status { get; set; } = null!;
    public DateTime Date { get; set; } 

    public Guid PitchPitchId { get; set; }
    public Guid BookingBookingId { get; set; }

    public virtual Booking BookingBooking { get; set; } = null!;
    public virtual Pitch PitchPitch { get; set; } = null!;
}