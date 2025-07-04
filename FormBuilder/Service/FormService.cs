﻿using FormBuilder.Data;
using FormBuilder.DTO;
using FormBuilder.Enums;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

public class FormService : IFormService
{
    private readonly AppDbContext _context;
    private readonly ITemplateService _templateService;
    private readonly IAuthService _authService;

    public FormService(AppDbContext context, ITemplateService templateService, IAuthService authService)
    {
        _context = context;
        _templateService = templateService;
        _authService = authService;
    }

    public async Task<Form> GetEmptyFormForTemplateAsync(int templateId)
    {
        var template = await _templateService.GetTemplateById(templateId);
        var user = await _authService.GetLoggedInUserAsync();

        if (template == null) return null;

        return new Form
        {
            TemplateId = templateId,
            Template = template,
            UserId = user.Id,
            User = user,
            Answers = template.Questions.Select(q => new FormAnswer
            {
                QuestionId = q.Id
            }).ToList()
        };
    }

    public async Task<Form> CreateForm(Form form, int templateId)
    {
        var user = await _authService.GetLoggedInUserAsync();
        var template = await _templateService.GetTemplateById(templateId);

        if (user is null || template is null)
            return null;

        if (await FormAlreadyExists(user, templateId))
            return null;

        form.SubmittedAt = DateTime.Now;
        form.TemplateId = template.Id;
        form.UserId = user.Id;
        form.Template = template;
        form.User = user;
        form.FilledCount += 1;
        template.FilledFormsCount = form.FilledCount;
        form.IsCompleted = true;
        form.FilledByUserId = user.Id;
        if (form.Answers != null)
        {
            foreach (var answer in form.Answers)
            {
                var question = template.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                answer.QuestionType = question != null ? question.Type : QuestionType.ShortText;
                answer.Form = form;
                answer.TemplateId = template.Id;
                answer.UserId = user.Id;
                answer.User = user;
            }
        }



        await _context.Forms.AddAsync(form);
        await _context.SaveChangesAsync();
        return form;
    }



    private async Task<bool> FormAlreadyExists(User user, int templateId)
    {
        return await _context.Forms.AnyAsync(x => x.UserId == user.Id && x.TemplateId == templateId);
    }

    public async Task<List<Form>> GetFormsForUser()
    {
        var user = await _authService.GetLoggedInUserAsync();

        return await _context.Forms
      .Include(f => f.Template)
      .Where(f => f.FilledByUserId == user.Id)
      .ToListAsync();

    }


    public async Task<Form> GetFormById(int formId)
    {
        return await _context.Forms.Include(x => x.Template).ThenInclude(x => x.Questions).Include(x => x.User).Include(x => x.Answers).FirstOrDefaultAsync(x => x.Id == formId);
    }



    public async Task<Form> Edit(Form updatedForm, int TemplateId, int FormId)
    {
        var existingForm = await GetFormById(FormId);

        var Template = await _templateService.GetTemplateById(TemplateId);

        if (existingForm == null)
            throw new Exception("Form not found");

        _context.Answers.RemoveRange(existingForm.Answers);

        foreach (var updatedAnswer in updatedForm.Answers)  
        {
            var newAnswer = new FormAnswer
            {
                Form = existingForm,
                QuestionId = updatedAnswer.QuestionId,
                TemplateId = Template.Id,  
                FormTemplate = Template, 
                Response = updatedAnswer.Response,
                QuestionType = updatedAnswer.QuestionType
            };

           await  _context.Answers.AddAsync(newAnswer);
        }

        await _context.SaveChangesAsync();
        return existingForm;
    }

    public async Task DeleteFormAsync(int formId)
    {

        var comments = await _context.Comments
                                     .Where(c => c.FormId == formId)
                                     .ToListAsync();
        _context.Comments.RemoveRange(comments);

        // Delete Answers linked to this form
        var answers = await _context.Answers
            .Include(x => x.Form)
                                    .Where(a => a.Form.Id == formId)
                                    .ToListAsync();
        _context.Answers.RemoveRange(answers);

        // Delete the form itself
        var form = await _context.Forms
                                 .FirstOrDefaultAsync(f => f.Id == formId);
        if (form != null)
        {
            _context.Forms.Remove(form);
        }

        await _context.SaveChangesAsync();
    }







}


