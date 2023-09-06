using IdentityService.BusinessLogic.DTOs;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using IdentityService.DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.BusinessLogic.Servcices
{
    /// <summary>
    /// this class Implements the authorization services to manage the authorization of the user
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly ILoggerManager _logger;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthorizationService" /> class.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="userRefreshTokenRepository"></param>
        /// <param name="saveChangesRepository"></param>
        public AuthorizationService(UserManager<User> userManager,
            ILoggerManager logger,
            IConfiguration configuration,
            IUserRefreshTokenRepository userRefreshTokenRepository,
            IUserClaimRepository userClaimRepository ,
            ISaveChangesRepository saveChangesRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userClaimRepository = userClaimRepository;
             _saveChangesRepository = saveChangesRepository;

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="email">The email of the user that we want to authorize</param>
        /// <param name="password">The password of the user</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a Token</returns>
        public async Task<TokenDto> AuthorizeAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await ValidateUserAsync(email, password);
            var refreshToken = GenerateRefreshToken();
            var token = await GenerateTokenAsync(user);

            _userRefreshTokenRepository.AddUserRefreshToken(new UserRefreshToken
            {
                CreationDate = DateTimeOffset.Now,
                LifeRefreshTokenInMinutes = Convert.ToInt32(_configuration.GetSection("JWT")["LifeTimeRefresh"]),
                RefreshToken = refreshToken,
                UserId = user.Id
            });

            await _saveChangesRepository.SaveChangesAsync(cancellationToken);
            _logger.LogInfo("Saved the changes to the database");

            return new TokenDto
            {
                RefreshToken = refreshToken,
                TokenLifeTimeInMinutes = Convert.ToInt32(_configuration.GetSection("JWT")["LifeTimeRefresh"]),
                AccessToken = token,
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="cancellationToken">he cancellation token</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <returns>A List of <see cref="UserClaim"/></returns>
        public async Task<List<UserClaim>> GetUserClaimsAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _userClaimRepository.GetUserClaimsAsync(id, cancellationToken);

            if (result == null)
            {
                _logger.LogError("An error occured the claim were not found");

                throw new NotFoundException("The claims were not found");
            }

            return result;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="token">The refresh token that has expired</param>
        /// <param name="cancellationToken">he cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/> that contains the token</returns>
        public async Task<TokenDto> RefreshTokenAsync(string token, CancellationToken cancellationToken)
        {
            var checkedToken = await _userRefreshTokenRepository
                            .GetSavedUserRefreshTokensAsync(token, cancellationToken);

            if (checkedToken == null || checkedToken.IsActive == false)
            {
                _logger.LogError("Error occured while processing the request : User Refresh Token Not Found");

                throw new NotFoundException("User Refresh Token Not Found");
            }

            if (checkedToken.User == null)
            {
                _logger.LogError("Error occured User Not Found");

                throw new NotFoundException("Error occured User Not Found");
            }

            var refreshTokenToSave = GenerateRefreshToken();
            checkedToken.RefreshToken = refreshTokenToSave;
            checkedToken.CreationDate = DateTimeOffset.Now;

            _userRefreshTokenRepository.UpdateUserRefreshToken(checkedToken);
            await _saveChangesRepository.SaveChangesAsync(cancellationToken);

            return new TokenDto
            {
                AccessToken = await GenerateTokenAsync(checkedToken.User),
                RefreshToken = refreshTokenToSave,
                TokenLifeTimeInMinutes = Convert.ToInt32(_configuration["JWT:LifeTimeRefresh"]),
            };
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <param name="password">The password of the user</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/> that contains the user</returns>
        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var checkedUser = await _userManager.FindByEmailAsync(email);
            var isValid = await _userManager.CheckPasswordAsync(checkedUser, password);

            if (isValid == false || checkedUser == null)
            {
                _logger.LogError("Error occured while processing the validation of credentials ");

                throw new NotFoundException("The user was not found check your password or your email");
            }

            return checkedUser;
        }


        /// <summary>
        /// Function To generate the token from the claims of the user
        /// </summary>
        /// <param name="user">The user</param>
        /// <exception cref="NotFoundException">When the user is not found</exception>
        /// <returns>A token as a <see cref="string"/> </returns>
        private async Task<string> GenerateTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT")["Key"]);
            var userRole = await _userManager.GetRolesAsync(user);
            var role = userRole.First();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , user.LastName),
                    new Claim(ClaimTypes.Email , user.Email),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(ClaimTypes.GivenName , user.FirstName),
                }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("JWT")["LifeTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
                Audience = _configuration.GetSection("JWT")["Audience"],
                Issuer = _configuration.GetSection("JWT")["Issuer"]
            };

            try
            {
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while creating the token");

                throw new NotFoundException("The token can not created " + ex.Message);
            }
        }


        /// <summary>
        /// Function to generate a refresh token
        /// </summary>
        /// <returns>Refresh Token as  a <see cref="string"/></returns>
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
