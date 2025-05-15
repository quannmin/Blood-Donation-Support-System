using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class EmergencyNotification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int DonorId { get; set; }

        public string NotificationMethod { get; set; } // sms, email, call
        public string Status { get; set; } //'sent', 'delivered', 'read', 'responded', 'failed'
        public string Response { get; set; } // enum('accepted', 'declined', 'maybe', 'no_response')
        public DateTime SendAt { get; set; }

        [ForeignKey("RequestId")]
        public virtual BloodRequest BloodRequest { get; set; }

        [ForeignKey("DonorId")]
        public virtual DonorProfile DonorProfile { get; set; }
    }
}
