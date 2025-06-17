using FormBuilder.Enums;
using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface IFormService
    {
        Task<Form> GetEmptyFormForTemplateAsync(int templateId);
        Task<Form> CreateForm(Form form, int templateId);
        Task<Form> GetFormById(int formId);
        Task<List<Form>> GetFormsForUser();


        Task AddFormComment(Comment comment, int formId, string Text);

        Task AddTemplateComment(Comment comment, int templateId, string Text);

        Task<List<Comment>> GetComments(
        int? formId = null,
        int? templateId = null,
        CommentTargetType? commentTargetType = null);
    }


}
