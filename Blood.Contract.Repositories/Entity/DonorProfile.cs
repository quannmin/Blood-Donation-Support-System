using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blood.Repositories.Entity;
using Blood.Core.Base;

namespace Blood.Contract.Repositories.Entity
{
    public class DonorProfile : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public string HealthStatus { get; set; } // eligible, temporary_deferral, permanent_deferral

        public DateTime? LastDonationDate { get; set; }

        public DateTime? NextAvailableDate { get; set; }

        public int DonationCount { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsEmergencyAvailable { get; set; }

        public string PreferredDonationType { get; set; }

        public string MedicalHistory { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<BloodDonation> BloodDonations { get; set; }
        public virtual ICollection<DonationRequestMatching> DonationRequestMatchings { get; set; }
        public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; }
        public virtual ICollection<EmergencyResponder> EmergencyResponders { get; set; }
    }
}
