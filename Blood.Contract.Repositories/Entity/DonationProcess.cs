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
    public class DonationProcess : BaseEntity
    {
        public int RequestId { get; set; }
        public virtual BloodRequest Request { get; set; }

        public int? DonorId { get; set; }
        public virtual DonorProfile Donor { get; set; }

        public int? InventoryId { get; set; }
        public virtual BloodInventory Inventory { get; set; }

        public string ProcessType { get; set; } // from_inventory, from_donor, hybrid

         public string CurrentStage { get; set; } // request_received, inventory_checked, donor_matching...

        public DateTime? AppointmentTime { get; set; } // Thời gian hẹn hiến máu

        public string HealthCheckResult { get; set; } // passed, failed, pending

        public DateTime? DonationCompletedAt { get; set; } // Thời điểm hoàn thành hiến máu

        public DateTime? ProcessingCompletedAt { get; set; } // Thời điểm hoàn thành xử lý máu

        public DateTime? TransfusionCompletedAt { get; set; } // Thời điểm hoàn thành truyền máu

        public int? StaffId { get; set; }
        public virtual ApplicationUser Staff { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<ProcessLog> ProcessLogs { get; set; }
        public virtual ICollection<BloodProcessing> BloodProcessings { get; set; }
        public virtual ICollection<HealthCheck> HealthChecks { get; set; }

    }
}
