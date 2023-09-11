namespace IdentityService.Api.Request
{
    public class UserGetRequest
    {

        /// <summary>
        /// The first Name of the user 
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The last name of the user
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// The username of the user
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The Phone Number of the user
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;
    }
}
