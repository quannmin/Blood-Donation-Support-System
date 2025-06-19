using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodUnitModelViews
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateBloodUnitModelView
    {
        [Required(ErrorMessage = "BloodGroupId is required")]
        public int BloodGroupId { get; set; }

        [Required(ErrorMessage = "BloodComponent is required")]
        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "BloodComponent must be one of: WholeBlood, RedBloodCells, Plasma, Platelets")]
        public string BloodComponent { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required")]
        public DateTime ExpiryDate { get; set; }

    }

}
