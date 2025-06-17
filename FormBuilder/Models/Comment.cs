using FormBuilder.Enums;  // Make sure to add this

namespace FormBuilder.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User user { get; set; }

        public string UserId { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;

        public int? TemplateId { get; set; }
        public FormTemplate? formTemplate { get; set; }

        public int? FormId { get; set; }  
        public Form? form { get; set; }

        public CommentTargetType CommentTargetType { get; set; }
    }
}
