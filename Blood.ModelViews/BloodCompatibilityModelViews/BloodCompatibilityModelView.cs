using Blood.ModelViews.BloodGroupModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.BloodCompatibilityModelViews
{
    public class BloodCompatibilityModelView
    {
        public int Id { get; set; }
        public BloodGroupModelView DonorBloodGroupModelView { get; set; }
        public BloodGroupModelView RecipientBloodGroupModelView { get; set; }

        public string BloodComponent { get; set; } // Loại máu: WholeBlood, RedBloodCells, Plasma, Platelets

        public bool IsCompatible { get; set; } // Có tương thích không
    }
}
