using Blood.ModelViews.BloodGroupModelViews;
using Blood.ModelViews.BloodUnitModelViews;
using Blood.ModelViews.UserModelViews.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodRequestModelViews
{
    public class BloodRequestModelView
    {
        public int Id { get; set; }
        public BloodGroupModelView BloodGroup { get; set; }
        public string BloodComponent { get; set; }
        public int Quantity { get; set; }
        public bool IsEmergency { get; set; }
        public string Status { get; set; }
        public UserResponseModel RequestedBy { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? FulfilledDate { get; set; }
        public string Notes { get; set; }
        public string RequestSource { get; set; }
        public BloodUnitModelView? BloodUnit { get; set; }
        public int? QuantityFromStock { get; set; }
    }
}
