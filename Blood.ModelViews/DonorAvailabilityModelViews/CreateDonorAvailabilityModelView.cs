using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Blood.ModelViews.DonorAvailabilityModelViews
{
    public class CreateDonorAvailabilityModelView
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "AvailableFrom is required")]
        public DateTime AvailableFrom { get; set; }

        [Required(ErrorMessage = "AvailableTo is required")]
        public DateTime AvailableTo { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
