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


    public async Task<ResponseLand> CreateLand(RequestLand requestLand)
    {
        var land = _mapper.Map<Land>(requestLand);
        land.Owner = _unitOfWork.Owner.GetById(requestLand.OwnerId);
        _unitOfWork.Land.Add(land);
        _unitOfWork.Save();
        return _mapper.Map<ResponseLand>(land);
    }
    
    public async Task<List<ResponseLand>> GetAllLands()
    {
        var landEntities = _unitOfWork.Land.GetAll(); // Assuming a method like GetAllAsync() exists in your repository

        var responseLands = _mapper.Map<List<ResponseLand>>(landEntities);

        return responseLands;
    }
}