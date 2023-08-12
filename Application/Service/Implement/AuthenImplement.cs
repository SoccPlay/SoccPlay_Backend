using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Security.HashPassword;
using Application.Common.Security.Token;
using Application.IRepository.IUnitOfWork;
using Application.Service;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;

namespace Infrastructure.Service
{
    public class AuthenImplement : AuthenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokensHandler _tokensHandler;
        private readonly IPasswordHasher _passwordHasher;

        public AuthenImplement(IUnitOfWork unitOfWork, IMapper mapper, ITokensHandler tokensHandler, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokensHandler = tokensHandler;
            _passwordHasher = passwordHasher;
        }

        public async Task<ResponseLogin> LoginAccessToken(RequestLogin requestLogin)
        {
            var account = await _unitOfWork.Account.GetUserNameByAccount(requestLogin.UserName);
            if (account == null)
            {
                throw new Exception("Not Found");
            }
            if (!_passwordHasher.VerifyPasswordB(requestLogin.Password, account.Password))
            {
                throw new Exception("ERROR HASH PASSWORD");
            }
            var token = _tokensHandler.CreateAccessToken(account);
            return _mapper.Map<ResponseLogin>(token);
        }

        public async Task<ResponseLogin> RefreshToken(string refreshToken, string username)
        {
            var token = _tokensHandler.TakeRefreshToken(refreshToken, username);
            if (token.Expiration < DateTime.UtcNow)
            {
                throw new Exception("Expired refresh token.");
            }
            var account = await _unitOfWork.Account.GetUserNameByAccount(username);
            var accessToken = _tokensHandler.CreateAccessToken(account);
            return _mapper.Map<ResponseLogin>(accessToken);
        }

        public void RevokeRefreshToken(string refreshToken, string username)
        {
            _tokensHandler.RevokeRefreshToken(refreshToken, username);
        }
    }
}
