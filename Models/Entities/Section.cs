namespace SchoolApi.Models.Entities
{
    public class Section : BaseEntity
    {
        public int ClassId { get; set; }

        public string Name { get; set; }

        public int? ClassTeacherId { get; set; }

        public Class Class { get; set; }

        public Staff ClassTeacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
