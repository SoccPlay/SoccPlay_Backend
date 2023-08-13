using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service;

public interface BookingService
{
    Task<ResponseBooking> BookingPitch(RequestBooking requestBooking);

    Task<List<ResponseBooking>> GetAllBooking();

    Task<bool> CancleBooking(Guid BookingId);
}