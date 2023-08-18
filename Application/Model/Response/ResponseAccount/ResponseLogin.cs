namespace Application.Model.Response.ResponseAccount;

public class ResponseLogin
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public long Expiration { get; set; }
}