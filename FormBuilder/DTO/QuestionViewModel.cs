using FormBuilder.Enums;

namespace FormBuilder.DTO
{
    public class QuestionViewModel
    {
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<string>? Options { get; set; }
    }
}
