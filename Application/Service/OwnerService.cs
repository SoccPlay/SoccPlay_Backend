using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;

namespace Application.Service;

public interface OwnerService
{
    Task<ResponseAccountOwner> CreateOwner(RequestAccountOwner request);
}