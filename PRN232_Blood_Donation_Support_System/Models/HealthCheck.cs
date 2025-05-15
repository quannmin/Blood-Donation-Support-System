using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class HealthCheck
    {
        [Key]
        public int Id { get; set; }
        public int? ProcessId { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Hemoglobin { get; set; }

        public string BloodPressure { get; set; }

        public int? Pulse { get; set; }

        [Column(TypeName = "decimal(3, 1)")]
        public decimal? Temperature { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? Weight { get; set; }

        [Required]
        public bool IsEligible { get; set; }

        public string ReasonIfIneligible { get; set; }

        [Required]
        public int PerformedBy { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("ProcessId")]
        public virtual DonationProcess Process { get; set; }

        [ForeignKey("PerformedBy")]
        public virtual User Performer { get; set; }

        public virtual BloodDonation BloodDonation { get; set; }

    }
}