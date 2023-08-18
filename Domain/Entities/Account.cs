namespace Domain.Entities;

public class Account
{
    public Account()
    {
        Admins = new HashSet<Admin>();
        Customers = new HashSet<Customer>();
        Owners = new HashSet<Owner>();
    }

    public Guid AccountId { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;

    public virtual ICollection<Admin> Admins { get; set; }
    public virtual ICollection<Customer> Customers { get; set; }
    public virtual ICollection<Owner> Owners { get; set; }
}