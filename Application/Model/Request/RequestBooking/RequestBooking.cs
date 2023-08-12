namespace Application.Model.Request.RequestBooking;

public class RequestBooking
{
    public string Note { get; set; } = null!;
    public int Size5 { get; set; } = 0;
    public int Size7 { get; set; } = 0;
    public Guid LandId { get; set; }
    public DateTime StarTime { get; set; }
    public DateTime EndTime { get; set; }
    public Guid CustomerId { get; set; }
}