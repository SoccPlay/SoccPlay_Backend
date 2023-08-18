namespace Domain.Entities;

public class Owner
{
    public Owner()
    {
        Lands = new HashSet<Land>();
        Pitches = new HashSet<Pitch>();
    }

    public Guid OwnerId { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
    public virtual ICollection<Land> Lands { get; set; }
    public virtual ICollection<Pitch> Pitches { get; set; }
}