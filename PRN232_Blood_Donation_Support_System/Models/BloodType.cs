using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PRN232_Blood_Donation_Support_System.Models
{
    public class BloodType
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(5)]
        public string Name { get; set; } // A+, A-, B+, B-, AB+, AB-, O+, O-

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<DonorProfile> DonorProfiles { get; set; }
        public virtual ICollection<RecipientProfile> RecipientProfiles { get; set; }
        public virtual ICollection<BloodCompatibility> DonorCompatibilities { get; set; }
        public virtual ICollection<BloodCompatibility> RecipientCompatibilities { get; set; }
        public virtual ICollection<BloodInventory> BloodInventories { get; set; }
    }
}