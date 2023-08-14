using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
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
    
    [HttpGet]
    public async Task<ActionResult<List<ResponseBooking>>> GetAllBookings()
    {
        try
        {
            var bookings = await _bookingService.GetAllBooking();
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<ResponseBooking>> CreateBooking(RequestBooking request)
    {
        try
        {
            var create = await _bookingService.BookingPitch(request);
            return Ok(create);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete]
    public async Task<bool> CancleBooking(Guid id)
    {
            return await _bookingService.CancleBooking(id);
    }

    
}