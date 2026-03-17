using SchoolApi.DTOs.Exams;
using SchoolApi.Models.Entities;

namespace SchoolApi.Services.Interfaces
{
    public interface IExamService
    {
        Task<List<Exam>> GetAllAsync();
        Task<Exam?> GetByIdAsync(int id);
        Task<List<Exam>> GetByClassAsync(int classId);
        Task<List<Exam>> GetByAcademicYearAsync(int academicYearId);
        Task<Exam> CreateAsync(CreateExamDto dto);
        Task<Exam?> UpdateAsync(int id, UpdateExamDto dto);
        Task<bool> DeleteAsync(int id);

        // Exam Results
        Task<List<ExamResult>> GetResultsByExamAsync(int examId);
        Task<List<ExamResult>> GetResultsByStudentAsync(int studentId);
        Task<ExamResult> CreateResultAsync(CreateExamResultDto dto);
        Task<List<ExamResult>> BulkCreateResultsAsync(BulkExamResultDto dto);
        Task<ExamResult?> UpdateResultAsync(int id, UpdateExamResultDto dto);
        Task<bool> DeleteResultAsync(int id);
    }
}
