using FormBuilder.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FormBuilder.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public string? OptionsJson { get; set; }
        [NotMapped]
        public List<string>? Options
        {
            get => OptionsJson == null ? null : JsonSerializer.Deserialize<List<string>>(OptionsJson);
            set => OptionsJson = value == null ? null : JsonSerializer.Serialize(value);
        }
        public int TemplateId { get; set; }
        public FormTemplate Template { get; set; }
    }

}
