namespace FormBuilder.Models
{
    // Models/Answer.cs
    namespace FormBuilder.Models
    {
        public class FormAnswer
        {
            public int Id { get; set; }


            public Form form { get; set; }
            public int QuestionId { get; set; }

            public  int TemplateId { get; set; }

            public FormTemplate formTemplate { get; set; }

            public string Response { get; set; } 
        }
    }

}
