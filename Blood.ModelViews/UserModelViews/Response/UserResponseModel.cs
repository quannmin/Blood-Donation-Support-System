using Blood.ModelViews.BloodGroupModelViews;


namespace Blood.ModelViews.UserModelViews.Response
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? Status { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } // male, female, other
        public string? Address { get; set; } // Địa chỉ
        public DateTime? LastDonationDate { get; set; }
        public BloodGroupModelView? BloodGroup { get; set; } // ID nhóm máu

    }
}
