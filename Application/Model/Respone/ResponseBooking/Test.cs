namespace Application.Model.Respone.ResponseBooking;

public class Test
{
    public Guid BookingId { get; set; }
    public float TotalPrice { get; set; }
    public string Location { get; set; }
    public DateTime DateBooking { get; set; }
    public string Note { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public List<Guid> ScheduleId { get; set; }
    
}