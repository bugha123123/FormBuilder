// Models/FormAnswer.cs
using FormBuilder.Enums;

namespace FormBuilder.Models
{
    public class FormAnswer
    {
        public int Id { get; set; }

        public int FormId { get; set; }
        public Form Form { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int QuestionId { get; set; }

        public int TemplateId { get; set; }
        public FormTemplate FormTemplate { get; set; }

        public string Response { get; set; }
        public QuestionType QuestionType { get; set; }
    }

}
