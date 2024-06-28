using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Backend.DTOs.User;
using Backend.Models;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services
{
    public class UserServiceHandler : IUserServices
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public UserServiceHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        //Generate Token
        public string GenerateToken(ApplicationUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //User Login Method
        public async Task<Service<UserLoginDto>> Login(Login login)
        {
            if (login == null)
            {
                return Service<UserLoginDto>.failure("Invalid Email or Password");
            }
            var user = await _usermanager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return Service<UserLoginDto>.failure("Invalid Email or Password");
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (!result.Succeeded)
            {
                return Service<UserLoginDto>.failure("Invalid Email or Password");
            }
            var userLogin = new UserLoginDto
            {
                Email = user.Email,
                Token = GenerateToken(user)

            };
            return Service<UserLoginDto>.success(userLogin);

        }

        //User Signup Method
        public async Task<Service<UserDto>> Signup(Signup signup)
        {
            var existingUser = await _usermanager.FindByEmailAsync(signup.Email);
            if (signup == null || existingUser != null)
            {
                string message = signup == null ? "Invalid data" : "email is already registered";
                return Service<UserDto>.failure(message);
            }

            var user = new ApplicationUser
            {
                Name = signup.Name,
                Email = signup.Email,
                CreatedDate = DateTime.Now
            };
            var result = await _usermanager.CreateAsync(user, signup.Password);
            if (!result.Succeeded)
            {
                return Service<UserDto>.failure(string.Join(", ", result.Errors.Select((e) => e.Description)));
            }
            var userDto = new UserDto
            {
                Name = user.Name,
                Email = user.Email
            };
            return Service<UserDto>.success(userDto);
        }



    }
}