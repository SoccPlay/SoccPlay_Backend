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

    public LandImplement(IUnitOfWork unitOfWork, IMapper mapper, IPitchImageRepository imageRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imageRepository = imageRepository;
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

        /*foreach (var land in responseLands)
        {
            var image = await _imageRepository.GetImageByLandId(land.LandId);
            if (image != null)
            {
                land.image = image;
            }
        }*/

        return responseLands;
    }

    public async Task<ResponseLand> LandDetail(Guid landId)
    {
        var land = _unitOfWork.Land.GetById(landId);
        var responseLand = _mapper.Map<ResponseLand>(land);
        /*var image = await _imageRepository.GetAllImageByLandId(responseLand.LandId);
        if (image != null)
        {
            responseLand.PitchImages = image;
        }*/
        return responseLand;
    }
}