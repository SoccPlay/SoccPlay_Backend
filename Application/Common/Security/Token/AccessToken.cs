namespace Application.Common.Security.Token
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpirationTicks { get; set; }
        public RefreshToken RefreshToken { get; set; }

        public AccessToken(string token, long expirationTicks, RefreshToken refreshToken)
        {
            Token = token;
            ExpirationTicks = expirationTicks;
            RefreshToken = refreshToken;
        }
    }
}
