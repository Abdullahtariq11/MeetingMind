using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.User;
using Backend.Models;

namespace Backend.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<Service<UserDto>> Signup(Signup signup);
        public Task<Service<UserLoginDto>> Login(Login login);
        public string GenerateToken(ApplicationUser user);
    }


}