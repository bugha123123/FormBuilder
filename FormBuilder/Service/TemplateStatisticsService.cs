using FormBuilder.Data;
using FormBuilder.Interface;
using Microsoft.EntityFrameworkCore;

public class TemplateStatisticsService : ITemplateStatisticsService
{
    private readonly AppDbContext _context;

    public TemplateStatisticsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<double> GetAverageLikesPerTemplateAsync()
    {
        var templates = await _context.FormTemplates
            .Include(t => t.Likes) 
            .ToListAsync();

        double averageLikes = templates.Any()
            ? templates.Average(t => t.Likes?.Count ?? 0)
            : 0;

        return averageLikes;
    }


    public async Task<Dictionary<string, int>> GetLikesHistogramAsync()
    {
        var likeCounts = await _context.FormTemplates
            .Select(t => t.Likes.Count)
            .ToListAsync();

        return new Dictionary<string, int>
        {
            { "0", likeCounts.Count(c => c == 0) },
            { "1-3", likeCounts.Count(c => c >= 1 && c <= 3) },
            { "4-6", likeCounts.Count(c => c >= 4 && c <= 6) },
            { "7+", likeCounts.Count(c => c >= 7) }
        };
    }

    public async Task<int> GetTimesUsedAsync(int templateId)
    {
        return await _context.Forms.CountAsync(f => f.TemplateId == templateId);
    }

    public async Task<double> GetCompletionRateAsync(int templateId)
    {
        var total = await _context.Forms.CountAsync(f => f.TemplateId == templateId);
        if (total == 0) return 0;

        var completed = await _context.Forms.CountAsync(f => f.TemplateId == templateId && f.IsCompleted);
        return ((double)completed / total) * 100;
    }
}
