using FormBuilder.Models;

namespace FormBuilder.Interface
{
    public interface IFormService
    {
        Task<Form> GetEmptyFormForTemplateAsync(int templateId);
        Task CreateForm(Form form, int templateId);

        Task<List<Form>> GetFormsForUser();

        Task<Form> GetFormById(int formId);
    }

}
