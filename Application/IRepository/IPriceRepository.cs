using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPriceRepository : IGenericRepository<Price>
{
    Task<Price?> GetBySizeAndLand(Guid LandId, int Size, DateTime startTime);

    Task<List<Price>> GetPriceByLandId(Guid LandId);
    Task<Price?> InActive(Guid landId, int startTime, int endTime, int size);
}