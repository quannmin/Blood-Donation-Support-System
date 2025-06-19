using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodRequestModelViews
{
    using System.ComponentModel.DataAnnotations;

    public class CreateBloodRequestModelView 
    {
        [Required]
        public int BloodGroupId { get; set; }

        [Required]
        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "Invalid blood component.")]
        public string BloodComponent { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required]
        public bool IsEmergency { get; set; }

        [Required]
        public int RequestedById { get; set; }

        public string? Notes { get; set; }

        [Required]
        [RegularExpression("^(FromStock|FromDonor)$", ErrorMessage = "RequestSource must be either 'FromStock' or 'FromDonor'.")]
        public string RequestSource { get; set; } // FromStock | FromDonor

        public int? BloodUnitId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "QuantityFromStock must be greater than 0.")]
        public int? QuantityFromStock { get; set; }

    }

}
