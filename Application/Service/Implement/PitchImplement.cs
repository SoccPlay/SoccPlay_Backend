using System.Globalization;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestPitch;
using Application.Model.Response.ResponsePitch;
using Application.Model.Response.ResponseSchedule;
using AutoMapper;
using Domain.Entities;

namespace Application.Service.Implement;

public class PitchImplement : PitchService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PitchImplement(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponsePitch> CreatePitch(RequestPitch requestPitch)
    {
        var pitch = _mapper.Map<Pitch>(requestPitch);
        var land = _unitOfWork.Land.GetById(requestPitch.LandId);
        land.TotalPitch = land.TotalPitch + 1;
        _unitOfWork.Pitch.Add(pitch);
        _unitOfWork.Save();
        return _mapper.Map<ResponsePitch>(pitch);
    }

    public async Task<List<ResponsePitchV2>> GetScheduleListByDate(Guid landId, string date, int size)
    {
        var provider = CultureInfo.InvariantCulture;
        var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", provider);
        if (dateTime < DateTime.Now.Date) throw new Exception("You Can Not Choose this day !");
        var pitches = await _unitOfWork.Pitch.GetAllPitchByLandAndDate(landId, dateTime, size);
        var response = _mapper.Map<List<ResponsePitchV2>>(pitches);
        var i = 0;
        foreach (var pitch in response)
        {
            pitch.Schedules = _mapper.Map<List<ResponseSchedule_v2>>(pitches[i].Schedules);
            i++;
        }

        return response;
    }

    public async Task<ResponsePitchV2> GetScheduleList(Guid landId, string date, int size, string name)
    {
        var provider = CultureInfo.InvariantCulture;
        var dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", provider);
        if (dateTime < DateTime.Now.Date) throw new Exception("You Can Not Choose this day !");
        var pitche = await _unitOfWork.Pitch.GetPitchByLandAndDate(landId, dateTime, size, name);
        var response = _mapper.Map<ResponsePitchV2>(pitche);
        response.Schedules = _mapper.Map<List<ResponseSchedule_v2>>(pitche.Schedules);
        return response;
    }
}