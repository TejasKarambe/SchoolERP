namespace SchoolApi.Models.Entities
{
    public class StudentEnrollment : BaseEntity
    {
        public int StudentId { get; set; }

        public int SectionId { get; set; }

        public int AcademicYearId { get; set; }

        public Student Student { get; set; }

        public Section Section { get; set; }

        public AcademicYear AcademicYear { get; set; }
    }
}
