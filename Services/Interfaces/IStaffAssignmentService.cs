using SchoolApi.DTOs.StaffAssignments;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IStaffAssignmentService
    {
        Task<List<StaffAssignment>> GetAllAsync();
        Task<StaffAssignment?> GetByIdAsync(int id);
        Task<List<StaffAssignment>> GetByStaffAsync(int staffId);
        Task<List<StaffAssignment>> GetBySectionAsync(int sectionId);
        Task<StaffAssignment> CreateAsync(CreateStaffAssignmentDto dto);
        Task<StaffAssignment?> UpdateAsync(int id, UpdateStaffAssignmentDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
