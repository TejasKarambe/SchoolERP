namespace SchoolApi.Models.Entities
{
    public class Staff : BaseEntity
    {
        public string StaffCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime JoiningDate { get; set; }

        public ICollection<Section> Sections { get; set; }
    }
}
