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
    public class BloodProcessing : BaseEntity
    {
        public int ProcessId { get; set; }
        public virtual DonationProcess Process { get; set; }
        public int DonationId { get; set; }
        public virtual BloodDonation Donation { get; set; }

        public string ProcessingType { get; set; } // whole_blood, component_separation

        public decimal BloodVolume { get; set; }

        public bool SeparatedComponents { get; set; }

        public string TestResults { get; set; }

        public string ProcessingStatus { get; set; } // in_progress, completed, failed

        public int ProcessedBy { get; set; }
        public virtual ApplicationUser Processor { get; set; }

        public bool AddedToInventory { get; set; }

        public string Notes { get; set; }
       
    }
}
