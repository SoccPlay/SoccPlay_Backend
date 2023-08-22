using Application.Model.Request.RequestLand;
using Application.Model.ResponseLand;

namespace Application.Service;

public interface LandService
{
    Task<ResponseLand_2> CreateLand(RequestLand requestLand);
    Task<List<ResponseLand>> GetAllLands();
    Task<ResponseLand_v3> LandDetail(Guid landId);
    Task<List<ResponseLand>> LandByOwnerId(Guid ownerId);
    Task<List<ResponseLand>> Top6Land();
    Task<List<ResponseLand>> SearchLand(string location, string landName);
    Task<List<ResponseLand>> SearchLandByLocation(string landName);
    Task<List<ResponseLand>> SearchLandByName(string landName);
    Task<List<ResponseLand>> FilterLand(RequestFilter requestFilter);
}