using Application.Model.Request.RequestLand;
using Application.Model.ResponseLand;

namespace Application.Service;

public interface LandService
{
    Task<ResponseLand> CreateLand(RequestLand requestLand);
    Task<List<ResponseLand>> GetAllLands();
    Task<ResponseLand> LandDetail(Guid landId);
}