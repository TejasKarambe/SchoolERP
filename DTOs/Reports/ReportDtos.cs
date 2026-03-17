namespace SchoolApi.DTOs.Reports
{
    public class StudentAttendanceSummaryDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
        public double AttendancePercentage { get; set; }
    }

    public class SectionAttendanceSummaryDto
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int TotalStudents { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
        public int LeaveCount { get; set; }
        public double AttendancePercentage { get; set; }
    }

    public class StaffAttendanceSummaryDto
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; } = string.Empty;
        public string StaffCode { get; set; } = string.Empty;
        public int TotalDays { get; set; }
        public int PresentDays { get; set; }
        public int AbsentDays { get; set; }
        public int LeaveDays { get; set; }
        public int HalfDays { get; set; }
        public double AttendancePercentage { get; set; }
    }

    public class ExamResultReportDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
        public int TotalStudents { get; set; }
        public int PassedCount { get; set; }
        public int FailedCount { get; set; }
        public int AbsentCount { get; set; }
        public double AverageMarks { get; set; }
        public double PassPercentage { get; set; }
        public List<StudentResultDto> Results { get; set; } = [];
    }

    public class StudentResultDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;
        public decimal MarksObtained { get; set; }
        public string? Grade { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsPassed { get; set; }
    }

    public class FeeCollectionSummaryDto
    {
        public int AcademicYearId { get; set; }
        public string AcademicYearName { get; set; } = string.Empty;
        public decimal TotalFeeAmount { get; set; }
        public decimal TotalCollected { get; set; }
        public decimal TotalPending { get; set; }
        public int TotalStudents { get; set; }
        public int PaidStudents { get; set; }
        public int PendingStudents { get; set; }
    }

    public class StudentFeeSummaryDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string AdmissionNumber { get; set; } = string.Empty;
        public List<FeeDetailDto> FeeDetails { get; set; } = [];
        public decimal TotalFee { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalDue { get; set; }
    }

    public class FeeDetailDto
    {
        public string FeeType { get; set; } = string.Empty;
        public decimal FeeAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
