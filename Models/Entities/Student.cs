namespace SchoolApi.Models.Entities
{
    public class Student : BaseEntity
    {
        public string AdmissionNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime AdmissionDate { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public string PhoneNumber { get; set; }
    }
}
