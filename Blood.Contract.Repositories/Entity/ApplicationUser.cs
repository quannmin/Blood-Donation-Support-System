using Microsoft.AspNetCore.Identity;
using Blood.Contract.Repositories.Entity;
using Blood.Core.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blood.Repositories.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; } // male, female, other

        public string? AvatarUrl { get; set; }

        public bool? Status { get; set; }

        public string FullName { get; set; } // Họ tên

        public int? BloodGroupId { get; set; } // ID nhóm máu

        public string? Address { get; set; } // Địa chỉ

        public DateTime? LastDonationDate { get; set; } // Ngày hiến máu cuối cùng

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset? DeletedTime { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual BloodGroup? BloodGroup { get; set; } // Liên kết với bảng BloodGroup
        public virtual ICollection<DonorAvailability> DonorAvailabilities { get; set; }
        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
        public virtual ICollection<Donation> Donations { get; set; }

    }
}
