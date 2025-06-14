using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using FormBuilder.Models.FormBuilder.Models;
using Microsoft.EntityFrameworkCore;

public class FormService : IFormService
{
    private readonly AppDbContext _context;
    private readonly ITemplateService _templateService;
    private readonly IAuthService _authService;

    public FormService(AppDbContext context, ITemplateService templateService, IAuthService authService)
    {
        _context = context;
        _templateService = templateService;
        _authService = authService;
    }

    public async Task<Form> GetEmptyFormForTemplateAsync(int templateId)
    {
        var template = await _templateService.GetTemplateById(templateId);
        var user = await _authService.GetLoggedInUserAsync();

        if (template == null) return null;

        return new Form
        {
            TemplateId = templateId,
            Template = template,
            UserId = user.Id,
            User = user,
            Answers = template.Questions.Select(q => new FormAnswer
            {
                QuestionId = q.Id
            }).ToList()
        };
    }

    public async Task<Form> CreateForm(Form form, int templateId)
    {
        var user = await _authService.GetLoggedInUserAsync();
        var template = await _templateService.GetTemplateById(templateId);

        if (user is null || template is null)
            return null;

        if (await FormAlreadyExists(user, templateId))
        {
            return null;
        }

        form.SubmittedAt = DateTime.Now;
        form.TemplateId = template.Id;
        form.UserId = user.Id;
        form.Template = template;
        form.User = user;
        form.FilledCount += 1;

 
        if (form.Answers != null)
        {
            foreach (var answer in form.Answers)
            {
                answer.form = form; 
                answer.TemplateId = template.Id; 
            }
        }

        await _context.Forms.AddAsync(form);
        await _context.SaveChangesAsync();
        return form;
    }

    private async Task<bool> FormAlreadyExists(User user, int templateId)
    {
        return await _context.Forms.AnyAsync(x => x.UserId == user.Id && x.TemplateId == templateId);
    }

    public async Task<List<Form>> GetFormsForUser()
    {
        var user = await _authService.GetLoggedInUserAsync();

        return await _context.Forms.Where(x => x.UserId == user.Id).ToListAsync();

        
    }

    public async Task<Form> GetFormById(int formId)
    {
        return await _context.Forms.Include(x => x.Template).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == formId);
    }
}
