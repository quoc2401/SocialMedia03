namespace SocialMedia03.WEB.Models
{
    public class UserReq
    {
        public string? Uuid { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Firstname { get; set; } = null!;
        public string? Lastname { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Hometown { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; } = null!;
    }
}
