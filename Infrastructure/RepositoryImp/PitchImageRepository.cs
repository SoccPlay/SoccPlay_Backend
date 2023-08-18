using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class PitchImageRepository : GenericRepository<PitchImage>, IPitchImageRepository
{
    public PitchImageRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<string>> GetAllImageByLandId(Guid LandId)
    {
        var list = _context.Set<PitchImage>().Where(image => image.LandId == LandId).Select(image => image.Name)
            .ToList();
        if (list == null) return null;
        return list;
    }

    public async Task<string> GetImageByLandId(Guid LandId)
    {
        var list = _context.Set<PitchImage>().Where(land => land.LandId == LandId)
            .OrderByDescending(item => item.LandId).FirstOrDefault();
        if (list == null) return null;
        return list.Name;
    }
}