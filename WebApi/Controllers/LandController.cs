using Application.Model.Request.RequestLand;
using Application.Model.ResponseLand;
using Application.Service;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LandController : ControllerBase
{
    private readonly LandService _landService;

    public LandController(LandService landService)
    {
        _landService = landService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ResponseLand>> CreateLand(RequestLand requestLand)
    {
        try
        {
            var create = await _landService.CreateLand(requestLand);
            return Ok(create);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /*[HttpGet]
    public async Task<ActionResult<List<ResponseLand>>> GetAllLand()
    {
        try
        {
            var lands = await _landService.GetAllLands();
            return Ok(lands);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }*/

    [HttpGet]
    public async Task<ActionResult<ResponseLand>> GetLandById(Guid landId)
    {
        try
        {
            var land = await _landService.LandDetail(landId);
            return Ok(land);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    
}