using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class PitchImageRepository : GenericRepository<PitchImage>, IPitchImageRepository
{
    public PitchImageRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<string>> GetAllImageByLandId(Guid LandId)
    {
        var list = _context.Set<PitchImage>().Where(image => image.LandId == LandId).Select(image => image.Name).ToList();
        return list;
    }

    public async Task<string> GetImageByLandId(Guid LandId)
    {
        var list = _context.Set<PitchImage>().FirstOrDefault(image => image.LandId == LandId);
        return list.Name;
    }
}