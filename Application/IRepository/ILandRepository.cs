using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface ILandRepository  : IGenericRepository<Land>
{
    Task<Land> GetLandByIdLand(Guid landId);
}