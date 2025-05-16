using Microsoft.AspNetCore.Identity;
using Blood.Contract.Repositories.Entity;
using Blood.Core.Utils;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blood.Repositories.Entity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } // male, female, other

        public string Address { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public string AvatarUrl { get; set; }

        public bool IsVerified { get; set; }

        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset LastUpdatedTime { get; set; } = DateTime.Now;
        public DateTimeOffset? DeletedTime { get; set; }

        public string? RefreshToken { get; set; }
        public DateTimeOffset RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public virtual DonorProfile DonorProfile { get; set; }
        public virtual RecipientProfile RecipientProfile { get; set; }
        public virtual ICollection<BloodDonation> BloodDonations { get; set; }
        public virtual ICollection<BloodRequest> BloodRequests { get; set; }

    }
}
