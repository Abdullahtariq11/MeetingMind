using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs.User
{
    public class Signup
    {
        [Required]
        public string Name { get; set; }=string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; }=string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password does not match confirmation password.")]
        public string ConfirmPassword { get; set; }=string.Empty;
    }
}