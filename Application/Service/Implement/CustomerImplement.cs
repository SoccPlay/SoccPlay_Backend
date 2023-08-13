using Application.Common.Security.HashPassword;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Implement;

public class CustomerImplement : CustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CustomerImplement(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<ResponseAccountCustomer> CreateCustomer(RequestAccountCustomer request)
    {
        var customer = _mapper.Map<Customer>(request);
        customer.Status = ACCOUNTENUM.ACTIVE.ToString();
        customer.Account.Role = ROLEENUM.CUSTOMER.ToString();
        customer.Account.Password = _passwordHasher.HashPassword(request.Password);
        _unitOfWork.Customer.Add(customer);
        _unitOfWork.Account.Add(customer.Account);
        _unitOfWork.Save();
        return _mapper.Map<ResponseAccountCustomer>(customer);
    }
}