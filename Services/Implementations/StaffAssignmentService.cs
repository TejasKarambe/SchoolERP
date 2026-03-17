using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.StaffAssignments;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StaffAssignmentService : IStaffAssignmentService
    {
        private readonly SchoolDbContext _context;

        public StaffAssignmentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffAssignment>> GetAllAsync()
        {
            return await _context.StaffAssignments
                .Include(a => a.Staff)
                .Include(a => a.Section)
                    .ThenInclude(s => s.Class)
                .Include(a => a.Subject)
                .ToListAsync();
        }

        public async Task<StaffAssignment?> GetByIdAsync(int id)
        {
            return await _context.StaffAssignments
                .Include(a => a.Staff)
                .Include(a => a.Section)
                    .ThenInclude(s => s.Class)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<StaffAssignment>> GetByStaffAsync(int staffId)
        {
            return await _context.StaffAssignments
                .Include(a => a.Section)
                    .ThenInclude(s => s.Class)
                .Include(a => a.Subject)
                .Where(a => a.StaffId == staffId)
                .ToListAsync();
        }

        public async Task<List<StaffAssignment>> GetBySectionAsync(int sectionId)
        {
            return await _context.StaffAssignments
                .Include(a => a.Staff)
                .Include(a => a.Subject)
                .Where(a => a.SectionId == sectionId)
                .ToListAsync();
        }

        public async Task<StaffAssignment> CreateAsync(CreateStaffAssignmentDto dto)
        {
            var entity = new StaffAssignment
            {
                StaffId = dto.StaffId,
                SectionId = dto.SectionId,
                SubjectId = dto.SubjectId,
                IsClassTeacher = dto.IsClassTeacher
            };

            _context.StaffAssignments.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StaffAssignment?> UpdateAsync(int id, UpdateStaffAssignmentDto dto)
        {
            var entity = await _context.StaffAssignments.FindAsync(id);
            if (entity == null) return null;

            entity.SubjectId = dto.SubjectId;
            entity.IsClassTeacher = dto.IsClassTeacher;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StaffAssignments.FindAsync(id);
            if (entity == null) return false;

            _context.StaffAssignments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
