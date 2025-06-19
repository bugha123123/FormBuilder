using FormBuilder.DTO;
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




        Task<Form> Edit(Form updatedForm, int TemplateId, int FormId);

    }


}
