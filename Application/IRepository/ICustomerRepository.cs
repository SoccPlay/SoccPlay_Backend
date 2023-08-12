using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    
}