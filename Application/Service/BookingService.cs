using Application.Model.Request.RequestBooking;
using Application.Model.Response.ResponseBooking;
using Application.Model.ResponseLand;

namespace Application.Service;

public interface BookingService
{

    Task<List<ResponseBooking>> GetAllBooking();
    Task<bool> CancelBooking_v2(Guid BookingId);
    public Task<List<ResponseManageBooking>> GetByCustomerId(Guid customerId);
    public Task<List<ResponseAllLandBooking>> GetByOwner(Guid ownerId);
    public Task<ResponseBooking_v2> BookingPitch_v3(RequestBooking_v3 requestBooking);
    public Task<List<ResponseBooking_v2>> GetBookingByLandId(Guid id);
    public Task<bool> ChangeStatus(Guid id, string status);
    public Task<List<ResponseAllLandBooking_v2>> GetByOwner_v2(Guid ownerId);
    public Task<ResponeBooking_v3> GetByBookingId(Guid id);
}