using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Backend.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }=string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogin { get; set; }
        //Relationship
        public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    }
}