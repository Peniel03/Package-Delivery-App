using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Repositories
{
    /// <summary>
    /// The user refresh repository for crud operations for the user refresh cancellationToken table
    /// </summary>
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly IdentityContext _identityContext;
        private readonly DbSet<UserRefreshToken> _userRefreshTokens;
        private readonly ILogger<UserRefreshTokenRepository> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="UserRefreshRepository"/>
        /// </summary>
        /// <param name="identityContext">The data base Context of the application</param>
        /// <param name="logger">The logger</param>
        public UserRefreshTokenRepository(IdentityContext identityContext, ILogger<UserRefreshTokenRepository> logger)
        {
            _identityContext = identityContext;
            _userRefreshTokens = _identityContext.Set<UserRefreshToken>();
            _logger = logger;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="userRefreshToken">The cancellationToken that we want to add</param>
        public void AddUserRefreshToken(UserRefreshToken userRefreshToken)
        {
            var result = _userRefreshTokens.Add(userRefreshToken);

            _logger.LogInformation("Added a user refresh Token");
        }

        /// <summary>
        ///  <inheritdoc/>
        /// </summary>
        /// <param name="tokenRefreshed">The refreshed cancellationToken that we want to get</param>
        /// <param name="cancellationToken">The cancellation cancellationToken</param>
        /// <returns></returns>

        public Task<UserRefreshToken?> GetSavedUserRefreshTokensAsync(string tokenRefreshed, CancellationToken cancellationToken)
        {
            return _userRefreshTokens.Include(i => i.User)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync(x => x.RefreshToken == tokenRefreshed, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="userRefreshToken">The user refresh cancellationToken to update</param>
        public void UpdateUserRefreshToken(UserRefreshToken userRefreshToken)
        {
            _userRefreshTokens.Update(userRefreshToken);

            _logger.LogInformation("Deleted the refresh cancellationToken");
        }

    }
}
