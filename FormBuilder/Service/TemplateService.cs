using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Service
{
    public class TemplateService : ITemplateService
    {
        private readonly AppDbContext _context;

        public TemplateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FormTemplate>> GetFormTemplates()
        {
            return await _context.FormTemplates.Include(t => t.Tags).ToListAsync();
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

    }
}
