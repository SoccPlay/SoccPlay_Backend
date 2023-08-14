using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class LandRepository : GenericRepository<Land>, ILandRepository
{
    public LandRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Land> GetLandByIdLand(Guid landId)
    {
        var owner = await _context.Set<Land>()!.FirstOrDefaultAsync(o => o.OwnerId == landId);
        if (owner == null)
        {
            throw new Exception("LAND ERROR NULL");
        }

        return owner;
    }
}