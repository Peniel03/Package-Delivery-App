using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The session service to manage sessions
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Function Logout the app
        /// </summary>
        /// <returns>A <see cref="Task"/></returns>
        Task LogoutAsync();

    }
}
