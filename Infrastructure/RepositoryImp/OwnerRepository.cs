using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Owner> GetOwnerById(Guid ownerId)
    {
       var owner = await _context.Set<Owner>()!.Include(o => o.Account).FirstOrDefaultAsync(o => o.OwnerId == ownerId);
       if (owner == null)
       {
           throw new Exception("Owner ERROR NULL");
       }
       return owner;
    }
    
    public async Task<Owner> GetOwnerByAccountId(Guid id)
    {
        var owner = await _context.Set<Owner>()!.Include(o => o.Account).FirstOrDefaultAsync(o => o.AccountId == id);
        if (owner == null)
        {
            throw new Exception("Owner ERROR NULL");
        }
        return owner;
    }
}