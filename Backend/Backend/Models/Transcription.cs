using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Transcription
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; } =string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}