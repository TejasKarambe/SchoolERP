using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Programs;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly SchoolDbContext _context;

        public ProgramService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProgramEntity>> GetAllAsync()
        {
            return await _context.Programs
                .Include(p => p.Classes)
                .ToListAsync();
        }

        public async Task<ProgramEntity?> GetByIdAsync(int id)
        {
            return await _context.Programs
                .Include(p => p.Classes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProgramEntity> CreateAsync(CreateProgramDto dto)
        {
            var entity = new ProgramEntity
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Programs.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ProgramEntity?> UpdateAsync(int id, UpdateProgramDto dto)
        {
            var entity = await _context.Programs.FindAsync(id);

            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Programs.FindAsync(id);

            if (entity == null) return false;

            _context.Programs.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
