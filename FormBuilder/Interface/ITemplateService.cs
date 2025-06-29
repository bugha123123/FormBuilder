using FormBuilder.Models;
using System.Threading.Tasks;

namespace FormBuilder.Interface
{
    public interface ITemplateService
    {
        Task<List<FormTemplate>> GetFormTemplatesAsync(string search = null, string tag = null, string sort = null);

        Task<List<Tag>> GetAutoCompleteTags(string kewWord);

        Task<FormTemplate> CreateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile? imageFile);

        Task<List<FormTemplate>> GetUserTemplates();

        Task<List<Tag>> GetTags();

        Task<FormTemplate> GetTemplateById(int id);

        Task<List<FormTemplate>> SearchTemplates(string Tag);

        Task<bool> AssignUserToTemplateAsync(int templateId, string userEmail);

        Task<List<FormTemplate>> SearchTemplatesAsync(string query);

        Task DeleteTemplatesAsync(List<int?> templateIds);

        Task UpdateTemplateAsync(FormTemplate template, List<string> selectedTagNames, IFormFile imageFile);

        Task<List<FormTemplate>> GetPopularTemplates(int count);

        Task<List<FormTemplate>> GetLatestTemplates();

        Task<FormTemplate> LikeTemplate(int templateId);

        Task<FormTemplate> UnlikeTemplate(int templateId);
        Task<bool> IsTemplateLikedByUser(int templateId);



    }
}
