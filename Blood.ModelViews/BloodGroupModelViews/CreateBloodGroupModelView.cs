using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodGroupModelViews
{
    public class CreateBloodGroupModelView
    {
        [Required(ErrorMessage = "BloodGroup Name is required.")]
        public string Name { get; set; }
    }
}
