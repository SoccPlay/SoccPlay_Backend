using Application.Model.Request.RequestBooking;
using Application.Model.Response.ResponseBooking;

namespace Application.Service;

public interface BookingService
{

    Task<List<ResponseBooking>> GetAllBooking();
    Task<bool> CancelBooking_v2(Guid BookingId);
    public Task<List<ResponseManageBooking>> GetByCustomerId(Guid customerId);
    public Task<ResponseBooking_v2> BookingPitch_v3(RequestBooking_v3 requestBooking);
}