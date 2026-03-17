using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Staff;
using SchoolApi.Models.Data;
using SchoolApi.Models.Entities;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly SchoolDbContext _context;

        public StaffService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Staff>> GetAllAsync()
        {
            return await _context.Staff.ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(int id)
        {
            return await _context.Staff
                .Include(s => s.Sections)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Staff> CreateAsync(CreateStaffDto dto)
        {
            var entity = new Staff
            {
                StaffCode = dto.StaffCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Role = dto.Role,
                Phone = dto.Phone,
                Email = dto.Email,
                JoiningDate = dto.JoiningDate
            };

            _context.Staff.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Staff?> UpdateAsync(int id, UpdateStaffDto dto)
        {
            var entity = await _context.Staff.FindAsync(id);
            if (entity == null) return null;

            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.Role = dto.Role;
            entity.Phone = dto.Phone;
            entity.Email = dto.Email;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Staff.FindAsync(id);
            if (entity == null) return false;

            _context.Staff.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
