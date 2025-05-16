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
    public class HealthCheck : BaseEntity
    {
        public int? ProcessId { get; set; }
        public virtual DonationProcess Process { get; set; }

        public decimal? Hemoglobin { get; set; }

        public string BloodPressure { get; set; }

        public int? Pulse { get; set; }

        public decimal? Temperature { get; set; }

        public decimal? Weight { get; set; }

        public bool IsEligible { get; set; }

        public string ReasonIfIneligible { get; set; }

        public int PerformedBy { get; set; }
        public virtual ApplicationUser Performer { get; set; }

        public string Notes { get; set; }


        public virtual BloodDonation BloodDonation { get; set; }

    }
}
