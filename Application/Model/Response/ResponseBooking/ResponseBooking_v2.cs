namespace Application.Model.Response.ResponseBooking;

public class ResponseBooking_v2
{
    public Guid BookingId { get; set; }
    public float TotalPrice { get; set; }
    public string? Location { get; set; }
    public DateTime DateBooking { get; set; }
    public string Note { get; set; } = null!;
    public Guid CustomerId { get; set; }
    
    public string? Status { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}