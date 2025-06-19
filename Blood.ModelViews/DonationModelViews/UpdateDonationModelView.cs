using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonationModelViews
{
    public class UpdateDonationModelView
    {
        public int? UserId { get; set; }

        public int? BloodRequestId { get; set; }

        public int? Quantity { get; set; }

        public DateTime? DonationDate { get; set; }

        public string? Notes { get; set; }
    }
}
