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
        /// function to get the user by id
        /// </summary>
        /// <param name="user">the user that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="UserDto"/></returns>
        Task<UserDto> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// function to get all users
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contaisn a list of <seealso cref="UserDto"/></returns>
        Task<List<UserDto>> GetAllUserAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Function to delete a user from the database
        /// </summary>
        /// <param name="user">The user that we want to delete</param>
        /// <returns>A <see cref="Task"/> that contains a <see cref="UserDto"/></returns>
        Task<DeleteUserDto> DeleteUserAsync(int id);

        /// <summary>
        /// Function to update the password of the user
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="oldPassword">The old password of the user</param>
        /// <param name="newPassword">The new password of the user</param>
        /// <returns>A <see cref="Task"/> That contains true if the password has been
        /// updated and false in other cases</returns></returns>
        Task<bool> UpdatePasswordAsync(UserUpdatePasswordDto user, string oldPassword, string newPassword);

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
