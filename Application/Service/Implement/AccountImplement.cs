using Application.IRepository.IUnitOfWork;
using Application.Model.Response.ResponseAccount;
using AutoMapper;

namespace Application.Service.Implement;

public class AccountImplement : AccountService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AccountImplement(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseAccountCustomer> GetCustomer(Guid AccountId)
    {
        var account = await _unitOfWork.Customer.GetCustomerByAccountId(AccountId);
        return _mapper.Map<ResponseAccountCustomer>(account);
    }

    public async Task<ResponseAccountAdmin> GetAdmin(Guid AccountId)
    {
        var account = await _unitOfWork.Admin.GetAdminByAccountId(AccountId);
        return _mapper.Map<ResponseAccountAdmin>(account);
    }

    public async Task<ResponseAccountOwner> GetOwner(Guid AccountId)
    {
        var account = await _unitOfWork.Owner.GetOwnerByAccountId(AccountId);
        return _mapper.Map<ResponseAccountOwner>(account);
    }
}