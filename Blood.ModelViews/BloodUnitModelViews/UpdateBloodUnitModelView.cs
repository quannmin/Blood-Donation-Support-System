using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodUnitModelViews
{
    public class UpdateBloodUnitModelView
    {
        public int? BloodGroupId { get; set; }

        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "BloodComponent must be one of: WholeBlood, RedBloodCells, Plasma, Platelets")]
        public string? BloodComponent { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
