using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestPrice;
using Application.Model.Respone.ResponsePrice;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Implement;

public class PriceImplement : PriceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPriceRepository _priceRepository;

    public PriceImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
    }
    public async Task<ResponsePrice> CreatePrice(RequestPrice requestPrice)
    {
        var price = _mapper.Map<Price>(requestPrice);
        _unitOfWork.Price.Add(price);
        _unitOfWork.Save();
        return _mapper.Map<ResponsePrice>(price);
    }

    public async Task<List<ResponsePrice>> GetPriceByLand(Guid LandId)
    {
        var list = _priceRepository.GetPriceByLandId(LandId);
        return _mapper.Map<List<ResponsePrice>>(list);
    }
}