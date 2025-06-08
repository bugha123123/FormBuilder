using Azure;

namespace FormBuilder.Models
{
    public class FormTemplate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; }

        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Question> Questions { get; set; }
        public List<Tag> Tags { get; set; }

        //public string? ImageUrl { get; set; }
    }

}
