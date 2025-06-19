using Blood.ModelViews.BloodGroupModelViews;
using Blood.ModelViews.RoleModelViews;

namespace Blood.ModelViews.UserModelViews.Response
{
    public class EmployeeResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; } // male, female, other
        public BloodGroupModelView? BloodGroup { get; set; } // ID nhóm máu

        public bool? Status { get; set; }

        public RoleModelView Role { get; set; }
    }
}
