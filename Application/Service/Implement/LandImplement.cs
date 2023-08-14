using System.Globalization;
using Application.IRepository;
using AutoMapper;
using Application.IRepository.IUnitOfWork;
using Application.Service;
using Application.Model.Request.RequestLand;
using Application.Model.Respone;
using Application.Model.ResponseLand;
using Domain.Entities;

namespace Application.Implement;

public class LandImplement : LandService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPitchImageRepository _imageRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly ILandRepository _landRepository;

    public LandImplement(IUnitOfWork unitOfWork, IMapper mapper, IPitchImageRepository imageRepository,
        IPriceRepository priceRepository, ILandRepository landRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageRepository = imageRepository;
        _priceRepository = priceRepository;
        _landRepository = landRepository;
    }

    public Task<ResponseLand> CreateLand(RequestLand requestLand)
    {
        var land = _mapper.Map<Land>(requestLand);
        land.Owner = _unitOfWork.Owner.GetById(requestLand.OwnerId);
        _unitOfWork.Land.Add(land);
        _unitOfWork.Save();
        return Task.FromResult(_mapper.Map<ResponseLand>(land));
    }

    public async Task<List<ResponseLand>> GetAllLands()
    {
        var landEntities = _unitOfWork.Land.GetAll(); // Assuming a method like GetAllAsync() exists in your repository
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);

        foreach (var land in responseLands)
        {
            var prices = await _priceRepository.GetPriceByLandId(land.LandId);
            land.MinPrice = prices.Min(p => p.Price1);
            land.MinPrice = prices.Max(p => p.Price1);
        }

        foreach (var land in responseLands)
        {
            var image = await _imageRepository.GetImageByLandId(land.LandId);

            land.image = image;
        }

        return responseLands;
    }

    public async Task<ResponseLand> LandDetail(Guid landId)
    {
        var land = _unitOfWork.Land.GetById(landId);
        var responseLand = _mapper.Map<ResponseLand>(land);
        var image = await _imageRepository.GetAllImageByLandId(responseLand.LandId);

        responseLand.PitchImages = image;

        return responseLand;
    }

    public async Task<List<ResponseLand>> SearchLand(string location, string landName)
    {
        var landEntities = await _landRepository.SearchLand(location, landName);

        if (landEntities == null)
        {
            throw new CultureNotFoundException("NotFound");
        }

        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);

        foreach (var land in responseLands)
        {
            var prices = await _priceRepository.GetPriceByLandId(land.LandId);
            land.MinPrice = prices.Min(p => p.Price1);
            land.MinPrice = prices.Max(p => p.Price1);
        }

        foreach (var land in responseLands)
        {
            var image = await _imageRepository.GetImageByLandId(land.LandId);

            land.image = image;
        }

        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByLocation(string location)
    {
        var landEntities = await _landRepository.SearchLandByLocation(location);

        if (landEntities == null)
        {
            throw new CultureNotFoundException("NotFound");
        }

        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);

        foreach (var land in responseLands)
        {
            var prices = await _priceRepository.GetPriceByLandId(land.LandId);
            land.MinPrice = prices.Min(p => p.Price1);
            land.MinPrice = prices.Max(p => p.Price1);
        }

        foreach (var land in responseLands)
        {
            var image = await _imageRepository.GetImageByLandId(land.LandId);

            land.image = image;
        }

        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByName(string landName)
    {
        var landEntities =
            await _landRepository
                .SearchLandByName(landName); // Assuming a method like GetAllAsync() exists in your repository

        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);

        foreach (var land in responseLands)
        {
            var prices = await _priceRepository.GetPriceByLandId(land.LandId);
            land.MinPrice = prices.Min(p => p.Price1);
            land.MinPrice = prices.Max(p => p.Price1);
        }

        foreach (var land in responseLands)
        {
            var image = await _imageRepository.GetImageByLandId(land.LandId);
            land.image = image;
        }

        return responseLands;
    }
}