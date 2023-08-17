using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IAccountRepository  : IGenericRepository<Account>
{
    Task<Account> GetUserNameByAccount(string username);
    Task<Account> GetCustomerByAccountId(Guid id);
    
    Task<Account> GetAdminByAccountId(Guid id);
    
    Task<Account> GetOwnerByAccountId(Guid id);
    
}