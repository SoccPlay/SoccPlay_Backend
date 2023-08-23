using Application.IRepository;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Entities;
using Infrastructure.RepositoryImp.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImp;

public class PitchRepository : GenericRepository<Pitch>, IPitchRepository
{
    public PitchRepository(FootBall_PitchContext context) : base(context)
    {
    }

    public async Task<List<Pitch>> GetAllPitchByLand(Guid LandId)
    {
        var list = await _context.Pitches.Where(pitch => pitch.LandId == LandId).Include(s => s.Schedules)
            .ToListAsync();
        return list;
    }
    
    public async Task<List<ICollection<Pitch>>> Get(Guid ownerId)
    {
        var list = await _context.Lands.Where(land => land.OwnerId == ownerId).Include(p => p.Pitches).Select( p => p.Pitches)
            .ToListAsync();
        return list;
    }

    public async Task<List<Pitch>> GetAllPitchByLandAndDate(Guid landId, DateTime date, int size)
    {
        var query = await _context.Pitches
            .Where(pitch => pitch.LandId == landId && pitch.Size == size)
            .Include(pitch => pitch.Schedules.Where(schedule =>
                schedule.StarTime.Date == date.Date && schedule.Status != ScheduleEnum.Inactive.ToString()))
            .ToListAsync();
        return query;
    }

    public async Task<Pitch> GetPitchByLandAndDate(Guid landId, DateTime date, int size, string name)
    {
        var query = await _context.Pitches
            .Where(pitch => pitch.LandId == landId && pitch.Size == size && pitch.Name == name)
            .Include(pitch => pitch.Schedules.Where(schedule =>
                schedule.StarTime.Date == date.Date && schedule.Status != ScheduleEnum.Inactive.ToString()))
            .FirstOrDefaultAsync();
        if (query == null)
        {
            throw new Exception("Not Found");
        }

        return query;
    }

    public async Task<Pitch> GetPitchToBooking(Guid landId, DateTime startTime, DateTime endTime, int size)
    {
        
        var query = await _context.Set<Pitch>().Include(pitch => pitch.Land).FirstOrDefaultAsync(
            p => p.LandId == landId && p.Size == size && p.Schedules.Any(schedule =>
                string.IsNullOrEmpty(schedule.Status) ||
                schedule.Status == ScheduleEnum.Active.ToString() &&
                ((startTime.AddMinutes(1) >= schedule.StarTime && startTime.AddMinutes(1) <= schedule.EndTime) ||
                 (endTime.AddMinutes(1) >= schedule.StarTime && endTime.AddMinutes(1) <= schedule.EndTime) ||
                 (startTime.AddMinutes(1) <= schedule.StarTime && endTime.AddMinutes(1) >= schedule.EndTime))
            ) == false);
        if (query == null) throw new Exception("Time Start:  " + startTime + "; Time End: " + endTime + "already Exit");
        return query;
    }
}