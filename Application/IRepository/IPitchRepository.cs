using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPitchRepository : IGenericRepository<Pitch>
{
    Task<List<Pitch>> GetAllPitchByLand(Guid LandId);

    Task<List<ICollection<Pitch>>> Get(Guid ownerId);

    Task<List<Pitch>> GetAllPitchByLandAndDate(Guid landId, DateTime date, int size);
    
    Task<Pitch> GetPitchByLandAndDate(Guid landId, DateTime date, int size, string name);

    Task<Pitch> GetPitchToBooking(Guid landId, DateTime startTime, DateTime endTime, int size);
    
}