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
        private readonly CloudinaryService _cloudinaryService;
        public TemplateService(AppDbContext context, IAuthService authService, CloudinaryService cloudinaryService)
        {
            _context = context;
            _authService = authService;
            _cloudinaryService = cloudinaryService;
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

            selectedTagNames ??= new List<string>();

            List<string> existingTags = await _context.Tags
                .Where(t => selectedTagNames.Contains(
                    EF.Functions.Collate(t.Name, "SQL_Latin1_General_CP1_CI_AS")
                ))
                .Select(t => t.Name)
                .ToListAsync();

            template.SavedTags = existingTags;
            template.User = LoggedInUser;
            template.UserId = LoggedInUser.Id;
            if (template.AssignedUsers != null)
            {
                template.AssignedUsers = template.AssignedUsers.Distinct().ToList();
            }


            if (imageFile != null && imageFile.Length > 0)
            {
                string imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                template.ImageUrl = imageUrl;  
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

        public async Task<FormTemplate> GetTemplateById(int id)
        {
            return await _context.FormTemplates.Include(x => x.User).Include(x => x.Questions).Include(x => x.Comments).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<FormTemplate>> SearchTemplates(string tag)
        {
            var allTemplates = await _context.FormTemplates.ToListAsync();

            var filtered = allTemplates
                .Where(t => t.SavedTags != null &&
                            t.SavedTags.Any(savedTag =>
                                savedTag.Contains(tag, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return filtered;
        }

        public async Task<bool> AssignUserToTemplateAsync(int templateId, string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) return false;

            var template = await _context.FormTemplates
                .FirstOrDefaultAsync(t => t.Id == templateId);

            if (template == null) return false;

            if (template.AssignedUsers == null)
                template.AssignedUsers = new List<string>();

            if (!template.AssignedUsers.Contains(userEmail))
            {
                template.AssignedUsers.Add(userEmail);
                _context.Update(template);
                await _context.SaveChangesAsync();
            }

            return true;
        }

    }
}
