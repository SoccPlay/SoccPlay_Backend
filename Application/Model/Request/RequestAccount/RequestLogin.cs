using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestAccount;

public class RequestLogin
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }
}