using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPitchRepository   : IGenericRepository<Pitch>
{
    Task<List<Pitch>> GetAllPitchByLand(Guid LandId);
}