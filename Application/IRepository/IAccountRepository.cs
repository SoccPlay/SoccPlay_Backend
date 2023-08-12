using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IAccountRepository  : IGenericRepository<Account>
{
    Task<Account> GetUserNameByAccount(string username);
    
}