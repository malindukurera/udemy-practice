using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DAL.Model
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public virtual AppUser AppUser { get; set; }
        public virtual AppRole AppRole { get; set; }
    }
}
