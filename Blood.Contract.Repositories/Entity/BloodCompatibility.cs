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
    public class BloodCompatibility : BaseEntity
    {
        public int DonorBloodTypeId { get; set; }
        public virtual BloodType DonorBloodType { get; set; }

        public int RecipientBloodTypeId { get; set; }
        public virtual BloodType RecipientBloodType { get; set; }

        public string TransfusionType { get; set; } // whole_blood, red_cells, plasma, platelets

        public string CompatibilityLevel { get; set; } // ideal, compatible, incompatible

        public string Notes { get; set; }
    }
}
