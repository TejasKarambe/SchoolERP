namespace SchoolApi.Models.Entities
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public string TargetAudience { get; set; } = string.Empty; // All, Students, Staff, Parents

        public bool IsPublished { get; set; } = false;

        public DateTime? PublishedAt { get; set; }

        public int CreatedByUserId { get; set; }

        public User CreatedBy { get; set; } = null!;
    }
}
