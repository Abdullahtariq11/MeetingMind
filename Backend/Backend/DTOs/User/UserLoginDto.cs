using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.User
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}