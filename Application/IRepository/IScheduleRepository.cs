using Application.IRepository.IGeneric;
using Domain.Entities;
using Domain.Enum;

namespace Application.IRepository;

public interface IScheduleRepository   : IGenericRepository<Schedule>
{
    Task<List<Schedule>> GetScheduleByPitch(Guid PitchId);
    
    Task<List<Schedule>> GetScheduleByBookingiD(Guid BookingId);
}