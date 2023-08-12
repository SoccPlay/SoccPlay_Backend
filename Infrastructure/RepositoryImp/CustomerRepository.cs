using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(FootBall_PitchContext context) : base(context)
    {
    }
}