using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestPitch;
using Application.Model.Respone.ResponsePitch;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Implement;

public class PitchImplement : PitchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PitchImplement(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponsePitch> CreatePitch(RequestPitch requestPitch)
    {
        var pitch = _mapper.Map<Pitch>(requestPitch);
        _unitOfWork.Pitch.Add(pitch);
        _unitOfWork.Save();
        return _mapper.Map<ResponsePitch>(pitch);
    }
}