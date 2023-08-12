using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class FeedBackRepository : GenericRepository<Feedback>, IFeedBackRepository
{
    public FeedBackRepository(FootBall_PitchContext context) : base(context)
    {
    }
}