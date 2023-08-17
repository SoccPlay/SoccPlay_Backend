using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IFeedBackRepository  : IGenericRepository<Feedback>
{
    Task<List<Feedback>> GetByFeedBackLandId(Guid landId);
}