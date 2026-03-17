namespace SchoolApi.DTOs.StaffAssignments
{
    public class CreateStaffAssignmentDto
    {
        public int StaffId { get; set; }
        public int SectionId { get; set; }
        public int? SubjectId { get; set; }
        public bool IsClassTeacher { get; set; } = false;
    }

    public class UpdateStaffAssignmentDto
    {
        public int? SubjectId { get; set; }
        public bool IsClassTeacher { get; set; } = false;
    }
}
