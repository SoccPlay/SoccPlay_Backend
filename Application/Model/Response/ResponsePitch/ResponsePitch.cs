namespace Application.Model.Response.ResponsePitch;

public class ResponsePitch
{
    public Guid PitchImageId { get; set; }
    public Guid PitchId { get; set; }
    public string Name { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid LandId { get; set; }
    public string nameLand { get; set; }
    public DateTime Date { get; set; }
    public float PriceMin { get; set; }
    public float PriceMax { get; set; }
    public Guid OwnerId { get; set; }
}