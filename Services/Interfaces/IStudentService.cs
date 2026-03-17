using SchoolApi.DTOs.Students;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<List<Student>> GetBySectionAsync(int sectionId);
        Task<Student> CreateAsync(CreateStudentDto dto);
        Task<Student?> UpdateAsync(int id, UpdateStudentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
