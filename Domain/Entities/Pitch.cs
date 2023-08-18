namespace Domain.Entities;

public class Pitch
{
    public Pitch()
    {
        Schedules = new HashSet<Schedule>();
    }

    public Guid PitchId { get; set; }
    public string Name { get; set; } = null!;
    public int Size { get; set; }
    public string Status { get; set; } = null!;
    public Guid LandId { get; set; }
    public Guid OwnerId { get; set; }
    public virtual Land Land { get; set; } = null!;
    public virtual Owner Owner { get; set; } = null!;
    public virtual ICollection<Schedule> Schedules { get; set; }
}