using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Implement;

public class BookingImplement : BookingService
{
    private readonly IMapper _mapper;
    private readonly IPitchRepository _pitchRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly ScheduleService _scheduleService;
    private readonly IUnitOfWork _unitOfWork;

    public BookingImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository,
        IPitchRepository pitchRepository, IScheduleRepository scheduleRepository, ScheduleService scheduleService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
        _pitchRepository = pitchRepository;
        _scheduleRepository = scheduleRepository;
        _scheduleService = scheduleService;
    }

    public async Task<ResponseBooking> BookingPitch(RequestBooking requestBooking)
    {
        //Check Schedule
        var pitchs = await _pitchRepository.GetAllPitchByLand(requestBooking.LandId);
        var size5 = requestBooking.Size5;
        var size7 = requestBooking.Size7;
        var pitchsEmpty = new List<Pitch>();
        foreach (var list in pitchs)
        {
            var schedules = await _scheduleRepository.GetPitchSchedule(list.PitchId);
            if (size5 == 0 && size7 == 0) break;
            if (schedules == null && list.Size == 5)
            {
                pitchsEmpty.Add(list);
                continue;
            }
            else if (schedules == null && list.Size == 7)
            {
                pitchsEmpty.Add(list);
                continue;
            }
            
            var isConflict = schedules.Any(schedule =>
                (requestBooking.StarTime >= schedule.StarTime && requestBooking.StarTime <= schedule.EndTime) ||
                (requestBooking.EndTime >= schedule.StarTime && requestBooking.EndTime <= schedule.EndTime) ||
                (requestBooking.StarTime <= schedule.StarTime && requestBooking.EndTime >= schedule.EndTime)
            );
            if (isConflict == true)
            {
                throw new Exception("Please Choose Time again");
            }
            else if (isConflict == false && list.Size == 5 && size5 != 0)
            {
                pitchsEmpty.Add(list);
                size5--;
            }
            else if(isConflict == false && list.Size == 7 && size7 != 0)
            {
                pitchsEmpty.Add(list);
                size7--;
            }
        }

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var scheduleList = new List<Schedule>();
        foreach (var p  in pitchsEmpty)
        {
            var scheduling = await _scheduleService.CreateSchedule(requestBooking.StarTime, requestBooking.EndTime, booking.BookingId,
                p.PitchId, requestBooking.LandId, p.Size);
            scheduleList.Add(scheduling);
        }

        var getBooking = _mapper.Map<ResponseBooking>(booking);
        getBooking.Schedules = scheduleList;
        return _mapper.Map<ResponseBooking>(booking);
    }
}