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
        public int DonorBloodGroupId { get; set; }
        public virtual BloodGroup DonorBloodGroup { get; set; }

        public int RecipientBloodGroupId { get; set; }
        public virtual BloodGroup RecipientBloodGroup { get; set; }

        public string BloodComponent { get; set; } // Loại máu: WholeBlood, RedBloodCells, Plasma, Platelets

        public bool IsCompatible { get; set; } // Có tương thích không
    }
}
