using SchoolApi.DTOs.Classes;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IClassService
    {
        Task<List<Class>> GetAllAsync();

        Task<Class?> GetByIdAsync(int id);

        Task<Class> CreateAsync(CreateClassDto dto);

        Task<Class?> UpdateAsync(int id, UpdateClassDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
