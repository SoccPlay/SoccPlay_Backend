using Application.Model.Request.RequestPrice;
using Application.Model.Response.ResponsePrice;
using Domain.Entities;

namespace Application.Service;

public interface PriceService
{
    Task<ResponsePrice> CreatePrice(RequestPrice requestPrice);

    Task<List<ResponsePrice>> GetPriceByLand(Guid LandId);

    Task<Boolean> RemovePrice(Guid id);

    Task<float> Calculator(RequestCaculator requestCaculator);
}