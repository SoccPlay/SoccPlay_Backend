using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class PriceRepository : GenericRepository<Price>, IPriceRepository
{
    public PriceRepository(FootBall_PitchContext context) : base(context)
    {
    }
}