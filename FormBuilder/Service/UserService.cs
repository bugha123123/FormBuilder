using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormBuilder.Data;
using FormBuilder.DTO;
using FormBuilder.Interface;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext _context;  

    public UserService(AppDbContext context)
    {
        _context = context;
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
