using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IPitchImageRepository   : IGenericRepository<PitchImage>
{
    Task<List<string>> GetAllImageByLandId(Guid LandId);
    Task<string> GetImageByLandId(Guid LandId);
    
}