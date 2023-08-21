using System.Globalization;
using Application.Heplers;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.Mail;
using Application.Model.Request.RequestBooking;
using Application.Model.Response.ResponseBooking;
using Application.Model.Response.ResponseSchedule;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Application.Service.Implement;

public class BookingImplement : BookingService
{
    private readonly IMailService _mailService;
    private readonly IMapper _mapper;
    private readonly ScheduleService _scheduleService;
    private readonly IUnitOfWork _unitOfWork;

    public BookingImplement(IUnitOfWork unitOfWork, IMapper mapper, ScheduleService scheduleService,
        IMailService mailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _scheduleService = scheduleService;
        _mailService = mailService;
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

            if (schedules == null && list.Size == 7)
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
            if (isConflict) throw new Exception("Please Choose Time again");

            if (isConflict == false && list.Size == 5 && size5 != 0)
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
        var total = scheduleList.Sum(s => s.Price);
        booking.TotalPrice = total;
        _unitOfWork.Save();

        var getBooking = _mapper.Map<ResponseBooking>(booking);
        getBooking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduleList);

        return getBooking;
    }

    public async Task<List<ResponseBooking>> GetAllBooking()
    {
        var bookingEntities =
            await _unitOfWork.Booking.GetAllBooking(); // Assuming a method like GetAllAsync() exists in your repository
        var responseBookings = _mapper.Map<List<ResponseBooking>>(bookingEntities);
        var i = 0;
        foreach (var booking in responseBookings)
        {
            booking.Schedules = _mapper.Map<List<ResponseSchedule>>(bookingEntities[i].Schedules);
            i++;
        }

        return responseBookings;
    }

    public async Task<List<ResponseManageBooking>> GetByCustomerId(Guid customerId)
    {
        var bookingEntities = await _unitOfWork.Booking.GetAllBookingByCustomerId(customerId); 
        var responseBookings = _mapper.Map<List<ResponseManageBooking>>(bookingEntities);
        int count = 0;
        foreach (var Booking in bookingEntities)
        {
            var firstSchedule = Booking.Schedules.FirstOrDefault( s=> s.BookingBookingId == Booking.BookingId);
            if (firstSchedule != null && firstSchedule.PitchPitch != null)
            {
                responseBookings[count].EndTime = firstSchedule.EndTime;
                responseBookings[count].StartTime = firstSchedule.StarTime;
                responseBookings[count].size = firstSchedule.PitchPitch.Size;
            }
            count++;
        }

        return responseBookings;
    }


    public async Task<bool> CancleBooking(Guid BookingId)
    {
        var booking = _unitOfWork.Booking.GetById(BookingId);
        booking.Status = BookingStatus.Inactive.ToString();

        var schedule = await _unitOfWork.Schedule.GetScheduleByBookingiD(BookingId);
        foreach (var s in schedule) s.Status = BookingStatus.Inactive.ToString();
        _unitOfWork.Save();

        return true;
    }

    public async Task<bool> CancelBooking_v2(Guid BookingId)
    {
        var booking = await _unitOfWork.Booking.GetBookingById(BookingId);
        booking.Status = BookingStatus.Inactive.ToString();
        foreach (var s in booking.Schedules)
        {
            s.Status = BookingStatus.Inactive.ToString();
            var sendMail = new Mail
            {
                To = _unitOfWork.Customer.GetById(booking.CustomerId).Email,
                Subject = "Your football pitch booking schedule is Cancel.",
                Body = "Your football pitch booking time starts from " + s.StarTime + " to" + s.EndTime + "is Cancel"
            };
            await _mailService.SendEmail(sendMail);
        }

        _unitOfWork.Save();
        return true;
    }

    public async Task<ResponseBooking> BookingPitch_v2(RequestBookingV2 requestBooking)
    {
        //Check Schedule
        var pitchs = await _unitOfWork.Pitch.GetAllPitchByLand(requestBooking.LandId);
        var pitchsList = new List<Pitch>();
        DateTime startTime;
        DateTime endTime;
        var size = 0;
        for (var i = 0; i < requestBooking.TotalPitch; i++)
        {
            startTime = requestBooking.StarTime[i].AddSeconds(1);
            endTime = requestBooking.EndTime[i].AddSeconds(1);
            size = requestBooking.Size[i];

            var p = pitchs.Find(p => p.Size == size && p.Schedules.Any(schedule =>
                (startTime >= schedule.StarTime && startTime <= schedule.EndTime) ||
                (endTime >= schedule.StarTime && endTime <= schedule.EndTime) ||
                (startTime <= schedule.StarTime && endTime >= schedule.EndTime)
            ) == false && pitchsList.Any(check => check.PitchId == p.PitchId) == false);
            if (p == null) throw new Exception("Time Start:  " + startTime + "; Time End: " + endTime + "already Exit");

            pitchsList.Add(p);
        }

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var scheduleList = new List<Schedule>();
        for (var i = 0; i < requestBooking.TotalPitch; i++)
        {
            var scheduling = await _scheduleService.CreateSchedule(requestBooking.StarTime[i],
                requestBooking.EndTime[i],
                booking.BookingId,
                pitchsList[i].PitchId, requestBooking.LandId, pitchsList[i].Size);
            scheduleList.Add(scheduling);
        }

        //Total Price 
        var total = scheduleList.Sum(s => s.Price);
        booking.TotalPrice = total;
        _unitOfWork.Save();

        var getBooking = _mapper.Map<ResponseBooking>(booking);
        getBooking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduleList);
        return getBooking;
    }


    public async Task<ResponseBooking_v2> BookingPitch_v3(RequestBooking_v3 requestBooking)
    {
        var checkDate = requestBooking.EndTime.Hour - requestBooking.StarTime.Hour;
        var checkDate2 = requestBooking.StarTime.Hour < DateTime.Now.Hour; 
        if (checkDate > 3 || checkDate < 0 )
        {
            throw new CultureNotFoundException("Thời gian đặt sân không quá 3 giờ !");
        }
        if (checkDate2 == true)
        {
            throw new CultureNotFoundException("Thời gian đặt sân lỗi!");
        }
        
        //Check Schedule
        var pitchs = await _unitOfWork.Pitch.GetPitchToBooking
            (requestBooking.LandId, requestBooking.StarTime, requestBooking.EndTime, requestBooking.Size);

        var booking = _mapper.Map<Booking>(requestBooking);
        _unitOfWork.Booking.Add(booking);
        _unitOfWork.Save();

        var scheduling = await _scheduleService.CreateSchedule(requestBooking.StarTime, requestBooking.EndTime,
            booking.BookingId, pitchs.PitchId, requestBooking.LandId, pitchs.Size);

        //Total Price 
        booking.TotalPrice = scheduling.Price;
        _unitOfWork.Save();

        var getBooking = _mapper.Map<ResponseBooking_v2>(booking);
        getBooking.StartTime = requestBooking.StarTime;
        getBooking.EndTime = requestBooking.EndTime;

        //Send Mail
        var sendMail = new Mail
        {
            To = _unitOfWork.Customer.GetById(requestBooking.CustomerId).Email,
            Subject = "Your football pitch booking schedule.",
            Body = "Your football pitch booking time starts from " + scheduling.StarTime + " to" + scheduling.EndTime
        };
        await _mailService.SendEmail(sendMail);

        return getBooking;
    }
}