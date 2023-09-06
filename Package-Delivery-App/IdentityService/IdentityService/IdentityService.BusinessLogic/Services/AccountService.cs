using AutoMapper;
using IdentityService.BusinessLogic.DTOs;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.DataAccess.Data.SeedData;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using IdentityService.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Servcices
{
    /// <summary>
    /// the class that implements the account service
    /// </summary>
    public class AccountService : IAccountService
    {


        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        /// itializes a new instance of <see cref="AccountService"/>
        /// </summary>
        /// <param name="userManager">The user manager from identity</param>
        /// <param name="mapper">The mapper to map object</param>
        /// <param name="logger">The logger to log information</param>
        /// <param name="userClaimRepository">the claim of the user</param>
        /// <param name="saveChangesRepository">the save changes</param>
        public AccountService(UserManager<User> userManager,
            IMapper mapper,
            ILoggerManager logger,
            IUserClaimRepository userClaimRepository,
            ISaveChangesRepository saveChangesRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
            _userClaimRepository = userClaimRepository;
            _saveChangesRepository = saveChangesRepository;   
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The user that we want to add</param>
        /// <param name="password">The use password</param>
        /// <exception cref="AlreadyExistException">When the user is not found</exception>
        /// <returns>A <see cref="Task"/></returns>
        public async Task<bool> CreateUserAsync(UserDto user, string password)
        {
            var mappedUser = _mapper.Map<User>(user);   
            using(_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(mappedUser.Email);    

               if(checkedUser != null)
                {
                    _logger.LogError("Error occured while creating user");

                    throw new AlreadyExistException("This user already exists");
                }

                //checkedUser.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(mappedUser, password);
                var role = new UserRole(UserRoleTypes.RolesTypes[0]);

                return result.Succeeded;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The user that we want to delete</param>
        /// <returns>A task <see cref="Task" that contains <seealso cref="UserDto"/>/></returns> 
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> DeleteUserAsync(UserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            using(_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(mappedUser.Email);
                if( checkedUser != null )
                {
                    _logger.LogError("Error occured while processing the delete request");

                    throw new NotFoundException("The user was not found");
                }
                var result = await _userManager.DeleteAsync(checkedUser);

                return user;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="UserDto"/></returns>
        /// <exception cref="NotFoundException">the not found exception</exception>
        public async Task<List<UserDto>> GetAllUserAsync(CancellationToken cancellationToken)
        {
            var list = await _userManager.Users.ToListAsync(cancellationToken);
            if(list == null)
            {
                throw new NotFoundException("The users were not found");

            }
            var listDto = _mapper.Map<List<UserDto>>(list);

            return listDto; 
        }

        /// <summary>
        /// <inheritdoc/> 
        /// </summary>
        /// <param name="user">the user</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a <seealso cref="UserDto"/></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<UserDto> GetUserByIdAsync(UserDto user, CancellationToken cancellationToken)
        {
            var mapperUser = _mapper.Map<User>(user);
            var checkedUder = await _userManager.FindByIdAsync(mapperUser.Id.ToString());
            if( checkedUder != null )
            {
                throw new NotFoundException("The user was not found");

            }
            return _mapper.Map<UserDto>(checkedUder); 

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The user for whom we want to reset the password</param>
        /// <param name="password">The old user's password</param>
        /// <param name="newPassword">The new user's password</param>
        /// <exception cref="NotFoundException"></exception>
        /// /// <returns>A <see cref="Task"/> that contatins the state of the request (true if the password
        /// has been reseted and false in the other case)</returns>
        public async Task<bool> ResetPasswordAsync(UserDto user, string password, string newPassword)
        {
            using (_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(user.Email);

                if (checkedUser == null)
                {
                    _logger.LogError("Error occured while reseting password");

                    throw new NotFoundException("The user was not found");
                }

                var result = await _userManager.ChangePasswordAsync(checkedUser, password, newPassword);

                return result.Succeeded;
            }
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The user for whom we want to update the password</param>
        /// <param name="oldPassword">The old password of the user</param>
        /// <param name="newPassword">The new password of the user</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>The state of the request</returns>

        public async Task<bool> UpdatePasswordAsync(UserDto user, string oldPassword, string newPassword)
        {
            var mappedUser = _mapper.Map<User>(user);

            using (_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(user.Email);
                var isValid = await _userManager.CheckPasswordAsync(mappedUser, oldPassword);

                if (checkedUser == null || isValid == false)
                {
                    _logger.LogError("The user was not found or the password was not correct while updating");

                    throw new NotFoundException("The user was not found or the password was not correct");
                }

                var result = await _userManager.ChangePasswordAsync(checkedUser, oldPassword, newPassword);

                return result.Succeeded;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The updated user</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/></returns>
        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);

            using (_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(mappedUser.Email);

                if (checkedUser == null)
                {
                    _logger.LogError("Error occured while processing the request");

                    throw new NotFoundException("User not found");
                }

                checkedUser.FirstName = mappedUser.FirstName;
                checkedUser.LastName = mappedUser.LastName;
                checkedUser.PhoneNumber = mappedUser.PhoneNumber;

                var result = await _userManager.UpdateAsync(checkedUser);

                return result.Succeeded;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="user">The user that we want to update the claims</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A <see cref="Task"/></returns>
        /// <exception cref="NotFoundException">the exception thrown when the user is not found</exception>
        public async Task UpdateUserClaimsAsync(UserDto user, CancellationToken cancellationToken)
        {
            using (_userManager)
            {
                var checkedUser = await _userManager.FindByEmailAsync(user.Email);

                if (checkedUser == null)
                {
                    _logger.LogError("Error occured while processing the update user claims request");

                    throw new NotFoundException("The user was not found");
                }

                var claims = await _userClaimRepository.GetUserClaimsAsync(checkedUser.Id, cancellationToken);

                if (claims == null)
                {
                    _logger.LogError("Error occured while processing the update user claims request");

                    throw new NotFoundException("The claims are null here");
                }

                _userClaimRepository.UpdateUserClaim(claims);

                await _saveChangesRepository.SaveChangesAsync(cancellationToken);

            }
        }

    }
}
