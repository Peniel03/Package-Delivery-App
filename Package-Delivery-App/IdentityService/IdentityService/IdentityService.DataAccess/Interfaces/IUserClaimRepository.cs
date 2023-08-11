using IdentityService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Interfaces
{
    /// <summary>
    /// Interface For the user claim repository
    /// </summary>
    public interface IUserClaimRepository
    {
        /// <summary>
        /// Function to get the claims of the user
        /// </summary>
        /// <param name="id">The id of the user we want to get the claims from</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A list of object <see cref="UserClaim"/></returns>
        Task<List<UserClaim>> GetUserClaimsAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Function to update the claims of the user
        /// </summary>
        /// <param name="claims">The claims that we want to update</param>
        void UpdateUserClaim(List<UserClaim> claims);

    }
}
