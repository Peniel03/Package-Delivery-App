using IdentityService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Interfaces
{
    /// <summary>
    /// Interface fo the user refresh userRefreshToken repository
    /// </summary>
    public interface IUserRefreshTokenRepository
    {
        /// <summary>
        /// Function to add the refresh userRefreshToken to the database
        /// </summary>
        /// <param name="userRefreshToken">the user refresh userRefreshToken that we want to save</param>
        void AddUserRefreshToken(UserRefreshToken userRefreshToken);

        /// <summary>
        /// Function to Delete a refresh userRefreshToken from the database
        /// </summary>
        /// <param name="userRefreshToken">the user refresh userRefreshToken</param>
        void UpdateUserRefreshToken(UserRefreshToken userRefreshToken);

        /// <summary>
        /// Function to get a refresh userRefreshToken that exists in the database
        /// </summary>
        /// <param name="tokenRefreshed">The user refresh userRefreshToken that we want to get</param>
        /// <returns>An object <see cref="UserRefreshToken"/></returns>
        Task<UserRefreshToken> GetSavedUserRefreshTokensAsync(string tokenRefreshed, CancellationToken token);

    }
}
