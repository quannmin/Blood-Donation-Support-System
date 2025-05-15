using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class ProcessLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProcessId { get; set; }

        [Required]
        public string Stage { get; set; } // request_received, inventory_checked, donor_matched...

        public int? PerformedBy { get; set; }

        public string ActionDetails { get; set; }

        public DateTime Timestamp { get; set; }

        // Navigation properties
        [ForeignKey("ProcessId")]
        public virtual DonationProcess Process { get; set; }

        [ForeignKey("PerformedBy")]
        public virtual User Performer { get; set; }
    }
}