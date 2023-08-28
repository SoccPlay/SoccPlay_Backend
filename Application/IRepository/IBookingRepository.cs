using Application.IRepository.IGeneric;
using Application.Model.Response.ResponseBooking;
using Domain.Entities;

namespace Application.IRepository;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<List<Booking>> GetAllBookingByCustomerId(Guid customerId);
    Task<List<Booking>> GetAllBooking();
    Task<Booking> GetBookingById(Guid bookingId);
    Task<bool> GetBookingByCustomerId(Guid id);
    Task<List<Booking>> GetBookingByLandId(Guid id);
    Task<List<BookingSummary>> GetBookingSummariesForYear(int year, Guid ownerId);
    Task<float> GetSummary(Guid ownerId);
    Task<int> GetNumBooking(Guid ownerId);

}