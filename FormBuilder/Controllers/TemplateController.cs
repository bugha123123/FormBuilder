using FormBuilder.DTO;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using FormBuilder.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FormBuilder.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;
        private readonly IAuthService _AuthService;
        public TemplateController(ITemplateService templateService, IAuthService authService)
        {
            _templateService = templateService;
            _AuthService = authService;
        }

        public async Task<IActionResult> Create()
        {
            var topics = await _templateService.GetTags();
            ViewBag.Topics = new SelectList(topics, "Id", "Name");
            return View(new FormTemplateCreateViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var result = await _templateService.GetFormTemplates();
            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _AuthService.GetLoggedInUserAsync();

            // If user is not logged in, redirect to signup
            if (user == null)
            {
                return RedirectToAction("signup", "Auth");
            }

            var template = await _templateService.GetTemplateById(id);

            if (template.UserId == user.Id ||
                template.AssignedUsers.Contains(user.Email) ||
                template.isPublic)
            {
                return View(template);
            }

            return RedirectToAction("AccessDenied", "Template");
        }

        public async Task<IActionResult> Search(string Tag)
        {

            var Template = await _templateService.SearchTemplates(Tag);
            return View(Template);
        }

        public async Task<IActionResult> AutoCompleteTags(string keyword)
        {
            var tags = await _templateService.GetAutoCompleteTags(keyword);
            return Json(tags.Select(t => t.Name));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTemplate(FormTemplate template, string TagNames, IFormFile? ImageFile)
        {
            var selectedTagNames = string.IsNullOrWhiteSpace(TagNames)
                ? new List<string>()
                : TagNames.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

            var createdTemplate = await _templateService.CreateTemplateAsync(template, selectedTagNames, ImageFile);

            return RedirectToAction("Details", new { id = createdTemplate.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AssignUser(int TemplateId, string email)
        {
            var success = await _templateService.AssignUserToTemplateAsync(TemplateId, email);

            if (success)
                return RedirectToAction("Details", new { TemplateId });
            else
                return RedirectToAction("Details", new { TemplateId });
        }



    }
}
