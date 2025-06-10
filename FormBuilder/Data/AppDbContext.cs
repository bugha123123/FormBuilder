using FormBuilder.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace FormBuilder.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<FormTemplate> FormTemplates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FormTemplate>()
              .Property(e => e.Title)
              .HasConversion<string>();
            builder.Seed(); 
        }

   


    }
}
