namespace SchoolApi.Models.Entities
{
    public class ExamResult : BaseEntity
    {
        public int ExamId { get; set; }

        public int StudentId { get; set; }

        public decimal MarksObtained { get; set; }

        public string? Grade { get; set; }

        public string? Remarks { get; set; }

        public bool IsAbsent { get; set; } = false;

        public Exam Exam { get; set; } = null!;

        public Student Student { get; set; } = null!;
    }
}
