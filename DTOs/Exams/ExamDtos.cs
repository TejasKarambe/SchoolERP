namespace SchoolApi.DTOs.Exams
{
    public class CreateExamDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // UnitTest, MidTerm, Final, Internal
        public int AcademicYearId { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public DateTime ExamDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
    }

    public class UpdateExamDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int TotalMarks { get; set; }
        public int PassingMarks { get; set; }
    }

    public class CreateExamResultDto
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public decimal MarksObtained { get; set; }
        public string? Grade { get; set; }
        public string? Remarks { get; set; }
        public bool IsAbsent { get; set; } = false;
    }

    public class BulkExamResultDto
    {
        public int ExamId { get; set; }
        public List<ExamResultEntryDto> Results { get; set; } = [];
    }

    public class ExamResultEntryDto
    {
        public int StudentId { get; set; }
        public decimal MarksObtained { get; set; }
        public string? Grade { get; set; }
        public string? Remarks { get; set; }
        public bool IsAbsent { get; set; } = false;
    }

    public class UpdateExamResultDto
    {
        public decimal MarksObtained { get; set; }
        public string? Grade { get; set; }
        public string? Remarks { get; set; }
        public bool IsAbsent { get; set; }
    }
}
