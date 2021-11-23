using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DAL.Model
{
    public class AppRole : IdentityRole<int>
    {
        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
