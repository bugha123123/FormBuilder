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

        public DbSet<FormAnswer> Answers { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Form> Forms { get; set; }

        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FormTemplate>()
              .Property(e => e.Title)
              .HasConversion<string>();
            builder.Seed();

            builder.Entity<FormAnswer>()
       .HasOne(fa => fa.form)
       .WithMany(f => f.Answers)
       .HasForeignKey("FormId")
       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FormAnswer>()
                .HasOne(fa => fa.formTemplate)
                .WithMany() 
                .HasForeignKey("TemplateId")
                .OnDelete(DeleteBehavior.Restrict);





        }




    }
}
