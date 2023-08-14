using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface ILandRepository  : IGenericRepository<Land>
{
    Task<Land> GetLandByIdLand(Guid landId);
    Task<List<Land>> SearchLand(string location, string name);
    Task<List<Land>> SearchLandByLocation(string location);
    Task<List<Land>> SearchLandByName(string name);
}