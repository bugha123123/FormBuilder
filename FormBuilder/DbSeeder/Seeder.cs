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
            // Seed FormTemplates
            builder.Entity<FormTemplate>().HasData(
                new FormTemplate
                {
                    Id = 1,
                    Title = TemplateTitle.JobApplication,
                    Description = "Basic job application form.",
                    UserId = null
                },
                new FormTemplate
                {
                    Id = 2,
                    Title = TemplateTitle.EventRegistration,
                    Description = "Template for event sign-ups.",
                    UserId = null
                },
                new FormTemplate
                {
                    Id = 3,
                    Title = TemplateTitle.FeedbackSurvey,
                    Description = "Collect feedback from customers.",
                    UserId = null
                }
            );

            builder.Entity<Question>().HasData(
      new Question { Id = 1, TemplateId = 1, Text = "Full Name", Type = QuestionType.ShortText, Options = null },
      new Question { Id = 2, TemplateId = 1, Text = "Email Address", Type = QuestionType.ShortText, Options = null },
      new Question { Id = 3, TemplateId = 1, Text = "Position Applying For", Type = QuestionType.OneFromList, Options = null },

      new Question { Id = 4, TemplateId = 2, Text = "Attendee Name", Type = QuestionType.ShortText, Options = null },
      new Question { Id = 5, TemplateId = 2, Text = "Email", Type = QuestionType.ShortText, Options = null },
      new Question { Id = 6, TemplateId = 2, Text = "Select Sessions", Type = QuestionType.OneFromList, Options = null },

      new Question { Id = 7, TemplateId = 3, Text = "Overall Satisfaction", Type = QuestionType.OneFromList, Options = null },
      new Question { Id = 8, TemplateId = 3, Text = "Comments", Type = QuestionType.LongText, Options = null }
  );

            builder.Entity<Tag>().HasData(
    new Tag { Id = 1, TemplateId = 1, Name = "HR",CreatedAt= DateTime.Now },
    new Tag { Id = 2, TemplateId = 1, Name = "Recruitment",CreatedAt = DateTime.Now },

    new Tag { Id = 3, TemplateId = 2, Name = "Event", CreatedAt = DateTime.Now },
    new Tag { Id = 4, TemplateId = 2, Name = "Signup",CreatedAt = DateTime.Now },

    new Tag { Id = 5, TemplateId = 3, Name = "Customer",CreatedAt = DateTime.Now },
    new Tag { Id = 6, TemplateId = 3, Name = "Survey",CreatedAt = DateTime.Now }
);


        }
    }
}
