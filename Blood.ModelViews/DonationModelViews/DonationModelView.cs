using Blood.ModelViews.BloodRequestModelViews;
using Blood.ModelViews.UserModelViews.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonationModelViews
{
    public class DonationModelView
    {
        public int Id { get; set; }

        public UserResponseModel User { get; set; }
        public BloodRequestModelView BloodRequest { get; set; }

        public int Quantity { get; set; }

        public DateTime DonationDate { get; set; }

        public string? Notes { get; set; }
    }
}
