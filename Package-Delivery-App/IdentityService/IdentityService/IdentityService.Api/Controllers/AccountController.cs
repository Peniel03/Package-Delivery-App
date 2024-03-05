using AutoMapper;
using IdentityService.Api.Request;
using IdentityService.BusinessLogic.DTOs;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityService.Api.Controllers
{
    /// <summary>
    /// The account controller
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountController"/>
        /// </summary>
        /// <param name="accountService">The account service</param>
        /// <param name="sessionService">The session service</param>
        /// <param name="mapper">The mapper</param>
        public AccountController(IAccountService accountService, ISessionService sessionService, IMapper mapper)
        {
            _accountService = accountService;
            _sessionService = sessionService;
            _mapper = mapper;
        }

        /// <summary>
        /// Function To create a New User 
        /// </summary> 
        /// <param name="user">The user that we want to create</param>
        /// <param name="cancellationToken">Cancellation token from the HTTP request</param>
        /// <returns>Returns type (IActionResult) OK if the user has successfully
        /// been created or badRequest in the other condition</returns>
        [HttpPost("create/")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest user, CancellationToken cancellationToken)
        {
            var userMapped = _mapper.Map<UserDto>(user);
            var result = await _accountService.CreateUserAsync(userMapped, user.Password);

            if (result == false)
            {
                return BadRequest("The user was not created");
            }

            return Ok(user);
        }

        /// <summary>
        /// Function to get the user by id.
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="usergetrequest">the user we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK the user exists and BadRequest if not </returns>
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(int id, [FromBody] UserGetRequest usergetrequest, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UserDto>(usergetrequest);
            user.Id = id;
            var result = await _accountService.GetUserByIdAsync(user, cancellationToken);
            if(result == null)
            {
                return BadRequest("this user does not exist");
            }
            return Ok(user);    
        }

        /// <summary>
        /// Function to get all users
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK the users exist and BadRequest if not</returns>
        [HttpGet("get-all/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var userList = await _accountService.GetAllUserAsync(cancellationToken);
            if(userList == null)
            {
                return BadRequest("there is no registered user yet");
            }

            return Ok(userList);
        }


        /// <summary>
        /// Function to logout the user 
        /// </summary>
        /// <param name="cancellationToken">the cancellationToken token</param>
        /// <returns>IActionResult(Ok) if the user is loggedout or 
        /// BadRequest if he is not logged out</returns>
        [HttpGet("logout/{cancellationToken}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            await _sessionService.LogoutAsync();

            return Ok("Successfully...");
        }

        /// <summary>
        /// Function to Update the user 
        /// </summary>
        /// <param name="user">the user that we want to update</param>
        /// <param name="cancellationToken">the cancellationToken</param>
        /// <returns>OK the user has been updated and BadRequest if has not been updated</returns>
        [HttpPut("update/")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest user, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            var checkedUser = await _accountService.UpdateUserAsync(mappedUser);

            if (checkedUser == false)
            {
                return BadRequest("The user was not updated");
            }

            return Ok(user);
        }

        /// <summary>
        /// Function to Delete the user 
        /// </summary>
        /// <param name="user">the user that we want to delete</param>
        /// <param name="cancellationToken">the cancellation token from the http request</param>
        /// <returns>OK if the user has been deleted and BadRequest if he has not been
        /// Deleted</returns>
        [HttpDelete("delete/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(UserUpdateRequest user, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<UserDto>(user);

            var checkedUser = await _accountService.DeleteUserAsync(mappedUser);

            if (checkedUser == null)
            {
                return BadRequest("The user was not deleted");
            }

            return Ok(user);
        }

        /// <summary>
        /// Function the Update the claims of the user 
        /// </summary>
        /// <param name="id">The id of the user </param>
        /// <param name="user">the user that we want to update</param>
        /// <param name="claims">the claim of the user that we want to update</param>
        /// <param name="cancellationToken">Cancellation token from the HTTP request</param>
        /// <returns></returns>
        [HttpPut("updateClaim/{id}/{claims}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateClaims(int id, [FromBody] UserUpdateRequest user, [FromRoute] List<Claim> claims, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            mappedUser.Id = id;
            var userClaims = _mapper.Map<List<UserClaim>>(claims);
            mappedUser.Claims = userClaims;

            await _accountService.UpdateUserClaimsAsync(mappedUser, cancellationToken);

            return Ok(user);
        }

        /// <summary>
        ///  Function to update the password of the user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword">he new password of the user</param>
        /// <param name="cancellationToken">Cancellation token from the HTTP request</param>
        /// <returns>OK if the user has been updated or BadRequest if the user 
        /// has not been updated</returns>
        [HttpPut("update-password/{newPassword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword([FromBody] UserUpdateRequest user, string newPassword, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            var result = await _accountService.UpdatePasswordAsync(mappedUser, user.Password, newPassword);

            if (result == false)
            {
                return BadRequest("The password of the user was not updated");
            }

            return Ok(user);
        }


        /// <summary>
        ///  Functon to reset the password of the user 
        /// </summary>
        /// <param name="user">the user that want to reset his password</param>
        /// <param name="newPassword">the new password of the user</param>
        /// <param name="cancellationToken">ancellation token from the HTTP request</param>
        /// <returns>Ok if the password has been updated and badrequest in
        /// the other case</returns>
        [HttpPut("reset-password/{newPassword}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] UserUpdateRequest user, string newPassword, CancellationToken cancellationToken)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            var result = await _accountService.ResetPasswordAsync(mappedUser, user.Password, newPassword);

            if (result == false)
            {
                return BadRequest("The password of the user was not updated");
            }

            user.Password = newPassword;

            return Ok(user);
        }


    }
}
