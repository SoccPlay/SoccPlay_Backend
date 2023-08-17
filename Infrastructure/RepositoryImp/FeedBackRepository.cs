using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class FeedBackRepository : GenericRepository<Feedback>, IFeedBackRepository
{
    public FeedBackRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Feedback>> GetByFeedBackLandId(Guid landId)
    {
        var feedBacks = await _context.Feedbacks.Where(feedback => feedback.LandId == landId).ToListAsync();
        return feedBacks;
    }
}