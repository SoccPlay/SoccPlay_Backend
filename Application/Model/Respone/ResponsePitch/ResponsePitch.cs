namespace Application.Model.Respone.ResponsePitch;

public class ResponsePitch
{
    public Guid PitchId { get; set; }
    public string Name { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid LandId { get; set; }
    public Guid OwnerId { get; set; }
}