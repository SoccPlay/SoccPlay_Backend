namespace Application.Model.Response.ResponseFeedback;

public class ResponseFeedback
{
    public Guid FeedbackId { get; set; }
    public int Rate { get; set; }
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }

    public Guid LandId { get; set; }
    public Guid CustomerId { get; set; }
}