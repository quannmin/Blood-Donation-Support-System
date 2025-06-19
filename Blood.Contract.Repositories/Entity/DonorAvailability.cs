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
    public class DonorAvailability : BaseEntity
    {
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime AvailableFrom { get; set; } // Từ ngày

        public DateTime AvailableTo { get; set; } // Đến ngày

        public bool IsActive { get; set; } = true; // Có còn hiệu lực không
    }
}
