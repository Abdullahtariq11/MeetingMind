using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public DateTime MeetingDate { get; set; }
        public DateTime UploadDate { get; set; }
        [Url]
        public string FileUrl { get; set; } = string.Empty;

        //Relationship
        public int ApplicationUserId { get; set; }
        [Required]
        public ApplicationUser? ApplicationUser { get; set; }
        public Transcription? Transcription { get; set; } 
        public Summary? Summary { get; set; } 
        public ICollection<ActionItem> ActionItems { get; set; }= new List<ActionItem>();
    }
}