namespace Application.Model.Response.ResponseBooking;

public class ResponeBooking_v3
{
    public Guid CustomerId { get; set; }
    public string? CustomerName { get; set; }
    
    public Guid LandId { get; set; }
    public string LandName { get; set; } = null!;
    public string? Location { get; set; }
    
    public Guid PitchId { get; set; }
    public string? PitchName { get; set; }
    public int Size { get; set; }
    
    public Guid BookingId { get; set; }
    public float TotalPrice { get; set; }
    public DateTime DateBooking { get; set; }
    public string Note { get; set; } = null!;
    public string? Status { get; set; }
    
    public Guid ScheduleId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}