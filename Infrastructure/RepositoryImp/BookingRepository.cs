using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;

namespace Infrastructure.RepositoryImp;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(FootBall_PitchContext context) : base(context)
    {
    }
}