using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blood.Core.Base;

namespace Blood.Contract.Repositories.Entity
{
    public class BloodUnit : BaseEntity
    {
        public int BloodGroupId { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }

        public string BloodComponent { get; set; } // Loại máu: WholeBlood, RedBloodCells, Plasma, Platelets

        public int Quantity { get; set; } // Số đơn vị có sẵn

        public DateTime ExpiryDate { get; set; } // Ngày hết hạn

        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
    }
}
