namespace Application.Model.Request.RequestFeedback;

public class RequestFeedback
{
    public int Rate { get; set; }
    public string Description { get; set; } = null!;
    public Guid LandId { get; set; }
    public Guid CustomerId { get; set; }
}