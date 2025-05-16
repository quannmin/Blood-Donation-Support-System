using Blood.Repositories.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blood.Contract.Repositories.Entity
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
