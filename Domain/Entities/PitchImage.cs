namespace Domain.Entities;

public class PitchImage
{
    public Guid ImageId { get; set; }
    public string Name { get; set; } = null!;
    public Guid LandId { get; set; }
    public virtual Land Land { get; set; } = null!;
}