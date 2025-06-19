using Blood.ModelViews.BloodGroupModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodUnitModelViews
{
    public class BloodUnitModelView
    {
        public int Id { get; set; }
        public BloodGroupModelView BloodGroup { get; set; }
        public string BloodComponent { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
