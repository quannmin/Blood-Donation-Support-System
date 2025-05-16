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
    public class DonationRequestMatching : BaseEntity
    {
        public int RequestId { get; set; }
        public virtual BloodRequest Request { get; set; }

        public int DonorId { get; set; }
        public virtual DonorProfile Donor { get; set; }

        public DateTime MatchingDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "matched"; // matched, contacted, accepted, declined, completed

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}
