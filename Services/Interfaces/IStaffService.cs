using SchoolApi.DTOs.Staff;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStaffService
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(int id);
        Task<Staff> CreateAsync(CreateStaffDto dto);
        Task<Staff?> UpdateAsync(int id, UpdateStaffDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
