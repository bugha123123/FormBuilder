using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface ITemplateService
    {
        Task<List<FormTemplate>> GetFormTemplates();

        Task<List<Tag>> GetAutoCompleteTags(string kewWord);

        Task<FormTemplate> CreateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile? imageFile);

        Task<List<FormTemplate>> GetUserTemplates();

        Task<List<Tag>> GetTags();

        Task<FormTemplate> GetTemplateById(int id);

        Task<List<FormTemplate>> SearchTemplates(string Tag);

        Task<bool> AssignUserToTemplateAsync(int templateId, string userEmail);

        Task<List<FormTemplate>> SearchTemplatesAsync(string query);

        Task DeleteTemplatesAsync(List<int> templateIds);

        Task UpdateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile imageFile);



    }
}
