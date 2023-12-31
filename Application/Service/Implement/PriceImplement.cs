﻿using System.Globalization;
using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestPrice;
using Application.Model.Response.ResponsePrice;
using AutoMapper;
using Domain.Entities;

namespace Application.Service.Implement;

public class PriceImplement : PriceService
{
    private readonly IMapper _mapper;
    private readonly IPriceRepository _priceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PriceImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
    }

    public async Task<ResponsePrice> CreatePrice(RequestPrice requestPrice)
    {
        var price = _mapper.Map<Price>(requestPrice);
        price.Date= DateTime.Now;
        var oldPrice = await _unitOfWork.Price.InActive(requestPrice.LandLandId, requestPrice.StarTime, requestPrice.EndTime, requestPrice.Size);
        if (oldPrice != null)
        {
            _unitOfWork.Price.Remove(oldPrice);
        }
        _unitOfWork.Price.Add(price);
        _unitOfWork.Save();
        return _mapper.Map<ResponsePrice>(price);
    }

    public async Task<List<ResponsePrice>> GetPriceByLand(Guid LandId)
    {
        var list = _priceRepository.GetPriceByLandId(LandId);
        return _mapper.Map<List<ResponsePrice>>(list);
    }

    public async Task<bool> RemovePrice(Guid id)
    {
        _unitOfWork.Price.Remove(_unitOfWork.Price.GetById(id));
        return true;
    }

    public async Task<float> Calculator(RequestCaculator requestCaculator)
    {
        var checkDate = requestCaculator.EndTime.Hour - requestCaculator.StarTime.Hour;
        var checkDate2 = requestCaculator.StarTime.Hour < DateTime.Now.Hour;
        if (checkDate > 3 || checkDate < 0)
        {
            throw new CultureNotFoundException("Thời gian đặt sân không quá 3 giờ !");
        }

        if (checkDate2 == true)
        {
            throw new CultureNotFoundException("Thời gian đặt sân lỗi!");
        }
        
        var land =  await _unitOfWork.Land.GetLandByIdLand(requestCaculator.LandId);
        var price = land.Prices.FirstOrDefault(p =>
            p.Size == requestCaculator.Size && requestCaculator.StarTime.Hour >= p.StarTime &&
            requestCaculator.StarTime.Hour <= p.EndTime)!.Price1;

        var hours = requestCaculator.EndTime - requestCaculator.StarTime;
        int minutes = hours.Minutes;
        float totalHours = (float)hours.Hours;
        float totalMin = (float)minutes / 60;
        float total = (float)(totalHours + totalMin) * price;
        return total;
    }
}