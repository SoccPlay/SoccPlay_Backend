using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPitchRepository : IGenericRepository<Pitch>
{
    Task<List<Pitch>> GetAllPitchByLand(Guid LandId);

    Task<List<Pitch>> GetAllPitchByLandAndDate(Guid landId, DateTime date, int size);

    Task<Pitch> GetPitchToBooking(Guid landId, DateTime startTime, DateTime endTime, int size);
}