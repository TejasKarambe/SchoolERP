namespace SchoolApi.Models.Entities
{
    public class Exam : BaseEntity
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

        public AcademicYear AcademicYear { get; set; } = null!;

        public Class Class { get; set; } = null!;

        public Subject Subject { get; set; } = null!;

        public ICollection<ExamResult> ExamResults { get; set; } = [];
    }
}
