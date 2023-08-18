using Application.Model.Request.RequestPitch;
using Application.Model.Response.ResponsePitch;

namespace Application.Service;

public interface PitchService
{
    Task<ResponsePitch> CreatePitch(RequestPitch requestPitch);
    Task<List<ResponsePitchV2>> GetScheduleListByDate(Guid landId, string date, int size);
}