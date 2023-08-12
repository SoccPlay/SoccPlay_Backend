using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
using Application.Service;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Implement;

public class BookingImplement : BookingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPriceRepository _priceRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IPitchRepository _pitchRepository;

    public BookingImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository, IPitchRepository pitchRepository, IScheduleRepository scheduleRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
        _pitchRepository = pitchRepository;
        _scheduleRepository = scheduleRepository;
    }
    public async Task<ResponseBooking> BookingPitch(RequestBooking requestBooking)
    {
        //Check Schedule
        var pitchs = await _pitchRepository.GetAllPitchByLand(requestBooking.LandId);
        int size5 = requestBooking.Size5;
        int size7 = requestBooking.Size7;
        List<Guid> pitchsEmpty = new List<Guid>();
        foreach (var list in pitchs)
        {
            var schedules = await _scheduleRepository.GetPitchSchedule(list.PitchId);
            if (size5 == 0 && size7 == 0)
            {
                break;
            }
            if(schedules == null)
            {
                pitchsEmpty.Add(list.PitchId);
                continue;
            }
            bool isConflict = schedules.Any(schedule =>
                (requestBooking.StarTime >= schedule.StarTime && requestBooking.StarTime <= schedule.EndTime) ||
                (requestBooking.EndTime >= schedule.StarTime && requestBooking.EndTime <= schedule.EndTime) ||
                (requestBooking.StarTime <= schedule.StarTime && requestBooking.EndTime >= schedule.EndTime)
            );
            if (isConflict == false && list.Size == 5 && size5 != 0)
            {
                pitchsEmpty.Add(list.PitchId);
                size5--;
            }
            else if (isConflict == false && list.Size == 7 && size7 != 0)
            {
                pitchsEmpty.Add(list.PitchId);
                size7--;
            }
        }

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();
        
        


    }
}