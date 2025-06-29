using Azure;
using FormBuilder.Enums;

namespace FormBuilder.Models
{
    public class FormTemplate
    {
        public int Id { get; set; }
        public TemplateTitle Title { get; set; }
        public string Description { get; set; }
        public string? UserId { get; set; }

        public User User { get; set; }
        public DateTime CreatedAt { get; set; }

        public bool isPublic { get; set; }
        public List<Question> Questions { get; set; }

        public List<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<FormAnswer> Answers { get; set; } = new List<FormAnswer>();
        public int CommentId { get; set; }
        public  List<string>? AssignedUsers { get; set; }

        public virtual List<string>? SavedTags { get; set; }

        public int FilledFormsCount { get; set; }

        public List<Like> Likes { get; set; }

        public string? ImageUrl { get; set; }

    }

}
