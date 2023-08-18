using Application.Model.Response.ResponseAccount;

namespace Application.Service;

public interface AccountService
{
    Task<ResponseAccountCustomer> GetCustomer(Guid AccountId);
    Task<ResponseAccountAdmin> GetAdmin(Guid AccountId);
    Task<ResponseAccountOwner> GetOwner(Guid AccountId);
}