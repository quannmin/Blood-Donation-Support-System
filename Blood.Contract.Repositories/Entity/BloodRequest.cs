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
    public class BloodRequest : BaseEntity
    {
        public int RecipientId { get; set; }
        public virtual RecipientProfile Recipient { get; set; }
        public int BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }

        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        public int Quantity { get; set; }

        public string Urgency { get; set; } // normal, urgent, emergency

        public DateTime RequiredBy { get; set; }

        public string Reason { get; set; }

        public string PatientCondition { get; set; }

        public string ContactPerson { get; set; }

        public string ContactPhone { get; set; }

        public string Status { get; set; } // pending, matching, partially_fulfilled, fulfilled, cancelled

        public int FulfilledUnits { get; set; }

        public int? StaffId { get; set; }
        public virtual ApplicationUser Staff { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<BloodDonation> BloodDonations { get; set; }
        public virtual ICollection<DonationProcess> DonationProcesses { get; set; }
        public virtual ICollection<DonationRequestMatching> DonationRequestMatchings { get; set; }
        public virtual ICollection<EmergencyNotification> EmergencyNotifications { get; set; }
        public virtual ICollection<EmergencyResponder> EmergencyResponders { get; set; }
    }
}
