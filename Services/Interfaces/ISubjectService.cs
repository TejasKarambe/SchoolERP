using SchoolApi.DTOs.Subjects;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject> CreateAsync(CreateSubjectDto dto);
        Task<Subject?> UpdateAsync(int id, UpdateSubjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
