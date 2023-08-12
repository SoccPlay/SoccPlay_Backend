using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

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
}