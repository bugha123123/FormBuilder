using FormBuilder.DTO;
using FormBuilder.Interface;
using FormBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FormBuilder.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public async Task<IActionResult> Create()
        {
            var topics = await _templateService.GetTags();
            ViewBag.Topics = new SelectList(topics, "Id", "Name");
            return View(new FormTemplateCreateViewModel());
        }


        public async Task<IActionResult> AutoCompleteTags(string keyword)
        {
            var tags = await _templateService.GetAutoCompleteTags(keyword);
            return Json(tags.Select(t => t.Name));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTemplate(FormTemplate template, string TagNames, IFormFile? imageFile)
        {
            var selectedTagNames = string.IsNullOrWhiteSpace(TagNames)
                ? new List<string>()
                : TagNames.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList();

            var createdTemplate = await _templateService.CreateTemplateAsync(template, selectedTagNames, imageFile);

            return RedirectToAction("Details", new { id = createdTemplate.Id });
        }


    }
}
