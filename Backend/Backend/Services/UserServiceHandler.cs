using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.DTOs.User;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services
{
    public class UserServiceHandler : IUserServices
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserServiceHandler(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IConfiguration configuration)
        {
            _usermanager=userManager;
            _signInManager=signInManager;
            _configuration=configuration;
        }

        public async Task<Service<UserDto>> Signup(Signup signup)
        {
            var existingUser= await _usermanager.FindByEmailAsync(signup.Email);
            if(signup==null || existingUser!=null)
            {
                string message=signup==null ? "Invalid data":"email is already registered";
                return Service<UserDto>.failure(message);
            }
            
            var user= new ApplicationUser
            {
                Name=signup.Name,
                Email=signup.Email,
                CreatedDate=DateTime.Now
            };
            var result= await _usermanager.CreateAsync(user,signup.Password);
            if(!result.Succeeded)
            {
                return Service<UserDto>.failure(string.Join(", ", result.Errors.Select((e)=>e.Description)));
            }
            var userDto= new UserDto
            {
                Name=user.Name,
                Email=user.Email
            };
            return Service<UserDto>.success(userDto);
        }
    }
}