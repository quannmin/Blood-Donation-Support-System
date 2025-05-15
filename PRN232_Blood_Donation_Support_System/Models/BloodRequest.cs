using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RecipientId { get; set; }

        [Required]
        public int BloodTypeId { get; set; }

        [Required]
        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Urgency { get; set; } // normal, urgent, emergency

        public DateTime RequiredBy { get; set; }

        public string Reason { get; set; }

        public string PatientCondition { get; set; }

        public string ContactPerson { get; set; }

        public string ContactPhone { get; set; }

        [Required]
        public string Status { get; set; } // pending, matching, partially_fulfilled, fulfilled, cancelled

        public int FulfilledUnits { get; set; }

        public int? StaffId { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("RecipientId")]
        public virtual RecipientProfile Recipient { get; set; }

        [ForeignKey("BloodTypeId")]
        public virtual BloodType BloodType { get; set; }

        [ForeignKey("StaffId")]
        public virtual User Staff { get; set; }

        public virtual ICollection<BloodDonation> BloodDonations { get; set; }
        public virtual ICollection<DonationProcess> DonationProcesses { get; set; }
        public virtual ICollection<DonationRequestMatching> DonationRequestMatchings { get; set; }

        // Trong BloodRequest và DonorProfile
        public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; }
        public virtual ICollection<EmergencyResponder> EmergencyResponders { get; set; }
    }
}