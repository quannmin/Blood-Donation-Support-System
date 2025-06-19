using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodCompatibilityModelViews
{
    public class CreateBloodCompatibilityModelView
    {
        [Required(ErrorMessage = "DonorBloodGroupId is required.")]
        public int DonorBloodGroupId { get; set; }

        [Required(ErrorMessage = "RecipientBloodGroupId is required.")]
        public int RecipientBloodGroupId { get; set; }

        [Required]
        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "Invalid blood component.")]
        public string BloodComponent { get; set; }

        [Required(ErrorMessage = "IsCompatible is required.")]
        public bool IsCompatible { get; set; }
    }
}
