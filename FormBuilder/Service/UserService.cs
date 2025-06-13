using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormBuilder.Data;
using FormBuilder.DTO;
using FormBuilder.Interface;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly ITemplateService _templateService;
    public UserService(AppDbContext context, ITemplateService templateService)
    {
        _context = context;
        _templateService = templateService;
    }

    public async Task<List<User>> GetUsers(int Id)
    {
        var template = await _templateService.GetTemplateById(Id);

        if (template is null) return null;


        return await _context.Users.Where(x => !template.AssignedUsers.Contains(x.Email)).ToListAsync();
    }

    public async Task<List<UserDTO>> SearchUsersAsync(string term)
    {
        if (string.IsNullOrWhiteSpace(term))
            return new List<UserDTO>();

        term = term.ToLower();

        return await _context.Users
            .Where(u => u.UserName.ToLower().Contains(term) || u.Email.ToLower().Contains(term))
            .Select(u => new UserDTO
            {
                Id = u.Id.ToString(),
                Username = u.UserName,
                Email = u.Email
            })
    
            .ToListAsync();
    }
}
