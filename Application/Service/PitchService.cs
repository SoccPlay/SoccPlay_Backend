using Application.Model.Request.RequestPitch;
using Application.Model.Response.ResponsePitch;

namespace Application.Service;

public interface PitchService
{
    Task<ResponsePitch> CreatePitch(RequestPitch requestPitch);
    Task<List<ResponsePitchV2>> GetScheduleListByDate(Guid landId, string date, int size);
    Task<ResponsePitchV2> GetScheduleList(Guid landId, string date, int size, string name);
    Task<List<ICollection<ResponsePitch>>> GetAllPitchOfOwner(Guid ownerId);
    Task<List<ResponsePitch>> GetAllPitchByNameLandAndOwnerId(Guid ownerId,Guid landId);


}