using Blood.ModelViews.UserModelViews.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonorAvailabilityModelViews
{
    public class DonorAvailabilityModelView
    {
        public int Id { get; set; }
        public UserResponseModel User { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public bool IsActive { get; set; }
    }

}
