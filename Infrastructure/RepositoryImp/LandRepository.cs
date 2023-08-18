using System.Net.Mime;
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
        var land = await _context.Set<Land>()!.Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).FirstOrDefaultAsync(land => land.LandId == landId  && land.TotalPitch != 0 );
        return land;
    }

    public async Task<List<Land>> GetAllLand()
    {
        return await _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).Include(f => f.Feedbacks).Where(land => land.TotalPitch != 0).ToListAsync();
    }

    public  async Task<List<Land>> GetTop6()
    {
        var topLands = await (
                from feedback in _context.Feedbacks
                group feedback by feedback.LandId into grouped
                orderby grouped.Average(f => f.Rate) descending
                select grouped.Key
            )
            .Take(6)
            .Join(
                _context.Set<Land>()
                    .Include(a => a.Prices)
                    .Include(f => f.Feedbacks)
                    .Include(c => c.Images),
                topLandId => topLandId,
                land => land.LandId,
                (topLandId, land) => land
            )
            .ToListAsync();

        return topLands;
    }

    public async Task<List<Land>> SearchLand(string location, string name)
    {
        var lands = await _context.Set<Land>().Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).Where(land => land.Location.ToLower().Contains(location.ToLower()) && land.TotalPitch != 0 && land.NameLand.Contains(name)).ToListAsync();
        return lands;
    }

    public async Task<List<Land>> SearchLandByLocation(string location)
    {
        var lands = await _context.Set<Land>().Include(a => a.Prices).Include(f => f.Feedbacks).Include(c => c.Images)
            .Where(land => land.Location.ToLower().Contains(location.ToLower()) && land.TotalPitch != 0 ).ToListAsync();
        return lands;
    }

    public async Task<List<Land>> SearchLandByName(string name)
    {
        var lands = await _context.Set<Land>().Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).Where(land => land.NameLand.Contains(name) && land.TotalPitch != 0 ).ToListAsync();
        return lands;
    }
}