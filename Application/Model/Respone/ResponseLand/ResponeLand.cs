using Domain.Entities;

namespace Application.Model.ResponseLand;

public class ResponseLand
{
    public Guid LandId { get; set; }
    public string NameLand { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int TotalPitch { get; set; }
    public string Description { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid OwnerId { get; set; }

    public string image { get; set; } = null!;

    public List<string> PitchImages { get; set; } = null!;

}