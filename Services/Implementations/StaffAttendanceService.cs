using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Attendance;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StaffAttendanceService : IStaffAttendanceService
    {
        private readonly SchoolDbContext _context;

        public StaffAttendanceService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffAttendance>> GetAllAsync()
        {
            return await _context.StaffAttendances
                .Include(a => a.Staff)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<StaffAttendance?> GetByIdAsync(int id)
        {
            return await _context.StaffAttendances
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<StaffAttendance>> GetByStaffAsync(int staffId)
        {
            return await _context.StaffAttendances
                .Where(a => a.StaffId == staffId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<StaffAttendance>> GetByDateAsync(DateTime date)
        {
            return await _context.StaffAttendances
                .Include(a => a.Staff)
                .Where(a => a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<StaffAttendance> CreateAsync(CreateStaffAttendanceDto dto)
        {
            var entity = new StaffAttendance
            {
                StaffId = dto.StaffId,
                Date = dto.Date.Date,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                Status = dto.Status
            };

            _context.StaffAttendances.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StaffAttendance?> UpdateAsync(int id, UpdateStaffAttendanceDto dto)
        {
            var entity = await _context.StaffAttendances.FindAsync(id);
            if (entity == null) return null;

            entity.CheckInTime = dto.CheckInTime;
            entity.CheckOutTime = dto.CheckOutTime;
            entity.Status = dto.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StaffAttendances.FindAsync(id);
            if (entity == null) return false;

            _context.StaffAttendances.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
