using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonorProfileViews
{
    public class CreateDonorProfileModelView
    {
        [Required(ErrorMessage = "UserId is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "BloodTypeId is required.")]
        public int BloodTypeId { get; set; }

        [Range(30, 200, ErrorMessage = "Weight must be between 30 and 200 kg.")]
        public decimal? Weight { get; set; }

        [Range(100, 250, ErrorMessage = "Height must be between 100 and 250 cm.")]
        public decimal? Height { get; set; }

        [Required(ErrorMessage = "Health status is required.")]
        [StringLength(50, ErrorMessage = "Health status cannot exceed 50 characters.")]
        public string HealthStatus { get; set; } = "eligible";

        public DateTime? LastDonationDate { get; set; }
        public DateTime? NextAvailableDate { get; set; }
        public int DonationCount { get; set; } = 0;
        public bool IsAvailable { get; set; } = true;
        public bool IsEmergencyAvailable { get; set; } = false;

        [StringLength(100, ErrorMessage = "Preferred donation type cannot exceed 100 characters.")]
        public string PreferredDonationType { get; set; }

        [StringLength(1000, ErrorMessage = "Medical history cannot exceed 1000 characters.")]
        public string MedicalHistory { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters.")]
        public string Notes { get; set; }
    }
}
