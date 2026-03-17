using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Students;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Class)
                        .ThenInclude(c => c.Program)
                .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Section)
                    .ThenInclude(sec => sec.Class)
                        .ThenInclude(c => c.Program)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Student>> GetBySectionAsync(int sectionId)
        {
            return await _context.Students
                .Include(s => s.Section)
                .Where(s => s.SectionId == sectionId)
                .ToListAsync();
        }

        public async Task<Student> CreateAsync(CreateStudentDto dto)
        {
            var entity = new Student
            {
                AdmissionNumber = dto.AdmissionNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                AdmissionDate = dto.AdmissionDate,
                SectionId = dto.SectionId,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Students.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student?> UpdateAsync(int id, UpdateStudentDto dto)
        {
            var entity = await _context.Students.FindAsync(id);
            if (entity == null) return null;

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Gender = dto.Gender;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.SectionId = dto.SectionId;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Students.FindAsync(id);
            if (entity == null) return false;

            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
