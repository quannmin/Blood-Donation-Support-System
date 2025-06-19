using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodCompatibilityModelViews
{
    public class UpdateBloodCompatibilityModelView
    {
        public int? DonorBloodGroupId { get; set; }
        public int? RecipientBloodGroupId { get; set; }

        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "Invalid blood component.")]
        public string? BloodComponent { get; set; }

        public bool? IsCompatible { get; set; }
    }
}
