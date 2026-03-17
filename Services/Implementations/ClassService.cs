using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Classes;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly SchoolDbContext _context;

        public ClassService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Class>> GetAllAsync()
        {
            return await _context.Classes
                .Include(c => c.Program)
                .ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(int id)
        {
            return await _context.Classes
                .Include(c => c.Program)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Class> CreateAsync(CreateClassDto dto)
        {
            var entity = new Class
            {
                ProgramId = dto.ProgramId,
                Name = dto.Name,
                DisplayOrder = dto.DisplayOrder
            };

            _context.Classes.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Class?> UpdateAsync(int id, UpdateClassDto dto)
        {
            var entity = await _context.Classes.FindAsync(id);

            if (entity == null) return null;

            entity.ProgramId = dto.ProgramId;
            entity.Name = dto.Name;
            entity.DisplayOrder = dto.DisplayOrder;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Classes.FindAsync(id);

            if (entity == null) return false;

            _context.Classes.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
