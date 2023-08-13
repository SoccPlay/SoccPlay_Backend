using AutoMapper;
using Application.Common.Security.HashPassword;
using Application.IRepository.IUnitOfWork;
using Application.Service;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Implement;

public class AdminImplement : AdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public AdminImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseAccountAdmin> CreateAdmin(RequestAccountAdmin request)
    {
        var admin = _mapper.Map<Admin>(request);
        admin.Status = ACCOUNTENUM.ACTIVE.ToString();
        admin.Account.Role = ROLEENUM.ADMIN.ToString();
        admin.Account.Password = _passwordHasher.HashPassword(request.Password);
        _unitOfWork.Admin.Add(admin);
        _unitOfWork.Account.Add(admin.Account);
        _unitOfWork.Save();
        return _mapper.Map<ResponseAccountAdmin>(admin);
    }
}