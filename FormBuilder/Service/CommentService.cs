using FormBuilder.Data;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;

namespace FormBuilder.Service
{
    public class CommentService : ICommentService

    {
        private readonly AppDbContext _context;
        private readonly ITemplateService _templateService;
        private readonly IAuthService _authService;
        private readonly IFormService _formService;
        public CommentService(AppDbContext context, ITemplateService templateService, IAuthService authService, IFormService formService)
        {
            _context = context;
            _templateService = templateService;
            _authService = authService;
            _formService = formService;
        }


        public async Task AddFormComment(Comment comment, int formId, string text)
        {
            var FoundForm = await _formService.GetFormById(formId);
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

        public async Task<int> AddTemplateComment(Comment comment, int templateId, string Text)
        {
            var FoundTemplate = await _templateService.GetTemplateById(templateId);
            var LoggedInUser = await _authService.GetLoggedInUserAsync();
            if (FoundTemplate is null || LoggedInUser is null) return 0;

            comment.Text = Text;
            comment.user = LoggedInUser;
            comment.UserId = LoggedInUser.Id;
            comment.TemplateId = FoundTemplate.Id;

            comment.formTemplate = FoundTemplate;
            comment.CommentTargetType = CommentTargetType.Template;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return FoundTemplate.Id;
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

    }
}
