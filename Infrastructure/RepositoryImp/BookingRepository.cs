using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using Application.IRepository;
using Application.Model.Response.ResponseBooking;
using Domain.Entities;
using Domain.Enum;
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
        var bookings = _context.Set<Booking>().Include(s => s.Schedules).ThenInclude(s => s.PitchPitch).Include(l => l.Land)
            .Where(b => b.CustomerId == customerId).ToList();
        if (bookings == null)
        {
            throw new CultureNotFoundException("Not Found");
        }
        return bookings;
    }

    public async Task<List<Booking>> GetAllBooking()
    {
        var booking = await _context.Set<Booking>().Include(b => b.Schedules).Include(c => c.Customer)
            .Include(b => b.Land).ToListAsync();
        return booking;
    }

    public async Task<Booking> GetBookingById(Guid bookingId)
    {
        var booking = await _context.Set<Booking>()
            .Include(b => b.Land)
            .ThenInclude(o=>o.Owner)
            .Include(b => b.Customer)
            .Include(b => b.Schedules)
            .ThenInclude(i=>i.PitchPitch.Land.Images)
            .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        if (booking == null) throw new Exception("Booking Not Exist");
        return booking;
    }

    public async Task<bool> GetBookingByCustomerId(Guid id)
    {
        var booking = await _context.Set<Booking>().AnyAsync(b => b.CustomerId == id && b.Status == BookingStatus.Done.ToString());
        return booking;
    }

    public async Task<List<Booking>> GetBookingByLandId(Guid id)
    {
        var bookings = await _context.Bookings.Include(s => s.Schedules).Where(b => b.LandId == id).ToListAsync();
        return bookings;
    }
    
    public async Task<List<BookingSummary>> GetBookingSummariesForYear(int year, Guid ownerId)
    {
        var summaries = await _context.Bookings
            .Where(b => b.DateBooking.Year == year && b.Land.OwnerId == ownerId && b.Status == BookingStatus.Done.ToString())
            .GroupBy(b => b.DateBooking.Month)
            .OrderBy(group => group.Key)
            .Select(group => new BookingSummary
            {
                BookingMonth = group.Key,
                TotalPriceSum = group.Sum(b => b.TotalPrice)
            })
            .ToListAsync();
        return summaries;
    }
    public async Task<float> GetSummary(Guid ownerId)
    {
        var summary = await _context.Bookings.Where(b => b.Land.Owner.OwnerId == ownerId && b.Status == BookingStatus.Done.ToString()).Select(b => b.TotalPrice).ToListAsync();
        return summary.Sum();
    }
    
    public async Task<int> GetNumBooking(Guid ownerId)
    {
        var count = await _context.Bookings.Where(b => b.Land.Owner.OwnerId == ownerId).ToListAsync();
        return count.Count;
    }

    public async Task<List<BookingSummary>> GetBookingSummariesForYearByLand(int year, Guid landId)
    { 
        var summaries = await _context.Bookings
            .Where(b => b.DateBooking.Year == year && b.LandId == landId && b.Status == BookingStatus.Done.ToString())
            .GroupBy(b => b.DateBooking.Month)
            .OrderBy(group => group.Key)
            .Select(group => new BookingSummary
            {
                BookingMonth = group.Key,
                TotalPriceSum = group.Sum(b => b.TotalPrice)
            })
            .ToListAsync();
        return summaries;
    }

    public async Task<float> GetSummaryByLand(Guid landId)
    {
        var summary = await _context.Bookings.Where(b => b.LandId == landId && b.Status == BookingStatus.Done.ToString()).Select(b => b.TotalPrice).ToListAsync();
        return summary.Sum();
    }

    public async Task<int> GetNumBookingByLand(Guid landId)
    {
        var count = await _context.Bookings.Where(b => b.LandId == landId).ToListAsync();
        return count.Count;
    }
}