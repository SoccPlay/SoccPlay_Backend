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
    public async Task<Schedule> CreateSchedule(DateTime starTime, DateTime endTime, Guid BookingId, Guid PitchId, Guid LandId, int Size)
    {
        Schedule schedule = new Schedule();
        
        //Caculate Price
        TimeSpan duration = endTime - starTime;
        
        double totalHours = duration.TotalHours;
        double totalMinutes = duration.TotalMinutes;
        float totalHoursAndMinutes = (float)(totalHours + (totalMinutes / 60));
        var getPrice = await _priceRepository.GetBySizeAndLand(LandId, Size, starTime);

        float price = getPrice.Price1 * totalHoursAndMinutes;
        
        schedule.BookingBookingId = BookingId;
        schedule.PitchPitchId = PitchId;
        schedule.EndTime = endTime;
        schedule.StarTime = starTime;
        schedule.Price = price;
        schedule.Status = ScheduleEnum.Active.ToString();
        _unitOfWork.Schedule.Add(schedule);
        _unitOfWork.Save();
        return schedule;
    }
}