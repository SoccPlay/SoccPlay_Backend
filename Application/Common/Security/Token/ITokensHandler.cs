using Domain.Entities;

namespace Application.Common.Security.Token;

public interface ITokensHandler
{
    AccessToken CreateAccessToken(Account account);
    RefreshToken TakeRefreshToken(string refresh, string userName);
    void RevokeRefreshToken(string token, string userName);
    Task<string> ClaimsFromToken(string token);
}