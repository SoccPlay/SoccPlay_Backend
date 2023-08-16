namespace Application.Model.ResponseLand;

public class ResponseLand_2
{
    public Guid LandId { get; set; }
    public string NameLand { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int TotalPitch { get; set; }
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid OwnerId { get; set; }
}