using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Repositories
{
    /// <summary>
    /// The user claim repository for the crud operation of the user claim
    /// </summary>
    public class UserClaimRepository : IUserClaimRepository
    {

        private readonly IdentityContext _identityContext;
        private readonly DbSet<UserClaim> _claims;

        /// <summary>
        /// Initializes a new instance of <see cref="UserClaimRepository"/>
        /// </summary>
        /// <param name="identityContext">The database context</param>
        public UserClaimRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _claims = _identityContext.Set<UserClaim>();
        }

        /// <summary>
        /// Getting the user claims
        /// </summary>
        /// <param name="id">The id of the user that we want to get the claims</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>List of <see cref="UserClaim"/></returns>
        public Task<List<UserClaim>> GetUserClaimsAsync(int id, CancellationToken cancellationToken)
        {
            return _claims
                        .Where(userClaim => userClaim.Id == id)
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Update the claim of the user
        /// </summary>
        /// <param name="claims">The claims of the user </param>
         public void UpdateUserClaim(List<UserClaim> claims)
        {
            _claims.UpdateRange(claims);
        }
    }
}
