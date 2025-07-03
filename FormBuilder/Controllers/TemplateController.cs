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
        private readonly ITemplateStatisticsService _statisticsService;
        public TemplateController(ITemplateService templateService, IAuthService authService, ITemplateStatisticsService statisticsService)
        {
            _templateService = templateService;
            _AuthService = authService;
            _statisticsService = statisticsService;
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
        public async Task<IActionResult> All(string search, string tag, string sort)
        {
            var result = await _templateService.GetFormTemplatesAsync(search, tag, sort);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int templateId)
        {
            var template = await _templateService.GetTemplateById(templateId);
            return View(template);
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _AuthService.GetLoggedInUserAsync();

            if (user == null)
            {
                return RedirectToAction("signup", "Auth");
            }

            var template = await _templateService.GetTemplateById(id);

            if (template == null)
            {
                return NotFound();
            }

            if (template.UserId == user.Id ||
                template.AssignedUsers.Contains(user.Email) ||
                template.isPublic || User.IsInRole("Admin"))
            {
                ViewData["IsLikedByUser"] = await _templateService.IsTemplateLikedByUser(id);
                ViewData["TimesUsed"] = await _statisticsService.GetTimesUsedAsync(id);
                ViewData["CompletionRate"] = await _statisticsService.GetCompletionRateAsync(id);
                ViewData["AverageLikesAcrossTemplates"] = await _statisticsService.GetAverageLikesPerTemplateAsync();
                ViewData["LikesHistogram"] = await _statisticsService.GetLikesHistogramAsync();

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
                return RedirectToAction("Details", new { id = TemplateId });
            else
                return RedirectToAction("Details", new { id = TemplateId });
        }


        
        public async Task<IActionResult> SearchTemplates(string query)
        {
            var results = await _templateService.SearchTemplatesAsync(query);

            return Json(results.Select(r => new
            {
                id = r.Id,
                title = r.Title.ToString(),
                link = Url.Action("Details", "Template", new { id = r.Id })
            }));
        }

   [HttpPost]
public async Task<IActionResult> DeleteMultiple([FromForm] List<int?> templateIds)
{
    if (templateIds == null || !templateIds.Any())
    {
        
        return RedirectToAction("Dashboard", "User");
    }

    await _templateService.DeleteTemplatesAsync(templateIds);
    return RedirectToAction("Dashboard", "User");
}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FormTemplate template, string TagNames, IFormFile ImageFile)
        {
        

            try
            {
                var selectedTagNames = string.IsNullOrWhiteSpace(TagNames)
                    ? new List<string>()
                    : TagNames.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

                await _templateService.UpdateTemplateAsync(template, selectedTagNames, ImageFile);
                return RedirectToAction("Details", new { id = template.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
                return View("Edit", template);
            }
        }
        [HttpPost]
        public async Task<IActionResult> LikeTemplate(int id)
        { 

            var result = await _templateService.LikeTemplate(id);
            return RedirectToAction("Details", "Template", new { id = result.Id });
        }
        [HttpPost]
        public async Task<IActionResult> UnlikeTemplate(int id)
        {

            var result =  await _templateService.UnlikeTemplate(id);
            return RedirectToAction("Details", "Template", new { id = result.Id });
        }
    }
}
