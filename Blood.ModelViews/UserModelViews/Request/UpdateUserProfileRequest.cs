using Microsoft.AspNetCore.Http;

namespace Blood.ModelViews.UserModelViews.Request
{
    public class UpdateUserProfileRequest
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public IFormFile? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } // male, female, other
        public int? BloodGroupId { get; set; } // ID nhóm máu

    }
}
