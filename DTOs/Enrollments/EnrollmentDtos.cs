namespace SchoolApi.DTOs.Enrollments
{
    public class CreateStudentEnrollmentDto
    {
        public int StudentId { get; set; }
        public int SectionId { get; set; }
        public int AcademicYearId { get; set; }
    }
}
