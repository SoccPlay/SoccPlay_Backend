using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class PitchRepository : GenericRepository<Pitch>, IPitchRepository
{
    public PitchRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Pitch>> GetAllPitchByLand(Guid LandId)
    {
        var list = await _context.Pitches.Where(pitch => pitch.LandId == LandId).ToListAsync();
        return list;
    }
}