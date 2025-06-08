using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface ITemplateService
    {
        Task<List<FormTemplate>> GetFormTemplates();
    }
}
