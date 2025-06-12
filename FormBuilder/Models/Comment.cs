namespace FormBuilder.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
        public string Text { get; set; } = string.Empty;

        public int TemplateId { get; set; }

        public FormTemplate formTemplate { get; set; }
    }
}
