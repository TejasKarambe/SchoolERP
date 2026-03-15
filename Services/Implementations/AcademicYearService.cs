using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.AcademicYear;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{


    public class AcademicYearService : IAcademicYearService
    {
        private readonly SchoolDbContext _context;

        public AcademicYearService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicYear>> GetAllAsync()
        {
            return await _context.AcademicYears.ToListAsync();
        }

        public async Task<AcademicYear?> GetByIdAsync(int id)
        {
            return await _context.AcademicYears.FindAsync(id);
        }

        public async Task<AcademicYear> CreateAsync(CreateAcademicYearDto dto)
        {
            var entity = new AcademicYear
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = dto.IsActive
            };

            _context.AcademicYears.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<AcademicYear?> UpdateAsync(int id, UpdateAcademicYearDto dto)
        {
            var entity = await _context.AcademicYears.FindAsync(id);

            if (entity == null) return null;

            entity.Name = dto.Name;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.IsActive = dto.IsActive;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AcademicYears.FindAsync(id);

            if (entity == null) return false;

            _context.AcademicYears.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
