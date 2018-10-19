using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AssWAD.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Member class
    public class Member : IdentityUser
    {
        [PersonalData] public string Name { get; set; }
        [PersonalData] public string Address { get; set; }
    }
}