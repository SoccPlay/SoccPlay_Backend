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
        var land = await _context.Set<Land>()!.Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).FirstOrDefaultAsync(o => o.LandId == landId);
        return land;
    }

    public async Task<List<Land>> GetAllLand()
    {
        return await _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).Include(f => f.Feedbacks).ToListAsync();
    }

    public  async Task<List<Land>> GetTop6()
    {
        var topLand = await (
            from feedback in _context.Feedbacks
            group feedback by feedback.LandId into grouped
            orderby grouped.Average(f => f.Rate) descending
            select new { LandId = grouped.Key}
        ).Take(6).ToListAsync();

        
        
        List<Land> lands = new List<Land>();
        foreach (var l in topLand)
        {
            lands.Add(await GetLandByIdLand(l.LandId));
        }
        return lands;
    }

    public async Task<List<Land>> SearchLand(string location, string name)
    {
        var lands = await _context.Set<Land>().Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).Where(land => land.Location.ToLower().Contains(location.ToLower()) && land.NameLand.Contains(name)).ToListAsync();
        return lands;
    }

    public async Task<List<Land>> SearchLandByLocation(string location)
    {
        var lands = await _context.Set<Land>().Include(a => a.Prices).Include(f => f.Feedbacks).Include(c => c.Images)
            .Where(land => land.Location.ToLower().Contains(location.ToLower())).ToListAsync();
        return lands;
    }

    public async Task<List<Land>> SearchLandByName(string name)
    {
        var lands = await _context.Set<Land>().Include(a=>a.Prices).Include(f => f.Feedbacks).Include(c=>c.Images).Where(land => land.NameLand.Contains(name)).ToListAsync();
        return lands;
    }
}