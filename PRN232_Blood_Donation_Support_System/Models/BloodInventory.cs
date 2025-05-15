using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodInventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BloodTypeId { get; set; }

        public int? DonationId { get; set; }

        public string BatchNumber { get; set; }

        [Required]
        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        [Required, Column(TypeName = "decimal(6, 2)")]
        public decimal QuantityML { get; set; }

        [Required]
        public int Units { get; set; }

        [Required]
        public DateTime CollectionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string StorageLocation { get; set; }

        [Required]
        public string Status { get; set; } // available, reserved, quarantined, used, discarded, expired

        public int? ReservationId { get; set; }

        public string DiscardedReason { get; set; }

        public bool Tested { get; set; }

        public string TestResults { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodType { get; set; }

        [ForeignKey("DonationId")]
        public virtual BloodDonation Donation { get; set; }

        [ForeignKey("ReservationId")]
        public virtual BloodRequest Reservation { get; set; }
    }
}