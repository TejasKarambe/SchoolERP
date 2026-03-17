namespace SchoolApi.DTOs.Students
{
    public class CreateStudentDto
    {
        public string AdmissionNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int SectionId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class UpdateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int SectionId { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
