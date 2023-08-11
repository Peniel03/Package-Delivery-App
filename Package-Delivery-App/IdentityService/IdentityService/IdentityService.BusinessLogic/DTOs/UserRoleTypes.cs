using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.DTOs
{
    /// <summary>
    /// the different type of user role
    /// </summary>
    public class UserRoleTypes
    {

        public static List<string> RolesTypes = new List<string>
        {
            "User",
            "Admin",
            "Worker"
        };
    }
}
