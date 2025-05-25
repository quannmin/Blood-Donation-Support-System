using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.ModelViews.DonorProfileViews
{
    public class DonorProfileModelView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BloodTypeId { get; set; }
        public string BloodTypeName { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string HealthStatus { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public DateTime? NextAvailableDate { get; set; }
        public int DonationCount { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsEmergencyAvailable { get; set; }
        public string PreferredDonationType { get; set; }
        public string MedicalHistory { get; set; }
        public string Notes { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastUpdatedTime { get; set; }
    }
}
