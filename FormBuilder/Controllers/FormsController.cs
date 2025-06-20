using FormBuilder.DTO;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Numerics;

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
    [Authorize]
    public async Task<IActionResult> Edit(int FormId, int templateId )
    {
        var result = await _formService.GetFormById(FormId);       
        return View(result);
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


    [HttpPost]
    public async Task<IActionResult> Edit(Form model, int templateId, int FormId)
    {

        var result = await _formService.Edit(model, templateId, FormId);
        return RedirectToAction("Details", "Forms", new { formId = result.Id });


    }

    [HttpPost]
    public async Task<IActionResult> Delete(int formId)
    {
        await _formService.DeleteFormAsync(formId);
        return RedirectToAction("User", "Dashboard");
    }

}
