using Application.Model.Request.RequestPrice;
using Application.Model.Response.ResponsePrice;

namespace Application.Service;

public interface PriceService
{
    Task<ResponsePrice> CreatePrice(RequestPrice requestPrice);

    Task<List<ResponsePrice>> GetPriceByLand(Guid LandId);
}