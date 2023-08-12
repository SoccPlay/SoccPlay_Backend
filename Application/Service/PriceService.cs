using Application.Model.Request.RequestPrice;
using Application.Model.Respone.ResponsePrice;

namespace Application.Service;

public interface PriceService
{
    Task<ResponsePrice> CreatePrice(RequestPrice requestPrice);

    Task<List<ResponsePrice>> GetPriceByLand(Guid LandId);
}