using SchoolApi.DTOs.Attendance;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStaffAttendanceService
    {
        Task<List<StaffAttendance>> GetAllAsync();
        Task<StaffAttendance?> GetByIdAsync(int id);
        Task<List<StaffAttendance>> GetByStaffAsync(int staffId);
        Task<List<StaffAttendance>> GetByDateAsync(DateTime date);
        Task<StaffAttendance> CreateAsync(CreateStaffAttendanceDto dto);
        Task<StaffAttendance?> UpdateAsync(int id, UpdateStaffAttendanceDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
