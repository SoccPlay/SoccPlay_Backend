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
}