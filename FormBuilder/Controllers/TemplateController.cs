using FormBuilder.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AutoCompleteTags(string keyWord)
        {
            var tags = await _templateService.GetAutoCompleteTags(keyWord);
            return Json(tags.Select(t => t.Name));
        }

    }
}
