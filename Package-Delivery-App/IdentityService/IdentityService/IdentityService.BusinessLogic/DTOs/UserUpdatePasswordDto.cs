﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.BusinessLogic.DTOs
{
    public class UserUpdatePasswordDto
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
