using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Models
{
    /// <summary>
    /// the claim of the user
    /// </summary>
    public class UserClaim : IdentityUserClaim<int>
    {
        /// <summary>
        /// Navigation property for the user
        /// </summary>
        public virtual User? User { get; set; }
    }
}
