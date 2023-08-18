using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IAdminRepository : IGenericRepository<Admin>
{
    Task<Admin> GetAdminByAccountId(Guid id);
}