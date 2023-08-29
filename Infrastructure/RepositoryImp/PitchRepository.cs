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
    public async Task<string> GetNameByBookingId(Guid bookingId)
    {
        var list = await _context.Pitches.FirstOrDefaultAsync(pitch => pitch.Schedules.FirstOrDefault(s => s.BookingBookingId == bookingId)!.PitchPitchId == pitch.PitchId);
        return list?.Name;
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
            .Where(pitch => pitch.LandId == landId && pitch.Status == PitchStatus.Active.ToString() && pitch.Size == size).Include(l => l.Land.Prices )
            .Include(pitch => pitch.Schedules.Where(schedule =>
                schedule.StarTime.Date == date.Date && schedule.Status == ScheduleEnum.Waiting.ToString() ))
            .ToListAsync();
        return query;
    }

    public async Task<Pitch> GetPitchByLandAndDate(Guid landId, DateTime date, int size, string name)
    {
        var query = await _context.Pitches
            .Where(pitch => pitch.LandId == landId &&  pitch.Size == size && pitch.Name == name && pitch.Status == PitchStatus.Active.ToString())
            .Include(pitch => pitch.Schedules.Where(schedule =>
                schedule.StarTime.Date == date.Date && schedule.Status == ScheduleEnum.Waiting.ToString()))
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
            p => p.LandId == landId && p.Size == size && p.Status == PitchStatus.Active.ToString() && p.Schedules.Any(schedule =>
                string.IsNullOrEmpty(schedule.Status) ||
                schedule.Status == ScheduleEnum.Waiting.ToString() &&
                ((startTime.AddMinutes(1) >= schedule.StarTime && startTime.AddMinutes(1) <= schedule.EndTime) ||
                 (endTime.AddMinutes(1) >= schedule.StarTime && endTime.AddMinutes(1) <= schedule.EndTime) ||
                 (startTime.AddMinutes(1) <= schedule.StarTime && endTime.AddMinutes(1) >= schedule.EndTime))
            ) == false);
        if (query == null) throw new Exception("Time Start:  " + startTime + "; Time End: " + endTime + "already Exit");
        return query;
    }
   public async Task<List<Pitch>> GetPitchByNameLandAndOwnerId(Guid landId, Guid ownerId)
    {
        var list = await _context.Pitches.Include(c=>c.Owner).Include(c=>c.Land).Include( p => p.Land.Prices).Where(c=>c.Land.LandId == landId && c.Owner.OwnerId==ownerId).ToListAsync();
        return list;
    }

   public async Task<int[]> GetNumPitch(Guid ownerId)
   {
       int size5 = await _context.Pitches
           .Where(p => p.Land.OwnerId == ownerId && p.Size == 5)
           .CountAsync();
       int size7 = await _context.Pitches
           .Where(p => p.Land.OwnerId == ownerId && p.Size == 7)
           .CountAsync();
       int[] count = new [] { size5, size7 }; 
       return count;
   }
   public async Task<int[]> GetNumPitchByLand(Guid landId)
   {
       int size5 = await _context.Pitches
           .Where(p => p.LandId == landId && p.Size == 5)
           .CountAsync();
       int size7 = await _context.Pitches
           .Where(p => p.LandId == landId && p.Size == 7)
           .CountAsync();
       int[] count = new [] { size5, size7 }; 
       return count;
   }

   public async Task<Pitch> GetPitchById(Guid id)
   {
       var pitch = await _context.Pitches.Include(s => s.Schedules).Include(l => l.Land).Include(l => l.Land.Prices).FirstOrDefaultAsync(p => p.PitchId == id);
       if (pitch == null)
       {
           throw new Exception("Null");
       }
       return pitch;
   }
}