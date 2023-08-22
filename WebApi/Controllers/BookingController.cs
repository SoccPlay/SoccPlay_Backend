using Application.Model.Request.RequestBooking;
using Application.Model.Response.ResponseBooking;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
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


    [HttpDelete]
    public async Task<bool> CancelBooking(Guid id)
    {
        return await _bookingService.CancelBooking_v2(id);
    }
}