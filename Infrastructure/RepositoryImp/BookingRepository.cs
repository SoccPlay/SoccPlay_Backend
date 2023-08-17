using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class BookingRepository : GenericRepository<Booking>, IBookingRepository
{
    public BookingRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Booking>> GetAllBookingByCustomerId(Guid customerId)
    {
        var bookings = _context.Set<Booking>().Include( s => s.Schedules).Include(l => l.Land).Where(b => b.CustomerId == customerId).ToList();
        return bookings;
    }

    public async Task<List<Booking>> GetAllBooking()
    {
        var booking = await _context.Set<Booking>().Include(b => b.Schedules).Include(c => c.Customer).Include(b => b.Land).ToListAsync();
        return booking;
    }
}