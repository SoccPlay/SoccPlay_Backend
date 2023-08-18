namespace Application.Model.Response.ResponseFile;

public class ResponseFile
{
    public Guid PitchImageId { get; set; }
    public Guid PitchId { get; set; }
    public string Name { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid LandId { get; set; }
    public Guid OwnerId { get; set; }
}