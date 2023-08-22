using Application.Model.Request.RequestPrice;
using Application.Model.Response.ResponsePrice;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PriceController : ControllerBase
{
    private readonly PriceService _priceService;

    public PriceController(PriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpPost]
    public async Task<ActionResult<ResponsePrice>> CreatePrice(RequestPrice requestPrice)
    {
        try
        {
            var create = await _priceService.CreatePrice(requestPrice);
            return Ok(create);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<float>> Calculator(RequestCaculator request)
    {
            var create = await _priceService.Calculator(request);
            return Ok(create);
    }
}