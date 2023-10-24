using Microsoft.AspNetCore.Identity;

namespace IdentityService.DataAccess.Models
{
    /// <summary>
    /// The role of the user inherit from IdentityRole
    /// </summary>
    public class UserRole : IdentityRole<int>
    {
        /// <summary>
        /// Initializes an instance of <see cref="UserRole"/>
        /// </summary>
        /// <param name="roleName">the name of the role</param>
        public UserRole(string name) : base(name)
        {

        }

        /// <summary>
        /// navigation property for user
        /// </summary>
        public virtual ICollection<User> users { get; set; }
    }
}
