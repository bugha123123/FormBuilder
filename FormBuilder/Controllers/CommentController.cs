using FormBuilder.Interface;
using FormBuilder.Models;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IActionResult> AddFormComment(Comment comment, int formId, string text)
        {
            await _commentService.AddFormComment(comment, formId, text);
            return RedirectToAction("Details", "Forms", new { formId = formId });
        }

        public async Task<IActionResult> AddTemplateComment(Comment comment, int templateId, string text)
        {
            await _commentService.AddTemplateComment(comment, templateId, text);
            return RedirectToAction("Create", "Forms", new { TemplateId = templateId });
        }

    }
}
