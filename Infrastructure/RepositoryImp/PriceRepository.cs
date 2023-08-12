using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class PriceRepository : GenericRepository<Price>, IPriceRepository
{
    public PriceRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Guid> GetBySizeAndLand(Guid LandId, int Size)
    {
        var price = _context.Set<Price>().First(p => p.Size == Size && p.LandLandId == LandId);
        return price.PriceId;
    }

    public async Task<List<Price>> GetPriceByLandId(Guid LandId)
    {
        var price = await _context.Set<Price>().Where(p => p.LandLandId == LandId).ToListAsync();
        return price;
    }
}