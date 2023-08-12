using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class PitchRepository : GenericRepository<Pitch>, IPitchRepository
{
    public PitchRepository(FootBall_PitchContext context) : base(context)
    {
    }
}