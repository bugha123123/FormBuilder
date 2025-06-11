using Azure.Storage.Blobs;
using FormBuilder.Data;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Markdig;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FormBuilder.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        public TemplateService(AppDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<List<FormTemplate>> GetFormTemplates()
        {
            return await _context.FormTemplates.ToListAsync();
        }

        public async Task<List<Tag>> GetAutoCompleteTags(string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return await _context.Tags.ToListAsync();
            }

            return await _context.Tags
      .Where(x => x.Name.StartsWith(keyWord))
      .ToListAsync();
        }

        public async Task<FormTemplate> CreateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile? imageFile)
        {
            var LoggedInUser = await _authService.GetLoggedInUserAsync();
            // Process markdown description
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            template.Description = Markdown.ToHtml(template.Description ?? "", pipeline);

            // If selectedTagNames is null or empty, initialize empty list
            selectedTagNames ??= new List<string>();

            // Fetch only existing tags from the DB, matching user selection
            var existingTags = await _context.Tags
                .Where(t => selectedTagNames.Contains(
                    EF.Functions.Collate(t.Name, "SQL_Latin1_General_CP1_CI_AS")
                ))
                .Select(t => t.Name)
                .ToListAsync();

            // Save the validated existing tags to SavedTags property
            template.SavedTags = existingTags;
            template.User = LoggedInUser;
            template.UserId = LoggedInUser.Id;
            // Remove duplicate assigned users if any
            if (template.AssignedUsers != null)
            {
                template.AssignedUsers = template.AssignedUsers.Distinct().ToList();
            }

            template.CreatedAt = DateTime.UtcNow;

            _context.FormTemplates.Add(template);
            await _context.SaveChangesAsync();

            return template;
        }





        public async Task<List<FormTemplate>> GetUserTemplates()
        {
            var loggedInUser = await _authService.GetLoggedInUserAsync();

            if (loggedInUser is null) return new List<FormTemplate>();

            return await _context.FormTemplates.Where(f => f.UserId == loggedInUser.Id).ToListAsync();

        }

        public async Task<List<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }
    }
}
