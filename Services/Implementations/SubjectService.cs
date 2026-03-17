using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Subjects;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly SchoolDbContext _context;

        public SubjectService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject> CreateAsync(CreateSubjectDto dto)
        {
            var entity = new Subject
            {
                Name = dto.Name,
                Code = dto.Code
            };

            _context.Subjects.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Subject?> UpdateAsync(int id, UpdateSubjectDto dto)
        {
            var entity = await _context.Subjects.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Code = dto.Code;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Subjects.FindAsync(id);
            if (entity == null) return false;

            _context.Subjects.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
