using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IOwnerRepository   : IGenericRepository<Owner>
{
    Task<Owner> GetOwnerById(Guid ownerId);
    
    Task<Owner> GetOwnerByAccountId(Guid id);
}