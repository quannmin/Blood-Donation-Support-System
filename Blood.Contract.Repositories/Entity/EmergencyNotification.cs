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
    public class EmergencyNotification : BaseEntity
    {
        public int RequestId { get; set; }
        public virtual BloodRequest BloodRequest { get; set; }

        public int DonorId { get; set; }
        public virtual DonorProfile DonorProfile { get; set; }

        public string NotificationMethod { get; set; } // sms, email, call
        public string Status { get; set; } //'sent', 'delivered', 'read', 'responded', 'failed'
        public string Response { get; set; } // enum('accepted', 'declined', 'maybe', 'no_response')
        public DateTime SendAt { get; set; }

    }
}
