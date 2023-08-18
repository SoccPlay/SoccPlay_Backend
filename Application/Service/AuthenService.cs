using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;

namespace Application.Service;

public interface AuthenService
{
    Task<ResponseLogin> LoginAccessToken(RequestLogin requestLogin);
    Task<ResponseLogin> RefreshToken(string refreshToken, string username);
    void RevokeRefreshToken(string refreshToken, string username);
}