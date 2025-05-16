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
    public class BloodInventory : BaseEntity
    {
        public int BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }

        public int? DonationId { get; set; }
        public virtual BloodDonation Donation { get; set; }

        public string BatchNumber { get; set; }

        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        public decimal QuantityML { get; set; }

        public int Units { get; set; }

        public DateTime CollectionDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public string StorageLocation { get; set; }

        [Required]
        public string Status { get; set; } // available, reserved, quarantined, used, discarded, expired

        public int? ReservationId { get; set; }
        public virtual BloodRequest Reservation { get; set; }

        public string DiscardedReason { get; set; }

        public bool Tested { get; set; }

        public string TestResults { get; set; }

        public string Notes { get; set; }


    }
}
