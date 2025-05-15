using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class DonorProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int BloodTypeId { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Weight { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
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

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodType { get; set; }

        public virtual ICollection<BloodDonation> BloodDonations { get; set; }
        public virtual ICollection<DonationRequestMatching> DonationRequestMatchings { get; set; }
        // Trong BloodRequest và DonorProfile
        public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; }
        public virtual ICollection<EmergencyResponder> EmergencyResponders { get; set; }
    }
}