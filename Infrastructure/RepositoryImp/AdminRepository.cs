using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
    public AdminRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Admin> GetAdminByAccountId(Guid id)
    {
        var account = await _context.Admins.Include(a => a.Account).FirstOrDefaultAsync(a => a.AccountId == id);
        return account;
    }
}