using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;

namespace Application.Service
{
    public interface AuthenService
    {
        Task<ResponseLogin> LoginAccessToken(RequestLogin requestLogin);
        Task<ResponseLogin> RefreshToken(string refreshToken, string username);
        void RevokeRefreshToken(string refreshToken, string username);


    }
}
