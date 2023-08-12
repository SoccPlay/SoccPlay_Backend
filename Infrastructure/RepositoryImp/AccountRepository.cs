using Microsoft.EntityFrameworkCore;
using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class AccountRepository : GenericRepository<Account>, IAccountRepository
{
    public AccountRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Account> GetUserNameByAccount(string username)
    {
        var account = await _context.Set<Account>()!.FirstOrDefaultAsync(a => a.UserName == username);
        return account;
    }


}