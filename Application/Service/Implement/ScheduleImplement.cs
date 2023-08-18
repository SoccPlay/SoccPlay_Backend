using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.Service.Implement;

public class ScheduleImplement : ScheduleService
{
    private readonly IMapper _mapper;
    private readonly IPriceRepository _priceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
    }

    public async Task<Schedule> CreateSchedule(DateTime starTime, DateTime endTime, Guid BookingId, Guid PitchId,
        Guid LandId, int Size)
    {
        var schedule = new Schedule();

        //Caculate Price
        var duration = endTime - starTime;

        var totalHours = duration.TotalHours;
        var totalMinutes = duration.TotalMinutes;
        var totalHoursAndMinutes = (float)(totalHours + totalMinutes / 60);
        var getPrice = await _priceRepository.GetBySizeAndLand(LandId, Size, starTime);

        var price = getPrice.Price1 * totalHoursAndMinutes;

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