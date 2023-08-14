using Application.Model.Request.RequestPitch;
using Application.Model.Respone.ResponsePitch;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PitchController : ControllerBase
{
    private readonly PitchService _pitchService;

    public PitchController(PitchService pitchService)
    {
        _pitchService = pitchService;
    }
    
    [HttpPost]
    public async Task<ActionResult<ResponsePitch>> CreateLand(RequestPitch request)
    {
        try
        {
            var create = await _pitchService.CreatePitch(request);
            return Ok(create);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}