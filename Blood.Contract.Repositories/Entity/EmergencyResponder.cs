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
    public class EmergencyResponder : BaseEntity
    {

        public int RequestId { get; set; }
        public virtual BloodRequest Request { get; set; }

        public int DonorId { get; set; }
        public virtual DonorProfile Donor { get; set; }

        public string ResponseStatus { get; set; }  //'accepted', 'on_the_way', 'arrived', 'donated', 'cancelled'
        public DateTime EstimatedArrivalTime { get; set; }
        public DateTime ActualArrivalTime { get; set; }
        public string notes { get; set; }
       
    }
}
