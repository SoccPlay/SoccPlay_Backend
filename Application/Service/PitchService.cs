using Application.Model.Request.RequestPitch;
using Application.Model.Respone.ResponsePitch;

namespace Application.Service;

public interface PitchService
{
    Task<ResponsePitch> CreatePitch(RequestPitch requestPitch);
}