using IdentityService.BusinessLogic.DTOs;
using IdentityService.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.SeedData
{
    /// <summary>
    /// Add seed data for the UserRole Class
    /// </summary>
    public class SeedUserRolesData
    {
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of <see cref="SeedUserRolesData"/>
        /// </summary>
        /// <param name="roleManager"></param>
        public SeedUserRolesData(RoleManager<UserRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// Function add seed data 
        /// </summary>
        public void SeedData()
        {
            try
            {
                if (_roleManager.Roles == null)
                {
                    foreach (var role in UserRoleTypes.RolesTypes)
                    {
                        _roleManager.CreateAsync(new UserRole(role));
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                throw ex;
            }
        }

    }
}
