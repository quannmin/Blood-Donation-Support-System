using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonationModelViews
{
    public class CreateDonationModelView
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BloodRequestId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required]
        public DateTime DonationDate { get; set; }

        public string? Notes { get; set; }

    }
}
