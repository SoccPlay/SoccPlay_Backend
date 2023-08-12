using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
    public AdminRepository(FootBall_PitchContext context) : base(context)
    {
    }

}