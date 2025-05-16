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
    public class ProcessLog : BaseEntity
    {
        public int ProcessId { get; set; }
        public virtual DonationProcess Process { get; set; }

        public string Stage { get; set; } // request_received, inventory_checked, donor_matched...

        public int? PerformedBy { get; set; }
        public virtual ApplicationUser Performer { get; set; }

        public string ActionDetails { get; set; }

        public DateTime Timestamp { get; set; }
       
    }
}
