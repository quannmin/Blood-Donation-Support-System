using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Core.Utils
{
    public class SystemConstant
    {
        public static int PAGE_SIZE = 10;

        public class BloodComponent
        {
            public const string WholeBlood = "WholeBlood";
            public const string RedBloodCells = "RedBloodCells";
            public const string Plasma = "Plasma";
            public const string Platelets = "Platelets";
        }

        public class RequestSource
        {
            public const string FromStock = "FromStock";
            public const string FromDonor = "FromDonor";
        }

        public class BloodRequestStatus
        {
            public const string Pending = "Pending";
            public const string Fulfilled = "Fulfilled";
            public const string PartiallyFulfilled = "PartiallyFulfilled";
            public const string Cancelled = "Cancelled";
        }
    }
}
