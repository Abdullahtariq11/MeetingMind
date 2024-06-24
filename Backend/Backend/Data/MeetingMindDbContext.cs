using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class MeetingMindDbContext:IdentityDbContext<ApplicationUser>
    {
        public MeetingMindDbContext(DbContextOptions<MeetingMindDbContext>options):base(options)
        {
            
        }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<Transcription> Transcriptions { get; set; }
        public DbSet<ActionItem> ActionItems { get; set; }

        //Relationship
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Meeting>()
            .HasOne(m=>m.ApplicationUser)
            .WithMany(u=>u.Meetings)
            .HasForeignKey(m=>m.ApplicationUserId);

            builder.Entity<Meeting>()
            .HasOne(m=>m.Summary)
            .WithOne(s=>s.Meeting)
            .HasForeignKey<Summary>(m => m.MeetingId);

            builder.Entity<Meeting>()
            .HasOne(m=>m.Transcription)
            .WithOne(t=>t.Meeting)
            .HasForeignKey<Transcription>(m=>m.MeetingId);

            builder.Entity<Meeting>()
            .HasMany(m=>m.ActionItems)
            .WithOne(a=>a.Meeting)
            .HasForeignKey(m=>m.MeetingId);
            
            base.OnModelCreating(builder);
        }

    }
}