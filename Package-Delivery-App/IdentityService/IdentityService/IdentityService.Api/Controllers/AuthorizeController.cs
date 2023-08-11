using IdentityService.Api.Request;
using IdentityService.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityService.Api.Controllers
{
    /// <summary>
    /// The authorize controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthorizeController"/>
        /// </summary>
        /// <param name="authorizationService">The authorization service</param>
        public AuthorizeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
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
        public async Task<IActionResult> Authorize([FromBody] UserLoginRequest user, CancellationToken cancellationToken)
        {
            var tokenAccess = await _authorizationService.AuthorizeAsync(user.Email, user.Password, cancellationToken);

            if (tokenAccess == null)
            {
                return Unauthorized("You are not authorized");
            }

            return Ok(tokenAccess);
        }

        /// <summary>
        /// Function to refresh the token of the user 
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="token">cancellation token from the HTTP request</param>
        /// <returns>return a new Token to the user</returns>
        [HttpGet("refresh/{refreshToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var newToken = await _authorizationService.RefreshTokenAsync(refreshToken, cancellationToken);

            if (newToken == null)
            {
                return BadRequest("The refresh token was not null");
            }

            return Ok(newToken); 
        }

        /// <summary>
        /// Function to get the claim of the user
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="token">cancellation token from the HTTP request</param>
        /// <returns>the claim of the user</returns>
        [HttpGet("getUserClaim/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserClaim(int id, CancellationToken token)
        {
            var claims = await _authorizationService.GetUserClaimsAsync(id, token);

            if (claims == null)
            {
                return BadRequest("The claims were null");
            }

            return Ok(claims);
        }




    }
}
