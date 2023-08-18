namespace Application.Common.Security.Token;

public class AccessToken
{
    public AccessToken(string token, long expirationTicks, RefreshToken refreshToken)
    {
        Token = token;
        ExpirationTicks = expirationTicks;
        RefreshToken = refreshToken;
    }

    public string Token { get; set; }
    public long ExpirationTicks { get; set; }
    public RefreshToken RefreshToken { get; set; }
}