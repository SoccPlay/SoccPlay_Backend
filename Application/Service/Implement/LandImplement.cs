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

    public LandImplement(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseLand_2> CreateLand(RequestLand requestLand)
    {
        var land = _mapper.Map<Land>(requestLand);
        if ( _unitOfWork.Owner.GetById(land.OwnerId) == null)
        {
            throw new Exception("Not Found Owner");
        }
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

    public async Task<ResponseLand> LandDetail(Guid landId)
    {
        var land = _unitOfWork.Land.GetLandByIdLand(landId);
        if (land == null)
        {
            throw new CultureNotFoundException("NotFound");
        }
        var responseLands = _mapper.Map<ResponseLand>(land);
        return responseLands;
    }
    public async Task<List<ResponseLand>> SearchLand(string location, string landName)
    {
        var landEntities = await _unitOfWork.Land.SearchLand(location, landName);
        if (landEntities == null)
        {
            throw new CultureNotFoundException("NotFound");
        }
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByLocation(string location)
    {
        var landEntities = await _unitOfWork.Land.SearchLandByLocation(location);

        if (landEntities == null)
        {
            throw new CultureNotFoundException("NotFound");
        }
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }

    public async Task<List<ResponseLand>> SearchLandByName(string landName)
    {
        var landEntities = await _unitOfWork.Land.SearchLandByName(landName); // Assuming a method like GetAllAsync() exists in your repository

        if (landEntities == null)
        {
            throw new CultureNotFoundException("NotFound");
        }
        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);
        return responseLands;
    }
}