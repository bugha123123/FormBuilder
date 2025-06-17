// Models/FormAnswer.cs
using FormBuilder.Enums;

namespace FormBuilder.Models
{
    public class FormAnswer
    {
        public int Id { get; set; }

        public Form form { get; set; }

        public int QuestionId { get; set; }

        public int TemplateId { get; set; }

        public FormTemplate formTemplate { get; set; }

        public string Response { get; set; }

        public QuestionType QuestionType { get; set; } 
    }
}
