using Application.IRepository;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Schedule>> GetScheduleByPitch(Guid PitchId)
    {
        var list = await _context.Schedules.Where(schedule =>
            schedule.PitchPitchId == PitchId && schedule.Status == ScheduleEnum.Active.ToString()).ToListAsync();
        return list;
    }

    public async Task<List<Schedule>> GetScheduleByBookingiD(Guid BookingId)
    {
        var list = await _context.Schedules.Where(schedule => schedule.BookingBookingId == BookingId).ToListAsync();
        return list;
    }
}