using System.ComponentModel.DataAnnotations;
using FormBuilder.Enums;

namespace FormBuilder.Models
{
    public class Form
    {
        public int Id { get; set; }

        public int? TemplateId { get; set; }
        public FormTemplate? Template { get; set; }

        public string? UserId { get; set; }
        public User User { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        public List<FormAnswer> Answers { get; set; }

        public  int FilledCount { get; set; }

        public bool IsCompleted { get; set; } = false;


    }
}
