using Application.Model.Request.RequestPitch;
using Application.Model.Response.ResponsePitch;
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
    public async Task<ActionResult<ResponsePitch>> CreatePitch(RequestPitch request)
    {
        var create = await _pitchService.CreatePitch(request);
        return Ok(create);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ResponsePitch>>> GetPitchsByOwnerId(Guid Id)
    {
        var create = await _pitchService.GetAllPitchOfOwner(Id);
        return Ok(create);
    }


    [HttpGet]
    public async Task<ActionResult<ResponsePitchV2>> GetPitchSchedule(Guid landId, string date, int size)
    {
        var create = await _pitchService.GetScheduleListByDate(landId, date, size);
        return Ok(create);
    }
    
    
    [HttpGet]
    public async Task<ActionResult<ResponsePitchV2>> GetPitchSchedule_v2(Guid landId, string date, int size, string pitchName)
    {
        var create = await _pitchService.GetScheduleList(landId, date, size, pitchName);
        return Ok(create);
    }

    [HttpGet]
    public async Task<ActionResult<ResponsePitchV2>> GetPitchByOwnerAndNameLand(Guid ownerId, Guid landId)
    {
        var create = await _pitchService.GetAllPitchByNameLandAndOwnerId(ownerId, landId);
        return Ok(create);
    }
    
    [HttpGet]
    public async Task<ActionResult<int[]>> GetNumPitch(Guid ownerId)
    {
        var create = await _pitchService.GetNumPitch(ownerId);
        return Ok(create);
    }
    
    [HttpGet]
    public async Task<ActionResult<int[]>> GetNumPitchByLand(Guid landId)
    {
        var create = await _pitchService.GetNumPitchByLand(landId);
        return Ok(create);
    }
    
    [HttpPut]
    public async Task<ActionResult<ResponsePitch>> InActivePitch(Guid id, string status)
    {
        var pitch = await _pitchService.ChangePitchStatus(id, status);
        return Ok(pitch);
    }
}