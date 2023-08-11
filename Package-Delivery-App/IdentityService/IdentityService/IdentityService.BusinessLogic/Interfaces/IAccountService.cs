using IdentityService.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The account service to manage the accounts of users
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Function to add a new user to the database
        /// </summary>
        /// <param name="user">The use That we want to add</param>
        /// <param name="password">The password of the user</param>
        /// <returns>A <see cref="Task"/></returns>
        Task<bool> CreateUserAsync(UserDto user, string password);

        /// <summary>
        /// Function to delete a user from the database
        /// </summary>
        /// <param name="user">The user that we want to delete</param>
        /// <returns>A <see cref="Task"/> that contains a <see cref="UserDto"/></returns>
        Task<UserDto> DeleteUserAsync(UserDto user);

        /// <summary>
        /// Function to reset the password of the user
        /// </summary>
        /// <param name="user">The user for whom we want to reset the password</param>
        /// <param name="password">The password</param>
        /// <param name="newPassword">The new password</param>
        /// <returns>A <see cref="Task"/> that contains true if the password has been
        /// reseted and false in other cases</returns></returns>
        Task<bool> ResetPasswordAsync(UserDto user, string password, string newPassword);

        /// <summary>
        /// Function to update the password of the user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="oldPassword">The old password of the user</param>
        /// <param name="newPassword">The new password of the user</param>
        /// <returns>A <see cref="Task"/> That contains true if the password has been
        /// updated and false in other cases</returns></returns>
        Task<bool> UpdatePasswordAsync(UserDto user, string oldPassword, string newPassword);

        /// <summary>
        /// Function to update the user
        /// </summary>
        /// <param name="user">he user</param>
        /// <returns>A <see cref="Task"/> That contains the updated user 
        /// <see cref="UserDto"/></returns>
        Task<bool> UpdateUserAsync(UserDto user);

        /// <summary>
        /// Function to add the update the claims of the user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="cancellationToken">The Cancellation token</param>
        /// <returns>A <see cref="Task"/></returns>
        Task UpdateUserClaimsAsync(UserDto user, CancellationToken cancellationToken);


    }
}
