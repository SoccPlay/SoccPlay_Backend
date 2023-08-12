using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Implement;

public class ScheduleImplement : ScheduleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPriceRepository _priceRepository;

    public ScheduleImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
    }
    public async Task<Schedule> CreateSchedule(DateTime starTime, DateTime endTime, Guid BookingId, Guid PitchId, Guid LandId)
    {
        Schedule schedule = new Schedule();
        
        
        schedule.BookingBookingId = BookingId;
        schedule.PitchPitchId = PitchId;
        schedule.EndTime = endTime;
        schedule.StarTime = starTime;
        schedule.Status = ScheduleEnum.Active.ToString();
    }
}