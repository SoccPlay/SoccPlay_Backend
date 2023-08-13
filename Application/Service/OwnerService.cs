using Application.Model.Request.RequestAccount;
using Application.Model.Respone.ResponseAccount;
using Microsoft.AspNetCore.Mvc;

namespace Application.Service;

public interface OwnerService
{
    Task<ResponseAccountOwner> CreateOwner(RequestAccountOwner request);
}