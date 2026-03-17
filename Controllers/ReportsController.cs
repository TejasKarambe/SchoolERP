using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("attendance/student/{studentId}")]
        public async Task<IActionResult> GetStudentAttendance(
            int studentId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            return Ok(await _service.GetStudentAttendanceSummaryAsync(studentId, from, to));
        }

        [HttpGet("attendance/section/{sectionId}/date/{date}")]
        public async Task<IActionResult> GetSectionAttendanceByDate(int sectionId, DateTime date)
        {
            return Ok(await _service.GetSectionAttendanceSummaryAsync(sectionId, date));
        }

        [HttpGet("attendance/section/{sectionId}/range")]
        public async Task<IActionResult> GetSectionAttendanceRange(
            int sectionId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to)
        {
            return Ok(await _service.GetSectionAttendanceRangeAsync(sectionId, from, to));
        }

        [HttpGet("attendance/staff/{staffId}")]
        public async Task<IActionResult> GetStaffAttendance(
            int staffId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            return Ok(await _service.GetStaffAttendanceSummaryAsync(staffId, from, to));
        }

        [HttpGet("exams/{examId}/results")]
        public async Task<IActionResult> GetExamResultReport(int examId)
        {
            return Ok(await _service.GetExamResultReportAsync(examId));
        }

        [HttpGet("students/{studentId}/results")]
        public async Task<IActionResult> GetStudentAllResults(int studentId)
        {
            return Ok(await _service.GetStudentAllResultsAsync(studentId));
        }

        [HttpGet("fees/academic-year/{academicYearId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFeeCollectionSummary(int academicYearId)
        {
            return Ok(await _service.GetFeeCollectionSummaryAsync(academicYearId));
        }

        [HttpGet("fees/student/{studentId}/academic-year/{academicYearId}")]
        public async Task<IActionResult> GetStudentFeeSummary(int studentId, int academicYearId)
        {
            return Ok(await _service.GetStudentFeeSummaryAsync(studentId, academicYearId));
        }
    }
}
