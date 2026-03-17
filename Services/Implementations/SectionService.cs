using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Sections;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly SchoolDbContext _context;

        public SectionService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Section>> GetAllAsync()
        {
            return await _context.Sections
                .Include(s => s.Class)
                    .ThenInclude(c => c.Program)
                .Include(s => s.ClassTeacher)
                .ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(int id)
        {
            return await _context.Sections
                .Include(s => s.Class)
                    .ThenInclude(c => c.Program)
                .Include(s => s.ClassTeacher)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Section>> GetByClassAsync(int classId)
        {
            return await _context.Sections
                .Include(s => s.ClassTeacher)
                .Where(s => s.ClassId == classId)
                .ToListAsync();
        }

        public async Task<Section> CreateAsync(CreateSectionDto dto)
        {
            var entity = new Section
            {
                ClassId = dto.ClassId,
                Name = dto.Name,
                ClassTeacherId = dto.ClassTeacherId
            };

            _context.Sections.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Section?> UpdateAsync(int id, UpdateSectionDto dto)
        {
            var entity = await _context.Sections.FindAsync(id);
            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.ClassTeacherId = dto.ClassTeacherId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Sections.FindAsync(id);
            if (entity == null) return false;

            _context.Sections.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
