using Domain.Entities;

namespace Application.Service;

public interface ScheduleService
{
    Task<Schedule> CreateSchedule(DateTime starTime, DateTime endTime, Guid BookingId, Guid PitchId, Guid LandId);
}