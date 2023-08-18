namespace Domain.Entities;

public class Booking
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
    public Guid LandId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
    public virtual Land Land { get; set; } = null!;
    public virtual ICollection<Schedule> Schedules { get; set; } = null!;
}