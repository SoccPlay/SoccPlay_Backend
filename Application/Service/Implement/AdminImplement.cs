using Application.Common.Security.HashPassword;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.Service.Implement;

public class AdminImplement : AdminService
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

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