using Microsoft.AspNetCore.Identity;
using System;


namespace IdentityService.DataAccess.Models
{
    /// <summary>
    /// the user class inheritb from IdentityUser
    /// </summary>
    public class User : IdentityUser<int>
    {
        /// <summary>
        /// the first name of the user
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        /// the last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Navigation property for the claims of the user
        /// </summary>
        public virtual ICollection<UserClaim>? Claims { get; set; }
        /// <summary>
        /// The refresh Token for the user
        /// </summary>
        public virtual List<UserRefreshToken>? UserRefreshTokens { get; set; }

    }
}
