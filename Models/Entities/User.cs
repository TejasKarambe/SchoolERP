namespace SchoolApi.Models.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // Admin, Teacher, Staff, Parent

        public int? StaffId { get; set; }

        public Staff? Staff { get; set; }

        public bool IsActive { get; set; } = true;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }
    }
}
