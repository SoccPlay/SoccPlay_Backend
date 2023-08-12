using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPriceRepository   : IGenericRepository<Price>
{
    Task<Guid> GetBySizeAndLand(Guid LandId, int Size);

    Task<List<Price>> GetPriceByLandId(Guid LandId);

}