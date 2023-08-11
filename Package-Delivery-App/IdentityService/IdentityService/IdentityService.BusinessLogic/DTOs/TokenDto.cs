using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.DTOs
{
    /// <summary>
    /// The token's data transfert object  
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// The token to get authorization
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// The token that will be used to refresh the access token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// The validity time in minutes
        /// </summary>
        public int TokenLifeTimeInMinutes { get; set; }

    }
}
