using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;
using Domain.Entities;

namespace Application.Service;

public interface AdminService
{
    Task<ResponseAccountAdmin> CreateAdmin(RequestAccountAdmin request);
}