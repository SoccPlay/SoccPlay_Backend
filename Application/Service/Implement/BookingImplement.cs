using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
using Application.Model.Respone.ResponseSchedule;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Implement;

public class BookingImplement : BookingService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ScheduleService _scheduleService;

    public BookingImplement(IUnitOfWork unitOfWork, IMapper mapper, ScheduleService scheduleService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _scheduleService = scheduleService;
    }

    public async Task<ResponseBooking> BookingPitch(RequestBooking requestBooking)
    {
        //Check Schedule
        var pitchs = await _unitOfWork.Pitch.GetAllPitchByLand(requestBooking.LandId);
        var size5 = requestBooking.Size5;
        var size7 = requestBooking.Size7;
        var pitchsEmpty = new List<Pitch>();
        foreach (var list in pitchs)
        {
            var schedules = await _unitOfWork.Schedule.GetScheduleByPitch(list.PitchId);
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

            var startTime = requestBooking.StarTime.AddSeconds(1);
            var endTime = requestBooking.EndTime.AddSeconds(1);
            var isConflict = schedules.Any(schedule =>
                (startTime >= schedule.StarTime && startTime <= schedule.EndTime) ||
                (endTime >= schedule.StarTime && endTime <= schedule.EndTime) ||
                (startTime <= schedule.StarTime && endTime >= schedule.EndTime)
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
            else if (isConflict == false && list.Size == 7 && size7 != 0)
            {
                pitchsEmpty.Add(list);
                size7--;
            }
        }

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var scheduleList = new List<Schedule>();
        foreach (var p in pitchsEmpty)
        {
            var scheduling = await _scheduleService.CreateSchedule(requestBooking.StarTime, requestBooking.EndTime,
                booking.BookingId,
                p.PitchId, requestBooking.LandId, p.Size);
            scheduleList.Add(scheduling);
        }

        //Total Price 
        float total = (float)scheduleList.Sum(s => s.Price);
        booking.TotalPrice = total;
        _unitOfWork.Save();

        var getBooking = _mapper.Map<ResponseBooking>(booking);
        getBooking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduleList);
        return getBooking;
    }

    public async Task<List<ResponseBooking>> GetAllBooking()
    {
        var bookingEntities = await _unitOfWork.Booking.GetAllBooking(); // Assuming a method like GetAllAsync() exists in your repository
        var responseBookings = _mapper.Map<List<ResponseBooking>>(bookingEntities);
        int i = 0;
        foreach (var booking in responseBookings)
        {
            booking.Schedules = _mapper.Map<List<ResponseSchedule>>(bookingEntities[i].Schedules);
            i++;
        }
        return responseBookings;
    }

    public async Task<List<ResponseBooking>> GetByCustomerId(Guid customerId)
    {
        var bookingEntities = await _unitOfWork.Booking.GetAllBookingByCustomerId(customerId); // Assuming a method like GetAllAsync() exists in your repository
        var responseBookings = _mapper.Map<List<ResponseBooking>>(bookingEntities);
        int i = 0;
        foreach (var booking in responseBookings)
        {
            booking.Schedules = _mapper.Map<List<ResponseSchedule>>(bookingEntities[i].Schedules);
            i++;
        }
        return responseBookings;
    }

    public async Task<bool> CancleBooking(Guid BookingId)
    {
        var booking = _unitOfWork.Booking.GetById(BookingId);
        booking.Status = BookingStatus.Inactive.ToString();

        var schedule = await _unitOfWork.Schedule.GetScheduleByBookingiD(BookingId);
        foreach (var s in schedule)
        {
            s.Status = BookingStatus.Inactive.ToString();
        }

        _unitOfWork.Save();
        return true;
    }

    
    public async Task<ResponseBooking> BookingPitch_v2(RequestBookingV2 requestBooking)
    {
        //Check Schedule
        var pitchs = await _unitOfWork.Pitch.GetAllPitchByLand(requestBooking.LandId);
        List<Pitch> pitchsList = new List<Pitch>();
        DateTime startTime;
        DateTime endTime;
        int size = 0;
        for (int i = 0; i < requestBooking.TotalPitch; i++)
        {
            startTime = requestBooking.StarTime[i].AddSeconds(1);
            endTime = requestBooking.EndTime[i].AddSeconds(1);
            size = requestBooking.Size[i];

            var p = pitchs.Find(p => p.Size == size && p.Schedules.Any(schedule =>
                (startTime >= schedule.StarTime && startTime <= schedule.EndTime) ||
                (endTime >= schedule.StarTime && endTime <= schedule.EndTime) ||
                (startTime <= schedule.StarTime && endTime >= schedule.EndTime)
            ) == false && pitchsList.Any(check => check.PitchId == p.PitchId) == false);
            if (p == null)
            {
                throw new Exception("Time Start:  " + startTime + "; Time End: " + endTime + "already Exit");
            }
            pitchsList.Add(p);
        }

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var scheduleList = new List<Schedule>();
        for (int i = 0; i < requestBooking.TotalPitch; i++)
        {
            var scheduling = await _scheduleService.CreateSchedule(requestBooking.StarTime[i], requestBooking.EndTime[i],
                booking.BookingId,
                pitchsList[i].PitchId, requestBooking.LandId, pitchsList[i].Size);
            scheduleList.Add(scheduling);
        }

        //Total Price 
        float total = (float)scheduleList.Sum(s => s.Price);
        booking.TotalPrice = total;
        _unitOfWork.Save();

        var getBooking = _mapper.Map<ResponseBooking>(booking);
        getBooking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduleList);
        return getBooking;
    }
}