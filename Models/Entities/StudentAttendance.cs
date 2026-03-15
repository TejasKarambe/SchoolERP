namespace SchoolApi.Models.Entities
{
    public class StudentAttendance : BaseEntity
    {
        public int StudentId { get; set; }

        public int SectionId { get; set; }

        public DateTime Date { get; set; }

        public string Status { get; set; }   // Present / Absent / Leave

        public int MarkedByStaffId { get; set; }

        public Student Student { get; set; }

        public Section Section { get; set; }

        public Staff MarkedBy { get; set; }
    }
}
