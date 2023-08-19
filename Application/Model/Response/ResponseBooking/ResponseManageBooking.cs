namespace Application.Model.Response.ResponseBooking;

public class ResponseManageBooking
{
    public Guid BookingId { get; set; }
    
    public string Name { get; set; } 
    public float TotalPrice { get; set; }
    public string? Location { get; set; }
    public DateTime DateBooking { get; set; }
    public string Note { get; set; }
    public int size { get; set; }
    public string Status { get; set; }
}