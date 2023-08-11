namespace IdentityService.Api.Request
{
    /// <summary>
    /// The user for the login request
    /// </summary>
    public class UserLoginRequest
    {
        // <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
