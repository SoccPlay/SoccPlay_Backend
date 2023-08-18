namespace Application.Model.Response.ResponseAccount;

public class ResponseAccountOwner
{
    public Guid AccountId { get; set; }
    public Guid OwnerId { get; set; }
    public string UserName { get; set; }
    public string Role { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
}