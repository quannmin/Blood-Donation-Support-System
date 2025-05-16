using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blood.Repositories.Entity;
using Blood.Core.Base;

namespace Blood.Contract.Repositories.Entity
{
    public class BloodDonation : BaseEntity
    {
        public int DonorId { get; set; }
        public virtual DonorProfile Donor { get; set; }

        public int? RequestId { get; set; }
        public virtual BloodRequest Request { get; set; }
        public int? HealthCheckId { get; set; }
        public virtual HealthCheck HealthCheck { get; set; }

        public DateTime DonationDate { get; set; }

        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        public decimal Quantity { get; set; }

        public string Status { get; set; } // scheduled, completed, cancelled, deferred

        public int? StaffId { get; set; }
        public virtual ApplicationUser Staff { get; set; }

        public bool InventoryAdded { get; set; }

        public string Notes { get; set; }

        public virtual BloodInventory BloodInventory { get; set; }
        public virtual BloodProcessing BloodProcessing { get; set; }

    }
}
