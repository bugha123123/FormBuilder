using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface ITemplateService
    {
        Task<List<FormTemplate>> GetFormTemplates();

        Task<List<Tag>> GetAutoCompleteTags(string kewWord);

    }
}
