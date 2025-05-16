using Blood.Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Repositories.Entity
{
    public class BloodType : BaseEntity
    {
        public string Name { get; set; } // A+, A-, B+, B-, AB+, AB-, O+, O-

        public string? Description { get; set; }

        public virtual ICollection<DonorProfile> DonorProfiles { get; set; }
        public virtual ICollection<RecipientProfile> RecipientProfiles { get; set; }
        public virtual ICollection<BloodCompatibility> DonorCompatibilities { get; set; }
        public virtual ICollection<BloodCompatibility> RecipientCompatibilities { get; set; }
        public virtual ICollection<BloodInventory> BloodInventories { get; set; }
        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
    }
}
