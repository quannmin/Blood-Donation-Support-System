
using Blood.ModelViews.BloodGroupModelViews;

namespace Blood.ModelViews.AuthModelViews.Response
{
    public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } // male, female, other
        public bool? Status { get; set; }

        public BloodGroupModelView? BloodGroup { get; set; } // ID nhóm máu

        public string? Address { get; set; } // Địa chỉ

        public DateTime? LastDonationDate { get; set; }

        public string? Role { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }
        public string AccessToken { get; set; }
        public DateTimeOffset AccessTokenExpiredTime { get; set; }
    }
}
