using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;


namespace IdentityService.DataAccess.Repositories
{
    /// <summary>
    /// The user refresh repository for crud operations for the user refresh cancellationToken table
    /// </summary>
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly IdentityContext _identityContext;
        private readonly DbSet<UserRefreshToken> _userRefreshTokens;

        /// <summary>
        /// Initializes a new instance of <see cref="UserRefreshRepository"/>
        /// </summary>
        /// <param name="identityContext">The data base Context of the application</param>
        /// <param name="logger">The logger</param>
        public UserRefreshTokenRepository(IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _userRefreshTokens = _identityContext.Set<UserRefreshToken>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="userRefreshToken">The cancellationToken that we want to add</param>
        public void AddUserRefreshToken(UserRefreshToken userRefreshToken)
        {
            _userRefreshTokens.Add(userRefreshToken);

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

        }

    }
}
