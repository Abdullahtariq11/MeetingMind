using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.User
{
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}