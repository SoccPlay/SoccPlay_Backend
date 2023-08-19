using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestAccount;
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

    public async Task<ResponseAccountCustomer> UpdateProfileCustomer(RequestUpdateProfile requestUpdateProfile)
    {
        var account = _unitOfWork.Customer.GetById(requestUpdateProfile.CustomerId);
        if (requestUpdateProfile.Address != null)
        {
            account.Address = requestUpdateProfile.Address;
        }
        if (requestUpdateProfile.Email != null)
        {
            account.Email = requestUpdateProfile.Email;
        }
        if (requestUpdateProfile.FullName != null)
        {
            account.FullName = requestUpdateProfile.FullName;
        }
        if (requestUpdateProfile.Phone != null)
        {
            account.Phone = requestUpdateProfile.Phone;
        }
        _unitOfWork.Save();
        return _mapper.Map<ResponseAccountCustomer>(account);
    }
}