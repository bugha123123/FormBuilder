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


        public List<string>? AssignedUsers { get; set; }

        public virtual List<string>? SavedTags { get; set; }





        //public string? ImageUrl { get; set; }
    }

}
