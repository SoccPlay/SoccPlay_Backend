using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestAccount;

public class RequestAccountCustomer
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Address { get; set; }
    [EmailAddress]
    public string Email { get; set; }
}