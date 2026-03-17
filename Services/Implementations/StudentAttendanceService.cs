using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Attendance;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StudentAttendanceService : IStudentAttendanceService
    {
        private readonly SchoolDbContext _context;

        public StudentAttendanceService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentAttendance>> GetAllAsync()
        {
            return await _context.StudentAttendances
                .Include(a => a.Student)
                .Include(a => a.Section)
                .Include(a => a.MarkedBy)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<StudentAttendance?> GetByIdAsync(int id)
        {
            return await _context.StudentAttendances
                .Include(a => a.Student)
                .Include(a => a.Section)
                .Include(a => a.MarkedBy)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<StudentAttendance>> GetByStudentAsync(int studentId)
        {
            return await _context.StudentAttendances
                .Include(a => a.Section)
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }

        public async Task<List<StudentAttendance>> GetBySectionAndDateAsync(int sectionId, DateTime date)
        {
            return await _context.StudentAttendances
                .Include(a => a.Student)
                .Where(a => a.SectionId == sectionId && a.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<StudentAttendance> CreateAsync(CreateStudentAttendanceDto dto)
        {
            var entity = new StudentAttendance
            {
                StudentId = dto.StudentId,
                SectionId = dto.SectionId,
                Date = dto.Date.Date,
                Status = dto.Status,
                MarkedByStaffId = dto.MarkedByStaffId
            };

            _context.StudentAttendances.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<StudentAttendance>> BulkCreateAsync(BulkStudentAttendanceDto dto)
        {
            var attendanceDate = dto.Date.Date;

            // Remove existing attendance for the section on that date before re-marking
            var existing = await _context.StudentAttendances
                .Where(a => a.SectionId == dto.SectionId && a.Date.Date == attendanceDate)
                .ToListAsync();

            if (existing.Any())
                _context.StudentAttendances.RemoveRange(existing);

            var entities = dto.Entries.Select(e => new StudentAttendance
            {
                StudentId = e.StudentId,
                SectionId = dto.SectionId,
                Date = attendanceDate,
                Status = e.Status,
                MarkedByStaffId = dto.MarkedByStaffId
            }).ToList();

            _context.StudentAttendances.AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<StudentAttendance?> UpdateAsync(int id, UpdateStudentAttendanceDto dto)
        {
            var entity = await _context.StudentAttendances.FindAsync(id);
            if (entity == null) return null;

            entity.Status = dto.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StudentAttendances.FindAsync(id);
            if (entity == null) return false;

            _context.StudentAttendances.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
