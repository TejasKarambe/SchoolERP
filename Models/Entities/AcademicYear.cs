namespace SchoolApi.Models.Entities
{
    public class AcademicYear : BaseEntity
    {
        public string Name { get; set; }   // Example: 2025-2026

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public ICollection<StudentEnrollment> StudentEnrollments { get; set; }
    }
}
