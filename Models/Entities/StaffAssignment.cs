namespace SchoolApi.Models.Entities
{
    public class StaffAssignment : BaseEntity
    {
        public int StaffId { get; set; }

        public int SectionId { get; set; }

        public int? SubjectId { get; set; }

        public bool IsClassTeacher { get; set; }

        public Staff Staff { get; set; }

        public Section Section { get; set; }

        public Subject Subject { get; set; }
    }
}
