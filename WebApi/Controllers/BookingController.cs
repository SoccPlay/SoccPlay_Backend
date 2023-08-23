using Application.Model.Request.RequestBooking;
using Application.Model.Response.ResponseBooking;
using Application.Model.ResponseLand;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly LandService _landService;

    public BookingController(BookingService bookingService, LandService landService)
    {
        _bookingService = bookingService;
        _landService = landService;
    }

    
    [HttpPost]
    public async Task<ActionResult<ResponseBooking>> CreateBooking_v3(RequestBooking_v3 request)
    {
        var create = await _bookingService.BookingPitch_v3(request);
        return Ok(create);
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponseBooking>>> GetAllBookings()
    {
        var bookings = await _bookingService.GetAllBooking();
        return Ok(bookings);
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponseManageBooking>>> GetAllBookingByCustomerId(Guid id)
    {
        var bookings = await _bookingService.GetByCustomerId(id);
        return Ok(bookings);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ResponseAllLandBooking>>> GetAllBookingByOwnerId(Guid id)
    {
        var bookings = await _bookingService.GetByOwner(id);
        return Ok(bookings);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ResponseAllLandBooking_v2>>> GetAllBookingByOwnerId_v2(Guid id)
    {
        var bookings = await _bookingService.GetByOwner_v2(id);
        return Ok(bookings);
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponseBooking_v2>>> GetAllBookingByLandId(Guid id)
    {
        var bookings = await _bookingService.GetBookingByLandId(id);
        return Ok(bookings);
    }


    [HttpDelete]
    public async Task<bool> CancelBooking(Guid id)
    {
        return await _bookingService.CancelBooking_v2(id);
    }
    
    [HttpDelete]
    public async Task<bool> ChangeStatusBooking(Guid id, string status)
    {
        return await _bookingService.ChangeStatus(id, status);
    }
}