namespace Domain.Entities;

public class Admin
{
    public Guid AdminId { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Status { get; set; } = null!;
    public Guid AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;
}