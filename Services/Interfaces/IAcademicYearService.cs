using SchoolApi.DTOs.AcademicYear;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IAcademicYearService
    {
        Task<List<AcademicYear>> GetAllAsync();

        Task<AcademicYear?> GetByIdAsync(int id);

        Task<AcademicYear> CreateAsync(CreateAcademicYearDto dto);

        Task<AcademicYear?> UpdateAsync(int id, UpdateAcademicYearDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
