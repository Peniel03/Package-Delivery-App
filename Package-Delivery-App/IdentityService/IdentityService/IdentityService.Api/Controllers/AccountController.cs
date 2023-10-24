using AutoMapper;
using IdentityService.BusinessLogic.DTOs;
using IdentityService.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    /// <summary>
    /// The account controller
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IAccountService _accountService;
        private readonly ISessionService _sessionService;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountController"/>
        /// </summary>
        /// <param name="authorizationService">The authorization service</param>
        /// <param name="accountService">The account service</param>
        /// <param name="sessionService">The session service</param>
        public AccountController(IAuthorizationService authorizationService, IAccountService accountService, ISessionService sessionService)
        {
            _authorizationService = authorizationService;
            _accountService = accountService;
            _sessionService = sessionService;
        }

        /// <summary>
        /// Function for authorizing the user to get acces to the application
        /// </summary>
        /// <param name="user">the user that we want to authorize</param>
        /// <param name="cancellationToken">the cancellationToken</param>
        /// <returns></returns>
        [HttpPost("authorize/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Authorize([FromBody] AuthorizeUserDto user, CancellationToken cancellationToken)
        {
            var tokenAccess = await _authorizationService.AuthorizeAsync(user.Email, user.Password, cancellationToken);
            return Ok(tokenAccess);
        }

        /// <summary>
        /// Function To create a New User 
        /// </summary> 
        /// <param name="user">The user that we want to create</param>
        /// <param name="cancellationToken">Cancellation token from the HTTP request</param>
        /// <returns>Returns type (IActionResult) OK if the user has successfully
        /// been created or badRequest in the other condition</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var result = await _accountService.CreateUserAsync(user, user.Password);
            return Ok(result);
        }

        /// <summary>
        /// Function to get the user by id.
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="usergetrequest">the user we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK the user exists and BadRequest if not </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
        {
            var result = await _accountService.GetUserByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Function to get all users
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK the users exist and BadRequest if not</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var userList = await _accountService.GetAllUserAsync(cancellationToken);
            return Ok(userList);
        }

        /// <summary>
        /// Function to logout the user 
        /// </summary>
        /// <returns>IActionResult(Ok) if the user is loggedout or 
        /// BadRequest if he is not logged out</returns>
        [HttpGet("logout/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
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
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto user, CancellationToken cancellationToken)
        {
            var checkedUser = await _accountService.UpdateUserAsync(user);
            return Ok(checkedUser);
        }

        /// <summary>
        /// Function to Delete the user 
        /// </summary>
        /// <param name="user">the user that we want to delete</param>
        /// <param name="cancellationToken">the cancellation token from the http request</param>
        /// <returns>OK if the user has been deleted and BadRequest if he has not been
        /// Deleted</returns>
        [HttpDelete("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
        {
            var checkedUser = await _accountService.DeleteUserAsync(id);
            return Ok(checkedUser);
        }

        /// <summary>
        ///  Function to update the password of the user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="newPassword">he new password of the user</param>
        /// <param name="cancellationToken">Cancellation token from the HTTP request</param>
        /// <returns>OK if the user has been updated or BadRequest if the user 
        /// has not been updated</returns>
        [HttpPut("{newPassword}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePassword([FromBody] UserUpdatePasswordDto user, string newPassword, CancellationToken cancellationToken)
        {
            var result = await _accountService.UpdatePasswordAsync(user, user.Password, newPassword);
            return Ok(result);
        }

        /// <summary>
        /// Function to refresh the token of the user 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="token">cancellation token from the HTTP request</param>
        /// <returns>return a new Token to the user</returns>
        [HttpGet("{refreshToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var newToken = await _authorizationService.RefreshTokenAsync(refreshToken, cancellationToken);
            return Ok(newToken);
        }

        /// <summary>
        /// Function to get the claim of the user
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="token">cancellation token from the HTTP request</param>
        /// <returns>the claim of the user</returns>
        [HttpGet("claim/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserClaim(int id, CancellationToken token)
        {
            var claims = await _authorizationService.GetUserClaimsAsync(id, token);
            return Ok(claims);
        }
    }
}
