namespace SchoolApi.DTOs.Notifications
{
    public class CreateNotificationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = "All"; // All, Students, Staff, Parents
        public bool IsPublished { get; set; } = true;
    }

    public class UpdateNotificationDto
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
    }
}
