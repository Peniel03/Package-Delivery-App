using IdentityService.BusinessLogic.DTOs;
using IdentityService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The auhtorization service to manage the authorization of users
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Function to authorize the user to get token
        /// </summary>
        /// <param name="email">The email of the user that wants to get authorization</param>
        /// <param name="password">The password of the user</param>
        /// <param name="cancellationToken">The xancellation token</param>
        /// <returns>A <see cref="Task"/> That contains a <see cref="TokenDto"/><</returns>
        Task<TokenDto> AuthorizeAsync(string email, string password, CancellationToken cancellationToken);

        /// <summary>
        /// Function to verify if the user is valid
        /// </summary>
        /// <param name="email">The email of the user that we want verify credentials</param>
        /// <param name="password">The password of the user</param>
        /// <returns>A <see cref="Task"/> That return true if the user is valid and false in 
        ///  other cases </returns>
        Task<User> ValidateUserAsync(string email, string password);

        /// <summary>
        /// Function to refresh a token that has expired
        /// </summary>
        /// <param name="token">The token that has expired</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <see cref="TokenDto"/></returns>
        Task<TokenDto> RefreshTokenAsync(string token, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the claims of the user
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A List of <see cref="UserClaim"/></returns>
        Task<List<UserClaim>> GetUserClaimsAsync(int id, CancellationToken cancellationToken);

    }
}
