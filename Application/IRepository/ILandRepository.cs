using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface ILandRepository : IGenericRepository<Land>
{
    Task<Land> GetLandByIdLand(Guid landId);

    Task<List<Land>> GetAllLand();

    Task<List<Land>> GetLandByOwnerId(Guid ownerId);

    Task<List<Guid>> GetPitchByOwnerId(Guid ownerId);
    
    Task<List<Land>> GetTop6();

    Task<List<Land>> SearchLand(string location, string name);
    Task<List<Land>> SearchLandByLocation(string location);
    Task<List<Land>> SearchLandByName(string name);
    
    Task<List<Land>> FilterLand(string location, int rate, float min, float max, int size);
}