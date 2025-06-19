using Blood.Repositories.Entity;
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
    public class BloodRequest : BaseEntity
    {
        public int BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }

        public string BloodComponent { get; set; } // WholeBlood, RedBloodCells, Plasma, Platelets

        public int Quantity { get; set; } // Tổng số đơn vị cần

        public bool IsEmergency { get; set; }

        public string Status { get; set; } // Pending, Fulfilled, PartiallyFulfilled, Cancelled

        public int RequestedById { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }

        public DateTime RequestDate { get; set; }
        public DateTime? FulfilledDate { get; set; }

        public string Notes { get; set; }

        // Từ người hiến
        public virtual ICollection<Donation> Donations { get; set; }

        // ✅ Thêm mới: Loại yêu cầu - FromStock hoặc FromDonor
        public string RequestSource { get; set; } // FromStock | FromDonor

        // ✅ Nếu từ kho máu (FromStock), thì liên kết đến BloodUnit và số lượng từ kho
        public int? BloodUnitId { get; set; } // null nếu là FromDonor
        public virtual BloodUnit? BloodUnit { get; set; }

        public int? QuantityFromStock { get; set; } // null nếu là FromDonor
    }

}
