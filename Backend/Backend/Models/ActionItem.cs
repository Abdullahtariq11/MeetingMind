using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class ActionItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }= string.Empty;
        public string? AssignTo { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}