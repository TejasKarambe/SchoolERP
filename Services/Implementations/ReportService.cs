using Microsoft.EntityFrameworkCore;
using SchoolApi.DTOs.Reports;
using SchoolApi.Models.Data;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly SchoolDbContext _context;

        public ReportService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<StudentAttendanceSummaryDto> GetStudentAttendanceSummaryAsync(int studentId, DateTime? from, DateTime? to)
        {
            var student = await _context.Students.FindAsync(studentId);

            var query = _context.StudentAttendances.Where(a => a.StudentId == studentId);
            if (from.HasValue) query = query.Where(a => a.Date >= from.Value.Date);
            if (to.HasValue) query = query.Where(a => a.Date <= to.Value.Date);

            var records = await query.ToListAsync();

            return new StudentAttendanceSummaryDto
            {
                StudentId = studentId,
                StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "",
                AdmissionNumber = student?.AdmissionNumber ?? "",
                TotalDays = records.Count,
                PresentDays = records.Count(r => r.Status == "Present"),
                AbsentDays = records.Count(r => r.Status == "Absent"),
                LeaveDays = records.Count(r => r.Status == "Leave"),
                AttendancePercentage = records.Count > 0
                    ? Math.Round((double)records.Count(r => r.Status == "Present") / records.Count * 100, 2)
                    : 0
            };
        }

        public async Task<SectionAttendanceSummaryDto> GetSectionAttendanceSummaryAsync(int sectionId, DateTime date)
        {
            var section = await _context.Sections
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == sectionId);

            var records = await _context.StudentAttendances
                .Where(a => a.SectionId == sectionId && a.Date.Date == date.Date)
                .ToListAsync();

            var totalStudents = await _context.Students.CountAsync(s => s.SectionId == sectionId);

            return new SectionAttendanceSummaryDto
            {
                SectionId = sectionId,
                SectionName = section?.Name ?? "",
                ClassName = section?.Class?.Name ?? "",
                Date = date.Date,
                TotalStudents = totalStudents,
                PresentCount = records.Count(r => r.Status == "Present"),
                AbsentCount = records.Count(r => r.Status == "Absent"),
                LeaveCount = records.Count(r => r.Status == "Leave"),
                AttendancePercentage = totalStudents > 0
                    ? Math.Round((double)records.Count(r => r.Status == "Present") / totalStudents * 100, 2)
                    : 0
            };
        }

        public async Task<List<SectionAttendanceSummaryDto>> GetSectionAttendanceRangeAsync(int sectionId, DateTime from, DateTime to)
        {
            var results = new List<SectionAttendanceSummaryDto>();
            for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
            {
                results.Add(await GetSectionAttendanceSummaryAsync(sectionId, date));
            }
            return results;
        }

        public async Task<StaffAttendanceSummaryDto> GetStaffAttendanceSummaryAsync(int staffId, DateTime? from, DateTime? to)
        {
            var staff = await _context.Staff.FindAsync(staffId);

            var query = _context.StaffAttendances.Where(a => a.StaffId == staffId);
            if (from.HasValue) query = query.Where(a => a.Date >= from.Value.Date);
            if (to.HasValue) query = query.Where(a => a.Date <= to.Value.Date);

            var records = await query.ToListAsync();

            return new StaffAttendanceSummaryDto
            {
                StaffId = staffId,
                StaffName = staff != null ? $"{staff.FirstName} {staff.LastName}" : "",
                StaffCode = staff?.StaffCode ?? "",
                TotalDays = records.Count,
                PresentDays = records.Count(r => r.Status == "Present"),
                AbsentDays = records.Count(r => r.Status == "Absent"),
                LeaveDays = records.Count(r => r.Status == "Leave"),
                HalfDays = records.Count(r => r.Status == "HalfDay"),
                AttendancePercentage = records.Count > 0
                    ? Math.Round((double)records.Count(r => r.Status == "Present") / records.Count * 100, 2)
                    : 0
            };
        }

        public async Task<ExamResultReportDto> GetExamResultReportAsync(int examId)
        {
            var exam = await _context.Exams
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == examId);

            var results = await _context.ExamResults
                .Include(r => r.Student)
                .Where(r => r.ExamId == examId)
                .ToListAsync();

            var passed = results.Where(r => !r.IsAbsent && r.MarksObtained >= (exam?.PassingMarks ?? 0)).ToList();

            return new ExamResultReportDto
            {
                ExamId = examId,
                ExamName = exam?.Name ?? "",
                SubjectName = exam?.Subject?.Name ?? "",
                TotalMarks = exam?.TotalMarks ?? 0,
                PassingMarks = exam?.PassingMarks ?? 0,
                TotalStudents = results.Count,
                PassedCount = passed.Count,
                FailedCount = results.Count(r => !r.IsAbsent && r.MarksObtained < (exam?.PassingMarks ?? 0)),
                AbsentCount = results.Count(r => r.IsAbsent),
                AverageMarks = results.Any(r => !r.IsAbsent)
                    ? Math.Round((double)results.Where(r => !r.IsAbsent).Average(r => r.MarksObtained), 2)
                    : 0,
                PassPercentage = results.Count > 0
                    ? Math.Round((double)passed.Count / results.Count * 100, 2)
                    : 0,
                Results = results.Select(r => new StudentResultDto
                {
                    StudentId = r.StudentId,
                    StudentName = $"{r.Student.FirstName} {r.Student.LastName}",
                    AdmissionNumber = r.Student.AdmissionNumber,
                    MarksObtained = r.MarksObtained,
                    Grade = r.Grade,
                    IsAbsent = r.IsAbsent,
                    IsPassed = !r.IsAbsent && r.MarksObtained >= (exam?.PassingMarks ?? 0)
                }).ToList()
            };
        }

        public async Task<List<StudentResultDto>> GetStudentAllResultsAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);

            return await _context.ExamResults
                .Include(r => r.Exam)
                    .ThenInclude(e => e.Subject)
                .Where(r => r.StudentId == studentId)
                .Select(r => new StudentResultDto
                {
                    StudentId = r.StudentId,
                    StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "",
                    AdmissionNumber = student != null ? student.AdmissionNumber : "",
                    MarksObtained = r.MarksObtained,
                    Grade = r.Grade,
                    IsAbsent = r.IsAbsent,
                    IsPassed = !r.IsAbsent && r.MarksObtained >= r.Exam.PassingMarks
                })
                .ToListAsync();
        }

        public async Task<FeeCollectionSummaryDto> GetFeeCollectionSummaryAsync(int academicYearId)
        {
            var academicYear = await _context.AcademicYears.FindAsync(academicYearId);

            var structures = await _context.FeeStructures
                .Where(f => f.AcademicYearId == academicYearId)
                .ToListAsync();

            var payments = await _context.FeePayments
                .Where(p => p.AcademicYearId == academicYearId)
                .ToListAsync();

            var totalFee = structures.Sum(s => s.Amount);
            var totalCollected = payments.Sum(p => p.AmountPaid);

            return new FeeCollectionSummaryDto
            {
                AcademicYearId = academicYearId,
                AcademicYearName = academicYear?.Name ?? "",
                TotalFeeAmount = totalFee,
                TotalCollected = totalCollected,
                TotalPending = totalFee - totalCollected,
                TotalStudents = payments.Select(p => p.StudentId).Distinct().Count(),
                PaidStudents = payments.Where(p => p.Status == "Paid").Select(p => p.StudentId).Distinct().Count(),
                PendingStudents = payments.Where(p => p.Status == "Pending" || p.Status == "Partial").Select(p => p.StudentId).Distinct().Count()
            };
        }

        public async Task<StudentFeeSummaryDto> GetStudentFeeSummaryAsync(int studentId, int academicYearId)
        {
            var student = await _context.Students.FindAsync(studentId);

            var structures = await _context.FeeStructures
                .Where(f => f.AcademicYearId == academicYearId)
                .ToListAsync();

            var payments = await _context.FeePayments
                .Where(p => p.StudentId == studentId && p.AcademicYearId == academicYearId)
                .ToListAsync();

            var details = structures.Select(s =>
            {
                var paid = payments.Where(p => p.FeeStructureId == s.Id).Sum(p => p.AmountPaid);
                return new FeeDetailDto
                {
                    FeeType = s.FeeType,
                    FeeAmount = s.Amount,
                    AmountPaid = paid,
                    AmountDue = s.Amount - paid,
                    Status = paid >= s.Amount ? "Paid" : paid > 0 ? "Partial" : "Pending"
                };
            }).ToList();

            return new StudentFeeSummaryDto
            {
                StudentId = studentId,
                StudentName = student != null ? $"{student.FirstName} {student.LastName}" : "",
                AdmissionNumber = student?.AdmissionNumber ?? "",
                FeeDetails = details,
                TotalFee = details.Sum(d => d.FeeAmount),
                TotalPaid = details.Sum(d => d.AmountPaid),
                TotalDue = details.Sum(d => d.AmountDue)
            };
        }
    }
}
