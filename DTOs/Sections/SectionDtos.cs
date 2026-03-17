namespace SchoolApi.DTOs.Sections
{
    public class CreateSectionDto
    {
        public int ClassId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ClassTeacherId { get; set; }
    }

    public class UpdateSectionDto
    {
        public string Name { get; set; } = string.Empty;
        public int? ClassTeacherId { get; set; }
    }
}
