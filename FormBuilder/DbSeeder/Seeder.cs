using FormBuilder.Enums;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace FormBuilder.Data
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
       

       

            builder.Entity<Tag>().HasData(
    new Tag { Id = 1,  Name = "HR",CreatedAt= DateTime.Now },
    new Tag { Id = 2,  Name = "Recruitment",CreatedAt = DateTime.Now },

    new Tag { Id = 3,  Name = "Event", CreatedAt = DateTime.Now },
    new Tag { Id = 4,  Name = "Signup",CreatedAt = DateTime.Now },

    new Tag { Id = 5,  Name = "Customer",CreatedAt = DateTime.Now },
    new Tag { Id = 6,  Name = "Survey",CreatedAt = DateTime.Now }
);


        }
    }
}
