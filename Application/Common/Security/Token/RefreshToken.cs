namespace Application.Common.Security.Token;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}