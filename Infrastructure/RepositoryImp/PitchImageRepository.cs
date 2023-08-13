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

    public Task<List<string>> GetAllImageByLandId(Guid LandId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetImageByLandId(Guid LandId)
    {
        var list = _context.Set<PitchImage>().FirstOrDefault(image => image.LandId == LandId);
        return list.Name;
    }
}