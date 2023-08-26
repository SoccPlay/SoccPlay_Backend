using System.Globalization;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestLand;
using Application.Model.Response.ResponsePrice;
using Application.Model.ResponseLand;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Implement;

public class LandImplement : LandService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LandImplement(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseLand_2> CreateLand(RequestLand requestLand)
    {
        var land = _mapper.Map<Land>(requestLand);
        land.Date = DateTime.Now;
        if (_unitOfWork.Owner.GetById(land.OwnerId) == null) throw new Exception("Not Found Owner");
        _unitOfWork.Land.Add(land);
        _unitOfWork.Save();
        return _mapper.Map<ResponseLand_2>(land);
    }

    public async Task<List<ResponseLand>> GetAllLands()
    {
        var landEntities = await _unitOfWork.Land.GetAllLand();
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<ResponseLand_v3> LandDetail(Guid landId)
    {
        var land = await _unitOfWork.Land.GetLandByIdLand(landId);
        if (land == null) throw new CultureNotFoundException("NotFound");
        var responseLands = _mapper.Map<ResponseLand_v3>(land);
        responseLands.Price = _mapper.Map<List<ResponsePrice>>(land.Prices);
        return responseLands;
    }

    public async Task<List<ResponseLand>> LandByOwnerId(Guid ownerId)
    {
        var landEntities = await _unitOfWork.Land.GetLandByOwnerId(ownerId);
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> Top6Land()
    {
        var land = await _unitOfWork.Land.GetTop6();
        var responseLands = _mapper.Map<List<ResponseLand>>(land);
        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLand(string location, string landName)
    {
        var landEntities = await _unitOfWork.Land.SearchLand(location, landName);
        if (landEntities == null) throw new CultureNotFoundException("NotFound");
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByLocation(string location)
    {
        var landEntities = await _unitOfWork.Land.SearchLandByLocation(location);

        if (landEntities == null) throw new CultureNotFoundException("NotFound");
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByName(string landName)
    {
        var landEntities =
            await _unitOfWork.Land
                .SearchLandByName(landName); // Assuming a method like GetAllAsync() exists in your repository

        if (landEntities == null) throw new CultureNotFoundException("NotFound");
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> FilterLand(RequestFilter requestFilter)
    {
        var landEntities = await _unitOfWork.Land
                .FilterLand(requestFilter.location, requestFilter.rate, requestFilter.min, requestFilter.max,
                    requestFilter.size);
        if (landEntities == null) throw new CultureNotFoundException("NotFound");
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }
}