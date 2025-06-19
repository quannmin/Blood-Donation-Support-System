using Blood.Core.Base;
using Blood.Repositories.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Repositories.Entity
{
    public class BloodGroup : BaseEntity
    {
        public string Name { get; set; } // Tên nhóm máu (A+, A-, O+, ...)

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<BloodUnit> BloodUnits { get; set; }
        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
        public virtual ICollection<BloodCompatibility> DonorCompatibilities { get; set; } // Nhóm máu này hiến cho ai
        public virtual ICollection<BloodCompatibility> RecipientCompatibilities { get; set; } // Nhóm máu này nhận từ ai
    }
}
