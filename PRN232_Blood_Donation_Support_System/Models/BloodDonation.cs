using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodDonation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DonorId { get; set; }
        public int? RequestId { get; set; }
        public int? HealthCheckId { get; set; }

        [Required]
        public DateTime DonationDate { get; set; }

        [Required]
        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        [Required, Column(TypeName = "decimal(5, 2)")]
        public decimal Quantity { get; set; }

        [Required]
        public string Status { get; set; } // scheduled, completed, cancelled, deferred

        public int? StaffId { get; set; }

        public bool InventoryAdded { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("HealthCheckId")]
        public virtual HealthCheck HealthCheck { get; set; }

        [ForeignKey("DonorId")]
        public virtual DonorProfile Donor { get; set; }

        [ForeignKey("RequestId")]
        public virtual BloodRequest Request { get; set; }

        [ForeignKey("StaffId")]
        public virtual User Staff { get; set; }

        public virtual BloodInventory BloodInventory { get; set; }
        public virtual BloodProcessing BloodProcessing { get; set; }

    }
}