using SchoolApi.DTOs.Programs;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IProgramService
    {
        Task<List<ProgramEntity>> GetAllAsync();

        Task<ProgramEntity?> GetByIdAsync(int id);

        Task<ProgramEntity> CreateAsync(CreateProgramDto dto);

        Task<ProgramEntity?> UpdateAsync(int id, UpdateProgramDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
