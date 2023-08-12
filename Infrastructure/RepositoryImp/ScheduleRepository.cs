using Application.IRepository;
using Domain.Entities;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Schedule>> GetPitchSchedule(Guid PitchId)
    {
        var list = await _context.Schedules.Where(schedule => schedule.PitchPitchId == PitchId).ToListAsync();
        return list;
    }
}