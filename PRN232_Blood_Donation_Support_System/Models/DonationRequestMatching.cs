using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class DonationRequestMatching
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int DonorId { get; set; }

        public DateTime MatchingDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "matched"; // matched, contacted, accepted, declined, completed

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("RequestId")]
        public virtual BloodRequest Request { get; set; }

        [ForeignKey("DonorId")]
        public virtual DonorProfile Donor { get; set; }
    }
}