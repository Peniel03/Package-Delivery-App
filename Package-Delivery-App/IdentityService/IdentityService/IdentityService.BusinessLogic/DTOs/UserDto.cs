using IdentityService.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.DTOs
{
    /// <summary>
    /// The user's data transfert object
    /// </summary>
    public class UserDto : IdentityUser<int>
    {
        /// <summary>
        /// The first Name of the user 
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The claims of the user
        /// </summary>
        public virtual ICollection<UserClaim>? Claims { get; set; }

    }
}
