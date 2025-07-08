using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AdminService : IAdminService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;
    public AdminService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task AddAdminsAsync(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }

    public async Task RemoveAdminsAsync(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && await _userManager.IsInRoleAsync(user, "Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
        }
    }

    public async Task BlockUsersAsync(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = System.DateTimeOffset.MaxValue;
                await _userManager.UpdateAsync(user);
            }
        }
    }

    public async Task UnblockUsersAsync(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.LockoutEnd = null;
                await _userManager.UpdateAsync(user);
            }
        }
    }
    public async Task DeleteUsersAsync(List<string> userIds)
    {
        foreach (var userId in userIds)
        {
            var userTemplates = await _context.FormTemplates
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var templateIds = userTemplates.Select(t => t.Id).ToList();

            var templateComments = await _context.Comments
                .Where(c => c.TemplateId != null && templateIds.Contains(c.TemplateId.Value))
                .ToListAsync();
            _context.Comments.RemoveRange(templateComments);

            var templateLikes = await _context.Likes
                .Where(l => l.TemplateId != null && templateIds.Contains(l.TemplateId))
                .ToListAsync();
            _context.Likes.RemoveRange(templateLikes);

            var forms = await _context.Forms
                .Where(f => f.TemplateId != null && templateIds.Contains(f.TemplateId.Value))
                .ToListAsync();
            var formIds = forms.Select(f => f.Id).ToList();

            var formComments = await _context.Comments
                .Where(c => c.FormId != null && formIds.Contains(c.FormId.Value))
                .ToListAsync();
            _context.Comments.RemoveRange(formComments);

            var answers = await _context.Answers
                .Where(a => a.FormId != null && formIds.Contains(a.FormId))
                .ToListAsync();

        
            _context.Answers.RemoveRange(answers);

            _context.Forms.RemoveRange(forms);

            _context.FormTemplates.RemoveRange(userTemplates);

            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task<int> GetTemplateCountForUserAsync(string userId)
    {
        return await _context.FormTemplates
            .CountAsync(t => t.UserId == userId);
    }

    public async Task<int> GetCommentCountForUserAsync(string userId)
    {
        return await _context.Comments
            .CountAsync(c => c.UserId == userId);
    }

    public async Task<int> GetLikeCountForUserAsync(string userId)
    {
        return await _context.Likes
            .CountAsync(l => l.UserId == userId);
    }
}
