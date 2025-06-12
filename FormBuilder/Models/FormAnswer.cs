namespace FormBuilder.Models
{
    // Models/Answer.cs
    namespace FormBuilder.Models
    {
        public class FormAnswer
        {
            public int Id { get; set; }

          



            public  int TemplateId { get; set; }

            public FormTemplate formTemplate { get; set; }

            public string Response { get; set; } 
        }
    }

}
