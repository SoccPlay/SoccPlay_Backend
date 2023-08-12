using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class LandRepository : GenericRepository<Land>, ILandRepository
{
    public LandRepository(FootBall_PitchContext context) : base(context)
    {
    }
}