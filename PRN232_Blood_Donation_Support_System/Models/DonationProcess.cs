using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class DonationProcess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        public int? DonorId { get; set; }

        public int? InventoryId { get; set; }

        [Required]
        public string ProcessType { get; set; } // from_inventory, from_donor, hybrid

        [Required]
        public string CurrentStage { get; set; } // request_received, inventory_checked, donor_matching...

        public DateTime? AppointmentTime { get; set; } // Thời gian hẹn hiến máu

        public string HealthCheckResult { get; set; } // passed, failed, pending

        public DateTime? DonationCompletedAt { get; set; } // Thời điểm hoàn thành hiến máu

        public DateTime? ProcessingCompletedAt { get; set; } // Thời điểm hoàn thành xử lý máu

        public DateTime? TransfusionCompletedAt { get; set; } // Thời điểm hoàn thành truyền máu

        public int? StaffId { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("RequestId")]
        public virtual BloodRequest Request { get; set; }

        [ForeignKey("DonorId")]
        public virtual DonorProfile Donor { get; set; }

        [ForeignKey("InventoryId")]
        public virtual BloodInventory Inventory { get; set; }

        [ForeignKey("StaffId")]
        public virtual User Staff { get; set; }

        public virtual ICollection<ProcessLog> ProcessLogs { get; set; }
        public virtual ICollection<BloodProcessing> BloodProcessings { get; set; }
        public virtual ICollection<HealthCheck> HealthChecks { get; set; }

    }
}