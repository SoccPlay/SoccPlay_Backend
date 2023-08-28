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

    public async Task<Price?> GetBySizeAndLand(Guid LandId, int Size, DateTime startTime)
    {
        var time = startTime.Hour;
        var price = _context.Set<Price>().FirstOrDefault(p =>
            p.Size == Size && p.LandLandId == LandId && time >= p.StarTime && time <= p.EndTime);
        return price;
    }

    public async Task<List<Price>> GetPriceByLandId(Guid LandId)
    {
        var price = await _context.Set<Price>().Where(p => p.LandLandId == LandId).ToListAsync();
        return price;
    }

    public async Task<Price?> InActive(Guid landId, int startTime, int endTime)
    {
        var price = await _context.Prices.FirstOrDefaultAsync(p => p.LandLandId == landId && p.EndTime == endTime && p.StarTime == startTime);
        return price;
    }
}