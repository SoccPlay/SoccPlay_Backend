using System.Globalization;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestPitch;
using Application.Model.Response.ResponsePitch;
using Application.Model.Response.ResponseSchedule;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.Service.Implement;

public class PitchImplement : PitchService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly BookingService _bookingService;

    public PitchImplement(IUnitOfWork unitOfWork, IMapper mapper, BookingService bookingService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _bookingService = bookingService;
    }

    public async Task<ResponsePitch> CreatePitch(RequestPitch requestPitch)
    {
        var pitch = _mapper.Map<Pitch>(requestPitch);
        pitch.Date= DateTime.Now;
        var land = await _unitOfWork.Land.GetLandByIdLand(requestPitch.LandId);
        if (land.Prices == null || land.Prices.Any(p => p.Size == requestPitch.Size) == false)
        {
            throw new Exception("You need to Add price before create Pitch");
        }

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
    public async Task<List<ICollection<ResponsePitch>>> GetAllPitchOfOwner(Guid ownerId)
    {
        var pitchs = await _unitOfWork.Pitch.Get(ownerId);
        var response = _mapper.Map<List<ICollection<ResponsePitch>>>(pitchs);
        return response;
    }
     public async Task<List<ResponsePitch>> GetAllPitchByNameLandAndOwnerId(Guid ownerId, Guid landId)
    {
        var pitchs = await _unitOfWork.Pitch.GetPitchByNameLandAndOwnerId(landId, ownerId);
        var respone = _mapper.Map<List<ResponsePitch>>(pitchs);
        int i = 0;
        foreach (var p in pitchs)
        {
            respone[i].PriceMax = p.Land.Prices.FirstOrDefault(price => price.Size == p.Size && price.StarTime == 14).Price1;
            respone[i].PriceMin = p.Land.Prices.FirstOrDefault(price => price.Size == p.Size && price.StarTime == 7).Price1;
            i++;
        }
        return respone;
    }
     public async Task<int[]> GetNumPitch(Guid ownerId)
     {
         return await _unitOfWork.Pitch.GetNumPitch(ownerId);
     }
     public async Task<int[]> GetNumPitchByLand(Guid ownerId)
     {
         return await _unitOfWork.Pitch.GetNumPitchByLand(ownerId);
     }
     public async Task<ResponsePitch> ChangePitchStatus(Guid pitchId, string status)
     {
         var pitch = await _unitOfWork.Pitch.GetPitchById(pitchId);
         if (status.ToUpper().Contains(PitchStatus.Inactive.ToString().ToUpper()))
         {
             pitch.Status = PitchStatus.Inactive.ToString();
             _unitOfWork.Save();
             if (pitch.Schedules.Count != 0)
             {
                 foreach (var s in pitch.Schedules)
                 {
                     await _bookingService.CancelBooking_v3(s.BookingBookingId);
                 }
             }

             return _mapper.Map<ResponsePitch>(pitch);
         }
         else
         {
             pitch.Status = PitchStatus.Active.ToString();
             _unitOfWork.Save();
             return _mapper.Map<ResponsePitch>(pitch);
         }
     }
}