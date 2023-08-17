using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<Customer> GetCustomerByAccountId(Guid id)
    {
        var customer = await _context.Set<Customer>().Include(account => account.Account).FirstOrDefaultAsync(customer => customer.AccountId == id);
        return customer;
    }
}