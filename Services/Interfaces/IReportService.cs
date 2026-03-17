using SchoolApi.DTOs.Reports;

namespace SchoolApi.Services.Interfaces
{
    public interface IReportService
    {
        Task<StudentAttendanceSummaryDto> GetStudentAttendanceSummaryAsync(int studentId, DateTime? from, DateTime? to);
        Task<SectionAttendanceSummaryDto> GetSectionAttendanceSummaryAsync(int sectionId, DateTime date);
        Task<List<SectionAttendanceSummaryDto>> GetSectionAttendanceRangeAsync(int sectionId, DateTime from, DateTime to);
        Task<StaffAttendanceSummaryDto> GetStaffAttendanceSummaryAsync(int staffId, DateTime? from, DateTime? to);
        Task<ExamResultReportDto> GetExamResultReportAsync(int examId);
        Task<List<StudentResultDto>> GetStudentAllResultsAsync(int studentId);
        Task<FeeCollectionSummaryDto> GetFeeCollectionSummaryAsync(int academicYearId);
        Task<StudentFeeSummaryDto> GetStudentFeeSummaryAsync(int studentId, int academicYearId);
    }
}
