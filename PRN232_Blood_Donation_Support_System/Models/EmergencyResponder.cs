using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class EmergencyResponder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int DonorId { get; set; }
        
        public string ResponseStatus { get; set; }  //'accepted', 'on_the_way', 'arrived', 'donated', 'cancelled'
        public DateTime EstimatedArrivalTime { get; set; }
        public DateTime ActualArrivalTime { get; set; }
        public string notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ForeignKey("RequestId")]
        public virtual BloodRequest Request { get; set; }

        [ForeignKey("DonorId")]
        public virtual DonorProfile Donor { get; set; }
    }
}
