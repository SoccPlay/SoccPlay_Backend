using Application.Common.Security.HashPassword;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.Service.Implement;

public class OwnerImplement : OwnerService
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public OwnerImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseAccountOwner> CreateOwner(RequestAccountOwner request)
    {
        var owner = _mapper.Map<Owner>(request);
        owner.Status = ACCOUNTENUM.ACTIVE.ToString();
        owner.Account.Role = ROLEENUM.OWNER.ToString();
        owner.Account.Password = _passwordHasher.HashPassword(request.Password);
        _unitOfWork.Owner.Add(owner);
        _unitOfWork.Account.Add(owner.Account);
        _unitOfWork.Save();
        return _mapper.Map<ResponseAccountOwner>(owner);
    }
}