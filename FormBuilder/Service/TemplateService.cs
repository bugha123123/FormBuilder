using Azure.Storage.Blobs;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using FormBuilder.Data;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Markdig;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace FormBuilder.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TemplateService(AppDbContext context, IAuthService authService, CloudinaryService cloudinaryService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _authService = authService;
            _cloudinaryService = cloudinaryService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<FormTemplate>> GetFormTemplates()
        {
            var templates = await _context.FormTemplates
                .Include(x => x.User)
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ToListAsync();

            var filledCounts = await _context.Answers
                .GroupBy(a => a.TemplateId)
                .Select(g => new
                {
                    TemplateId = g.Key,
                    Count = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            // Map counts back to the templates
            foreach (var template in templates)
            {
                var countEntry = filledCounts.FirstOrDefault(c => c.TemplateId == template.Id);
                template.FilledFormsCount = countEntry?.Count ?? 0;
            }

            return templates;
        }
        public async Task<List<FormTemplate>> GetPopularTemplates(int count)
        {
            var filledCounts = await _context.Answers
                .GroupBy(a => a.TemplateId)
                .Select(g => new
                {
                    TemplateId = g.Key,
                    Count = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            var templates = await _context.FormTemplates
                .Include(t => t.User)
                .Include(t => t.Tags)
                .Include(t => t.Comments)
                .ToListAsync();

            foreach (var template in templates)
            {
                var match = filledCounts.FirstOrDefault(f => f.TemplateId == template.Id);
                template.FilledFormsCount = match?.Count ?? 0;
            }

            return templates
                .OrderByDescending(t => t.FilledFormsCount)
                .Take(count)
                .ToList();
        }
        public async Task<List<FormTemplate>> GetLatestTemplates()
        {
            var templates = await _context.FormTemplates
                .Include(t => t.User)
                .Include(t => t.Tags)
                .Include(t => t.Comments)
                .OrderByDescending(t => t.CreatedAt)
                .Take(10) 
                .ToListAsync();

            // Populate FilledFormsCount
            var filledCounts = await _context.Answers
                .GroupBy(a => a.TemplateId)
                .Select(g => new
                {
                    TemplateId = g.Key,
                    Count = g.Select(a => a.UserId).Distinct().Count()
                })
                .ToListAsync();

            foreach (var template in templates)
            {
                var count = filledCounts.FirstOrDefault(f => f.TemplateId == template.Id);
                template.FilledFormsCount = count?.Count ?? 0;
            }

            return templates;
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
            var loggedInUser = await _authService.GetLoggedInUserAsync();

            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            template.Description = Markdown.ToHtml(template.Description ?? "", pipeline);

            selectedTagNames ??= new List<string>();

            var distinctTagNames = selectedTagNames
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Select(t => t.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var existingTags = await _context.Tags
                .Where(t => distinctTagNames.Contains(t.Name))
                .ToListAsync();

            var existingTagNames = existingTags.Select(t => t.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

            var newTags = distinctTagNames
                .Where(name => !existingTagNames.Contains(name))
                .Select(name => new Tag { Name = name })
                .ToList();

            if (newTags.Any())
            {
                _context.Tags.AddRange(newTags);
                await _context.SaveChangesAsync();
            }

            // Combine tags
            var allTags = existingTags.Concat(newTags).ToList();

            if (template.Tags == null)
                template.Tags = new List<Tag>();

            template.Tags.Clear();
            foreach (var tag in allTags)
                template.Tags.Add(tag);

            template.SavedTags = allTags.Select(t => t.Name).ToList();

            template.User = loggedInUser;
            template.UserId = loggedInUser.Id;

            if (template.AssignedUsers != null)
                template.AssignedUsers = template.AssignedUsers.Distinct().ToList();

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




        public async Task UpdateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile imageFile)
        {
            var existingTemplate = await _context.FormTemplates
                .Include(t => t.Questions)
                .FirstOrDefaultAsync(t => t.Id == template.Id);

            if (existingTemplate == null)
                throw new Exception("Template not found.");

            // Update basic fields
            existingTemplate.Title = template.Title;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            existingTemplate.Description = Markdown.ToHtml(template.Description ?? "", pipeline);
            existingTemplate.isPublic = template.isPublic;

            // Upload image if provided
            if (imageFile != null && imageFile.Length > 0)
            {
                var imageUrl = await _cloudinaryService.UploadImageAsync(imageFile);
                existingTemplate.ImageUrl = imageUrl;
            }

            // Update SavedTags (List<string>)
            selectedTagNames ??= new List<string>();
            existingTemplate.SavedTags = selectedTagNames
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Remove existing Questions and replace with new ones
            if (existingTemplate.Questions != null && existingTemplate.Questions.Any())
            {
                _context.Questions.RemoveRange(existingTemplate.Questions);
            }

            existingTemplate.Questions = new List<Question>();

            if (template.Questions != null)
            {
                foreach (var question in template.Questions)
                {
                    var newQuestion = new Question
                    {
                        Text = question.Text,
                        Type = question.Type,
                        Options = question.Options != null ? new List<string>(question.Options) : new List<string>()
                    };

                    existingTemplate.Questions.Add(newQuestion);
                }
            }

            // Update AssignedUsers (List<string> of emails)
            template.AssignedUsers ??= new List<string>();

            existingTemplate.AssignedUsers = template.AssignedUsers
                .Where(email => !string.IsNullOrWhiteSpace(email))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            // Update owner
            var loggedInUser = await _authService.GetLoggedInUserAsync();
            existingTemplate.User = loggedInUser;
            existingTemplate.UserId = loggedInUser.Id;

            // No need to call _context.FormTemplates.Update(...) since EF tracks the entity already

            await _context.SaveChangesAsync();
        }






        public async Task<List<FormTemplate>> GetUserTemplates()
        {
            var loggedInUser = await _authService.GetLoggedInUserAsync();

            if (loggedInUser is null) return new List<FormTemplate>();

            return await _context.FormTemplates.Where(f => f.UserId == loggedInUser.Id && f.UserId != null).ToListAsync();

        }

        public async Task<List<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }
        public async Task<FormTemplate> GetTemplateById(int id)
        {
            return await _context.FormTemplates
                .Include(x => x.User)
                .Include(x => x.Questions)
                .Include(x => x.Comments)
                .Include(x => x.Answers)
                .FirstOrDefaultAsync(t => t.Id == id);
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

        public async Task<List<FormTemplate>> SearchTemplatesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<FormTemplate>();

            query = query.ToLower();

            var templates = await _context.FormTemplates
                .Include(t => t.Questions)
                .Include(t => t.Comments)
                .Where(t => t.isPublic)
                .ToListAsync();

           
            var filtered = templates.Where(t =>
                (t.Title != null && t.Title.ToString().ToLower().Contains(query)) ||
                (t.Description != null && t.Description.ToLower().Contains(query)) ||
                (t.Questions != null && t.Questions.Any(q => q.Text != null && q.Text.ToLower().Contains(query))) ||
                (t.Comments != null && t.Comments.Any(c => c.Text != null && c.Text.ToLower().Contains(query))) ||
                (t.Tags != null && t.SavedTags.Any(tag => tag != null && tag.ToLower().Contains(query)))
            ).ToList();

            return filtered;
        }

        public async Task DeleteTemplatesAsync(List<int?> templateIds)
        {
            var nonNullTemplateIds = templateIds.Where(id => id.HasValue).Select(id => id.Value).ToList();

            // Delete Tags linked to templates first
            var tagsToDelete = await _context.Tags
                .Where(t => t.TemplateId != null && nonNullTemplateIds.Contains(t.TemplateId))
                .ToListAsync();
            _context.Tags.RemoveRange(tagsToDelete);

            // Delete Comments linked to templates
            var templateComments = await _context.Comments
                .Where(c => c.TemplateId != null && nonNullTemplateIds.Contains(c.TemplateId.Value))
                .ToListAsync();
            _context.Comments.RemoveRange(templateComments);

            // Get forms linked to these templates
            var forms = await _context.Forms
                .Where(f => f.TemplateId != null && nonNullTemplateIds.Contains(f.TemplateId.Value))
                .ToListAsync();
            var formIds = forms.Select(f => f.Id).ToList();

            // Delete Comments linked to those forms
            var formComments = await _context.Comments
                .Where(c => c.FormId != null && formIds.Contains(c.FormId.Value))
                .ToListAsync();
            _context.Comments.RemoveRange(formComments);

            // Delete Answers linked to those forms
            var answers = await _context.Answers
                .Where(a => a.FormId != null && formIds.Contains(a.FormId))
                .ToListAsync();
            _context.Answers.RemoveRange(answers);

            // Delete forms
            _context.Forms.RemoveRange(forms);

            // Delete templates
            var templates = await _context.FormTemplates
                .Where(t => nonNullTemplateIds.Contains(t.Id))
                .ToListAsync();
            _context.FormTemplates.RemoveRange(templates);

            await _context.SaveChangesAsync();
        }





    }
}
