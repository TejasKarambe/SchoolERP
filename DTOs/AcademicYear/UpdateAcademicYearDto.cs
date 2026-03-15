namespace SchoolApi.DTOs.AcademicYear
{
    public class UpdateAcademicYearDto
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
