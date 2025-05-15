using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodProcessing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProcessId { get; set; }

        [Required]
        public int DonationId { get; set; }

        [Required]
        public string ProcessingType { get; set; } // whole_blood, component_separation

        [Column(TypeName = "decimal(6, 2)")]
        public decimal BloodVolume { get; set; }

        public bool SeparatedComponents { get; set; }

        public string TestResults { get; set; }

        [Required]
        public string ProcessingStatus { get; set; } // in_progress, completed, failed

        [Required]
        public int ProcessedBy { get; set; }

        public bool AddedToInventory { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("ProcessId")]
        public virtual DonationProcess Process { get; set; }

        [ForeignKey("DonationId")]
        public virtual BloodDonation Donation { get; set; }

        [ForeignKey("ProcessedBy")]
        public virtual User Processor { get; set; }
    }
}