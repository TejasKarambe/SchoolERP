using SchoolApi.DTOs.Sections;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface ISectionService
    {
        Task<List<Section>> GetAllAsync();
        Task<Section?> GetByIdAsync(int id);
        Task<List<Section>> GetByClassAsync(int classId);
        Task<Section> CreateAsync(CreateSectionDto dto);
        Task<Section?> UpdateAsync(int id, UpdateSectionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
