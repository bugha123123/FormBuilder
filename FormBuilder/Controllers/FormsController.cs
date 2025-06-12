using FormBuilder.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FormBuilder.Controllers
{
    public class FormsController : Controller
    {
        private readonly ITemplateService _templateService;

        public FormsController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        public async Task<IActionResult> Create(int TemplateId)
        {
            var Result = await _templateService.GetTemplateById(TemplateId);
            return View(Result);
        }
    }
}
