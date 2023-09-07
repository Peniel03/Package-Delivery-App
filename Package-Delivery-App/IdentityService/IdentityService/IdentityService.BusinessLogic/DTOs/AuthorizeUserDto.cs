﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.DTOs
{    
    /// <summary>
    /// the authorize user data transfert object
    /// </summary>
    public class AuthorizeUserDto 
    {

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}