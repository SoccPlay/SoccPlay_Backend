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
    
    [HttpPost]
    public async Task<ActionResult<ResponseBooking>> CreateBooking_v2(RequestBookingV2 request)
    {
        try
        {
            var create = await _bookingService.BookingPitch_v2(request);
            return Ok(create);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
    
    [HttpGet]
    public async Task<ActionResult<List<ResponseBooking>>> GetAllBookingByCustomerId(Guid id)
    {
        try
        {
            var bookings = await _bookingService.GetByCustomerId(id);
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    
    [HttpDelete]
    public async Task<bool> CancleBooking(Guid id)
    {
            return await _bookingService.CancleBooking(id);
    }

    
}