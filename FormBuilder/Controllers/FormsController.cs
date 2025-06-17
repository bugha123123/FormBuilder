using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class FormsController : Controller
{
    private readonly ITemplateService _templateService;
    private readonly IFormService _formService;
    private readonly IAuthService _authService;

    public FormsController(ITemplateService templateService, IFormService formService, IAuthService authService)
    {
        _templateService = templateService;
        _formService = formService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int templateId)
    {
 

        var form = await _formService.GetEmptyFormForTemplateAsync(templateId);
        if (form == null) return NotFound();

        return View(form);
    }
    [HttpGet]
    public async Task<IActionResult> AccessDenied()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Details(int formId)
    {

        var result = await _formService.GetFormById(formId);
        var user = await _authService.GetLoggedInUserAsync();
        if (result.UserId == user.Id || result.Template.AssignedUsers.Contains(user.Email) || !result.Template.isPublic)
        {
            return View(result);
        }


        return RedirectToAction("AccessDenied", "Forms");
    }
    [HttpPost]
    public async Task<IActionResult> Create(Form form, int templateId)
    {
        var result = await _formService.CreateForm(form, templateId);

        if (result == null)
        {
            TempData["Error"] = "You have already submitted this form.";
            return RedirectToAction("Create", "Forms", new { TemplateId  = templateId }); 
        }

        return RedirectToAction("Details", "Forms", new { formId = result.Id });
    }

    public async Task<IActionResult> AddFormComment(Comment comment, int formId, string text)
    {
        await _formService.AddFormComment(comment, formId, text);
        return RedirectToAction("Details", "Forms", new { formId  = formId});
    }

    public async Task<IActionResult> AddTemplateComment(Comment comment, int templateId, string text)
    {
        await _formService.AddTemplateComment(comment, templateId, text);
        return RedirectToAction("Create", "Forms", new { TemplateId = templateId });
    }

}
