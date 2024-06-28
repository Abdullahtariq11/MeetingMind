using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.User;

namespace Backend.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<Service<UserDto>> Signup(Signup signup);
    }


}