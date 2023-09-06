using IdentityService.BusinessLogic.Interfaces;
using IdentityService.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.Servcices
{
    /// <summary>
    /// Implementation of the session service to manage the session of the user
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SessionService"/>
        /// </summary>
        /// <param name="signInManager">The sign in manager</param>
        public SessionService(SignInManager<User> signInManager)
        {
            this._signInManager = signInManager;
        }

        /// <summary>
        /// Function to log out the app 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
