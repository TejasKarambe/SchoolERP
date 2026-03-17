namespace SchoolApi.DTOs.Subjects
{
    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }

    public class UpdateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
