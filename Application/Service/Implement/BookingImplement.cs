using Application.IRepository;
using Application.IRepository.IUnitOfWork;
using Application.Model.Request.RequestBooking;
using Application.Model.Respone.ResponseBooking;
using Application.Model.Respone.ResponseSchedule;
using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;

namespace Infrastructure.Implement;

public class BookingImplement : BookingService
{
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;
    private readonly IPitchRepository _pitchRepository;
    private readonly IPriceRepository _priceRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly ScheduleService _scheduleService;
    private readonly ILandRepository _landRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BookingImplement(IUnitOfWork unitOfWork, IMapper mapper, IPriceRepository priceRepository,
        IBookingRepository bookingRepository,
        IPitchRepository pitchRepository, IScheduleRepository scheduleRepository, ScheduleService scheduleService,
        ILandRepository landRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _priceRepository = priceRepository;
        _pitchRepository = pitchRepository;
        _scheduleRepository = scheduleRepository;
        _scheduleService = scheduleService;
        _bookingRepository = bookingRepository;
        _landRepository = landRepository;
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
            var schedules = await _scheduleRepository.GetScheduleByPitch(list.PitchId);
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
        var bookingEntities =
            _unitOfWork.Booking.GetAll(); // Assuming a method like GetAllAsync() exists in your repository
        var responseBookings = _mapper.Map<List<ResponseBooking>>(bookingEntities);
        foreach (var booking in responseBookings)
        {
            var scheduling = await _scheduleRepository.GetScheduleByBookingiD(booking.BookingId);
            if (scheduling.Count != 0)
            {
                string guid = scheduling.First().ScheduleId.ToString();
                var pitch = scheduling.First().PitchPitchId;
                var landid = _pitchRepository.GetById(pitch).LandId;
                booking.Location = _unitOfWork.Land.GetById(landid).Location;
            }

            booking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduling);
        }

        return responseBookings;
    }

    public async Task<List<ResponseBooking>> GetByCustomerId(Guid customerId)
    {
        var bookingEntities = await _unitOfWork.Booking.GetAllBookingByCustomerId(customerId); // Assuming a method like GetAllAsync() exists in your repository
        var responseBookings = _mapper.Map<List<ResponseBooking>>(bookingEntities);
        foreach (var booking in responseBookings)
        {
            var scheduling = await _scheduleRepository.GetScheduleByBookingiD(booking.BookingId);
            if (scheduling.Count != 0)
            {
                string guid = scheduling.First().ScheduleId.ToString();
                var pitch = scheduling.First().PitchPitchId;
                var landid = _pitchRepository.GetById(pitch).LandId;
                booking.Location = _unitOfWork.Land.GetById(landid).Location;
            }

            booking.Schedules = _mapper.Map<List<ResponseSchedule>>(scheduling);
        }

        return responseBookings;
    }

    public async Task<bool> CancleBooking(Guid BookingId)
    {
        var booking = _unitOfWork.Booking.GetById(BookingId);
        booking.Status = BookingStatus.Inactive.ToString();

        var schedule = await _scheduleRepository.GetScheduleByBookingiD(BookingId);
        foreach (var s in schedule)
        {
            s.Status = BookingStatus.Inactive.ToString();
        }

        _unitOfWork.Save();
        return true;
    }


   
}