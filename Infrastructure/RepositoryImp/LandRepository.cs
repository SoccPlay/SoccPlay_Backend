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
        var land = await _context.Set<Land>()!.Include(a=>a.Prices).Include(c=>c.Images).FirstOrDefaultAsync(o => o.LandId == landId);
        return land;
    }

    public async Task<List<Land>> GetAllLand()
    {
        return await _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).ToListAsync();
    }

    public async Task<List<Land>> SearchLand(string location, string name)
    {
        var lands = _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).Where(land => land.NameLand == name && land.Location == location).ToList();
        return lands;
    }

    public async Task<List<Land>> SearchLandByLocation(string location)
    {
        var lands = _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).Where(land => land.Location == location).ToList();
        return lands;
    }

    public async Task<List<Land>> SearchLandByName(string name)
    {
        var lands = _context.Set<Land>().Include(a=>a.Prices).Include(c=>c.Images).Where(land => land.NameLand.Contains(name)).ToList();
        return lands;
    }
}