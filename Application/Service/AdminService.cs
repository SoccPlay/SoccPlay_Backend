using Application.Model.Request.RequestAccount;
using Application.Model.Response.ResponseAccount;

namespace Application.Service;

public interface AdminService
{
    Task<ResponseAccountAdmin> CreateAdmin(RequestAccountAdmin request);
}