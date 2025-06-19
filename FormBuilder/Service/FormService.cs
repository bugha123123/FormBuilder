using FormBuilder.Data;
using FormBuilder.DTO;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using static System.Net.Mime.MediaTypeNames;

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
            return null;

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
                var question = template.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                answer.QuestionType = question != null ? question.Type : QuestionType.ShortText;
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
        return await _context.Forms.Include(x => x.Template).ThenInclude(x => x.Questions).Include(x => x.User).Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == formId);
    }

    public async Task AddFormComment(Comment comment, int formId, string text)
    {
        var FoundForm = await GetFormById(formId);
        var LoggedInUser = await _authService.GetLoggedInUserAsync();
        if (FoundForm is null || LoggedInUser is null) return;

        comment.Text = text;
        comment.user = LoggedInUser;
        comment.UserId = LoggedInUser.Id;
        comment.TemplateId = null;
        comment.formTemplate = null;
        comment.CommentTargetType = CommentTargetType.Form;
        comment.FormId = FoundForm.Id;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();

    }

    public async Task AddTemplateComment(Comment comment, int templateId, string Text)
    {
        var FoundTemplate = await _templateService.GetTemplateById(templateId);
        var LoggedInUser = await _authService.GetLoggedInUserAsync();
        if (FoundTemplate is null || LoggedInUser is null) return;

        comment.Text = Text;
        comment.user = LoggedInUser;
        comment.UserId = LoggedInUser.Id;
        comment.TemplateId = FoundTemplate.Id;
        
        comment.formTemplate = FoundTemplate;
        comment.CommentTargetType = CommentTargetType.Template;

        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetComments(
        int? formId = null,
        int? templateId = null,
        CommentTargetType? commentTargetType = null)
    {
        var query = _context.Comments
            .Include(x => x.user)
            .Include(x => x.formTemplate)
                .ThenInclude(x => x.Questions)
            .Include(x => x.form)
            .AsQueryable();

        if (commentTargetType.HasValue)
        {
            query = query.Where(x => x.CommentTargetType == commentTargetType.Value);
        }

        if (formId.HasValue)
        {
            query = query.Where(x => x.FormId == formId.Value);
        }

        if (templateId.HasValue)
        {
            query = query.Where(x => x.TemplateId == templateId.Value);
        }

        var comments = await query.ToListAsync();

        return comments ?? new List<Comment>();
    }

    public async Task<Form> Edit(Form updatedForm, int TemplateId, int FormId)
    {
        var existingForm = await GetFormById(FormId);

        var Template = await _templateService.GetTemplateById(TemplateId);

        if (existingForm == null)
            throw new Exception("Form not found");

        _context.Answers.RemoveRange(existingForm.Answers);

        foreach (var updatedAnswer in updatedForm.Answers)  
        {
            var newAnswer = new FormAnswer
            {
                form = existingForm,
                QuestionId = updatedAnswer.QuestionId,
                TemplateId = Template.Id,  
                formTemplate = Template, 
                Response = updatedAnswer.Response,
                QuestionType = updatedAnswer.QuestionType
            };

           await  _context.Answers.AddAsync(newAnswer);
        }

        await _context.SaveChangesAsync();
        return existingForm;
    }




}


