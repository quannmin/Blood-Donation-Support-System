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
    public class Donation : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int BloodRequestId { get; set; }
        public virtual BloodRequest BloodRequest { get; set; }

        public int Quantity { get; set; } // Số đơn vị đã hiến

        public DateTime DonationDate { get; set; } // Ngày hiến

        public string Notes { get; set; } // Ghi chú (ví dụ: địa điểm hiến)
    }
}
