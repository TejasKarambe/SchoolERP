using SchoolApi.DTOs.Enrollments;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStudentEnrollmentService
    {
        Task<List<StudentEnrollment>> GetAllAsync();
        Task<StudentEnrollment?> GetByIdAsync(int id);
        Task<List<StudentEnrollment>> GetByStudentAsync(int studentId);
        Task<List<StudentEnrollment>> GetByAcademicYearAsync(int academicYearId);
        Task<List<StudentEnrollment>> GetBySectionAsync(int sectionId);
        Task<StudentEnrollment> CreateAsync(CreateStudentEnrollmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
