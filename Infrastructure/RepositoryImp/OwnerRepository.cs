using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    public OwnerRepository(FootBall_PitchContext context) : base(context)
    {
    }
}