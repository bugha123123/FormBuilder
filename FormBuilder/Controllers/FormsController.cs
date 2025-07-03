using FormBuilder.Data;
using FormBuilder.DTO;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Linq;
using System.Numerics;

public class FormsController : Controller
{
    private readonly ITemplateService _templateService;
    private readonly IFormService _formService;
    private readonly IAuthService _authService;
    private readonly AppDbContext _context;
    public FormsController(ITemplateService templateService, IFormService formService, IAuthService authService, AppDbContext context)
    {
        _templateService = templateService;
        _formService = formService;
        _authService = authService;
        _context = context;
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

        if (result.UserId == user.Id || result.Template.AssignedUsers.Contains(user.Email) || !result.Template.isPublic || User.IsInRole("Admin"))
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
            return RedirectToAction("Create", "Forms", new { templateId  = templateId }); 
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
    public async Task<IActionResult> DeleteMultipleForms([FromForm] List<int> formIds)
    {
        if (formIds == null || !formIds.Any())
        {
            return RedirectToAction("Dashboard", "User");
        }

        foreach (var id in formIds)
        {
            await _formService.DeleteFormAsync(id);
        }
        return RedirectToAction("Dashboard", "User");
    }

}
