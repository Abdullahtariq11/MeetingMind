using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Summary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }= string.Empty;
        public DateTime Createddate { get; set; }
    }
}