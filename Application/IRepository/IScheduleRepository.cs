using Application.IRepository.IGeneric;
using Domain.Entities;

namespace Application.IRepository;

public interface IScheduleRepository : IGenericRepository<Schedule>
{
    Task<List<Schedule>> GetScheduleByPitch(Guid PitchId);

    Task<List<Schedule>> GetScheduleByBookingiD(Guid BookingId);
}