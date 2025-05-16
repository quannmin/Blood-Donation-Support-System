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
    public class RecipientProfile : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }

        public string MedicalCondition { get; set; }

        public bool Emergency { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<BloodRequest> BloodRequests { get; set; }
    }
}
