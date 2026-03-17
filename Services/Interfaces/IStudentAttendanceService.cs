using SchoolApi.DTOs.Attendance;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStudentAttendanceService
    {
        Task<List<StudentAttendance>> GetAllAsync();
        Task<StudentAttendance?> GetByIdAsync(int id);
        Task<List<StudentAttendance>> GetByStudentAsync(int studentId);
        Task<List<StudentAttendance>> GetBySectionAndDateAsync(int sectionId, DateTime date);
        Task<StudentAttendance> CreateAsync(CreateStudentAttendanceDto dto);
        Task<List<StudentAttendance>> BulkCreateAsync(BulkStudentAttendanceDto dto);
        Task<StudentAttendance?> UpdateAsync(int id, UpdateStudentAttendanceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
