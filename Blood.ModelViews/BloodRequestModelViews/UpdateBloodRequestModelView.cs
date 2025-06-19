using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blood.Core.Utils.SystemConstant;

namespace Blood.ModelViews.BloodRequestModelViews
{
    public class UpdateBloodRequestModelView : IValidatableObject
    {
        public int? BloodGroupId { get; set; }

        [RegularExpression("^(WholeBlood|RedBloodCells|Plasma|Platelets)$", ErrorMessage = "Invalid blood component.")]
        public string? BloodComponent { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int? Quantity { get; set; }

        public bool? IsEmergency { get; set; }

        public string? Status { get; set; }

        public int? RequestedById { get; set; }

        public string? Notes { get; set; }

        [RegularExpression("^(FromStock|FromDonor)$", ErrorMessage = "RequestSource must be either 'FromStock' or 'FromDonor'.")]
        public string? RequestSource { get; set; }

        public int? BloodUnitId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "QuantityFromStock must be greater than 0.")]
        public int? QuantityFromStock { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Status))
            {
                var validStatuses = new[]
                {
                BloodRequestStatus.Pending,
                BloodRequestStatus.Fulfilled,
                BloodRequestStatus.PartiallyFulfilled,
                BloodRequestStatus.Cancelled
            };

                if (!validStatuses.Contains(Status))
                {
                    yield return new ValidationResult("Invalid status value.", new[] { nameof(Status) });
                }
            }
        }
    }
}
