using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodCompatibility
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DonorBloodTypeId { get; set; }

        [Required]
        public int RecipientBloodTypeId { get; set; }

        [Required]
        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        public string CompatibilityLevel { get; set; } // ideal, compatible, incompatible

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("DonorBloodTypeId")]
        public virtual BloodType DonorBloodType { get; set; }

        [ForeignKey("RecipientBloodTypeId")]
        public virtual BloodType RecipientBloodType { get; set; }
    }
}